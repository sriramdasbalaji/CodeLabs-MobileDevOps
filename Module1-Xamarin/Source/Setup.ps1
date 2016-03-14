$workshopPath = "c:\buildworkshop"
if (!(Test-Path -Path $workshopPath)) {
    mkdir $workshopPath 
}

xcopy $pwd\Setup\*.* $workshopPath /Y /E