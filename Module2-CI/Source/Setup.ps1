param(
    [Parameter(Mandatory=$true, HelpMessage="Your VSTS account (e.g. https://buildworkshop.visualstudio.com)")]
    [ValidateNotNullOrEmpty()]
    [string]$vstsUrl,
    
    [Parameter(Mandatory=$true, HelpMessage="Your VSTS PAT")]
    [ValidateNotNullOrEmpty()]
    [string]$vstsPat    
)

$ErrorActionPreference = "Stop"

############################################################################
#                           CONSTANTS                                      #
############################################################################

$projectName = "HealthClinic"
$workshopPath = "c:\buildworkshop"

# do not change these
$agileTemplateId = "adcc42ab-9882-485e-a3ed-7678f01f66bc"
$createProjectUri = "defaultcollection/_apis/projects?api-version=2.0-preview"
$queryProjectUri = "defaultcollection/_apis/projects/{0}?&api-version=1.0"

# create headers
$headers = @{
    Authorization = 'Basic ' + [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(":$($vstsPat)")) 
}

############################################################################
#                      START OF SCRIPT                                     #
############################################################################

############################################################################
Write-Host "Copying files" -ForegroundColor Yellow

if (!(Test-Path -Path $workshopPath)) {
    mkdir $workshopPath 
}

xcopy $pwd\Setup\*.* $workshopPath /Y /E

############################################################################
Write-Host "Creating VSTS Team Project $projectName" -ForegroundColor Yellow
$uri = "$vstsUrl/$createProjectUri"

$body = @{
    name = $projectName
    description = "Project created for Build Workshop"
    capabilities = @{
        versioncontrol = @{
            sourceControlType = "Git"
        }
        processTemplate = @{
        templateTypeId = $agileTemplateId
        }
    }
}

$response = Invoke-RestMethod -Method Post -Uri $uri -ContentType "application/json" `
                              -Headers $headers -Body (ConvertTo-Json $body)

# wait for the project to be created
$uri = "$vstsUrl/$queryProjectUri" -f $projectName
$projectId = ""
for ($i = 0; $i -lt 15; $i++) {
    Write-Host "   waiting for Team Project job to complete..."
    sleep 20
    
    $response = Invoke-RestMethod -Method Get -Uri $uri -Headers $headers
    if ($response.state -eq "wellFormed") {
        $projectId = $response.id
        break
    }
}
if ($projectId -ne "") {
    Write-Host "VSTS Team project was created successfully" -ForegroundColor Cyan
} else {
    throw "Could not create VSTS project (timeout)"
}

############################################################################
Write-Host "Initializing local Git repo and committing code" -ForegroundColor Yellow

Push-Location
cd "$workshopPath\HealthClinic.biz"

git init

git add .

git commit -m "Initial commit"

############################################################################
Write-Host "Pushing code to VSTS" -ForegroundColor Yellow

git remote add origin "$vstsUrl/DefaultCollection/_git/$projectName"
git push -u origin --all

Pop-Location

############################################################################
#                        END OF SCRIPT                                     #
############################################################################
Write-Host "Done!" -ForegroundColor Green