<a name="HOLTop" ></a>
# Continuous Integration using Visual Studio Team Services for Cross Platform Mobile Apps #

---

<a name="Overview" ></a>
## Overview ##

Continuous Integration (CI) is a foundational pillar of good DevOps. Once set up, each time code is committed to the source repo, a build is triggered. The build not only compiles the code, but ideally runs code analysis, unit (and sometimes even integration) tests. This provides the team with rapid feedback on the quality of the code just committed. The CI build can even package the code so that it is ready for deployment to test, QA or even Production environments.

In [Module 1](../Module1-Xamarin), you covered developing cross platform mobile apps using [Xamarin](https://xamarin.com/). You learned about the project structure and **Portable Class Libraries** and saw how to unit test the core of the application. You then created a [Visual Studio Team Services](https://www.visualstudio.com/products/visual-studio-team-services-vs.aspx) (VSTS) Team Project and committed the code into it.

### A Note About HealthClinic.Biz ###
The solution that you will use for this workshop is from [HealthClinic.biz](https://github.com/microsoft/healthclinic.biz). This is a sample repo that Microsoft created. The code has been modified slightly for this workshop, so if you go to the GitHub repo you may find differences. The mobile apps in the project connect to two services that are hosted in Azure - an Azure Mobile App and an Azure Web App. The Azure apps have been modified for this workshop so that they are _read only_. Any add, update or delete method will return a successful response but will not modify any data in the backing database. If you wish to host these services yourself after the workshop, then please refer to the [Deployment to Azure](https://github.com/Microsoft/HealthClinic.biz/wiki/Deployment-to-Azure) page in the wiki.

<a name="Objectives" ></a>
### Objectives ###
In this module, you will see how to:

- Create a (CI) build for the application in VSTS
- Analyze test results and code coverage for a build
- Log rich bugs from test failures
- Trigger a build by committing code to the repo
- Improve the versioning of the compiled app

<a name="Prerequisites"></a>
### Prerequisites ###

The following is required to complete this module:

- An email address for creating a new Xamarin account and VSTS account (you will skip this if you have already done Module 1)

<a name="Setup" ></a>
### Setup ###

> **Note**: If you have completed Module 1, then you can skip this section and go straight to [Exercise 1](#Exercise1)

#### Sign up for a VSTS Account ####

Before continuing, you must sign up for a free VSTS account.

> **Note**: You can skip this task if you already have a VSTS account. However, you need to ensure that it is an account in which you are the account owner. In other words, if you create the account yourself, you can skip this step and just sign into your account. However, if some else created the account and added you to the account, then you will need to complete this step. Once you have signed into your existing account, then create a new Team Project as specified in [Task 2](#Ex4Task2).

<a name="SetupTask1"></a>
#### Setup Task 1 - Create a New VSTS Account ####

In this setup task, you will create a new VSTS account.

1. Browse to [https://go.microsoft.com/fwlink/?LinkId=307137](https://go.microsoft.com/fwlink/?LinkId=307137). Enter your Microsoft Account credentials.

    ![Signing in to visualstudio.com](Images/vsts-signin-no-account.png "Signing into visualstudio.com")

    _Signing into visualstudio.com_

    > **Note**: If you do not have a Microsoft Account, then you will need to create one by clicking **Sign up now**. Once you have created an account, you can sign in using that account.

1. Create a new VSTS account. Enter the following information.

    - Your name
    - Your region
    - The name of your new account
    - Click the **Create Account** button

    ![Creating a New VSTS Account](Images/vsts-new-account-form.png "Creating a New VSTS Account")

    _Creating a New VSTS Account_

    > **Note**: You can change your email address and region if you want to.    

1. Once the account is created, you will see the VSTS landing page, which will prompt you to create a new Team Project. The setup script will do this for you, so you can just go on to the next task.

<a name="SetupTask2"></a>
#### Setup Task 2 - Generate a Personal Access Token (PAT) ####

In this setup task, you will generate a Personal Access Token (PAT) which is required to run the setup script.

> **Note**: Generating PATs for API calls is best practice - the tokens can be revoked at any time, and can be scoped as required.

1. In the upper right corner of the browser, pull down the menu by clicking your name. Then click **My profile**.

    ![Accessing My Profile page](Images/vsts-my-profile.png "Accessing My Profile page")

    _Accessing My Profile page_

1. Click the Security menu on the left. In the Personal Access Tokens section, click the **Add** button.

    ![Click Add in the Personal Access Token section](Images/vsts-add-pat.png "Click Add in the Personal Access Token section")

    _Click Add in the Personal Access Token section_

1. In the form, enter the name BuildWorkshop. Leave all the defaults and click Create:

    ![Creating a PAT](Images/vsts-create-pat-form.png "Creating a PAT")

    _Creating a PAT_

1. After the token is created, it will be displayed to you, but only once. Make a note of this access token - if you lose it you will have to create a new one.

    ![Copying the PAT](Images/vsts-note-pat.png "Copying the PAT")

    _Copying the PAT_
 
<a name="SetupTask3"></a>
#### Setup Task 3 - Create a Xamarin Account ####

In this task, you will create a Xamarin account.

> **Note**: If you have a Xamarin business license you can use it and skip this task.

1. Open a browser to [https://store.xamarin.com/login](https://store.xamarin.com/login "Xamarin Store login"). Click the **Create a new account** link.

	![Xamarin store login](Images/xamarin-store-login.png "Xamarin store login")

	_Xamarin Store Login screen_

1. Fill in a name, email address and password. Make a note of the password, since you will need this in a later exercise. Make sure you check both checkboxes and then click the **Register** button.

    ![Create a new Xamarin account](Images/xamarin-new-account.png "Create a new Xamarin account")

    _Create a new Xamarin account_

	> **Note**: If you get a "validation failed" error, it probably means that your password is not strong enough. Make sure your password is at least 12 characters long.

<a name="SetupTask4"></a>
#### Setup Task 4 - Activate your Xamarin Account in Visual Studio ####

In this setup task, you will activate your Xamarin Account in Visual Studio.

1. Open the Xamarin Account window. To do this, start Visual Studio. In the **Tools** menu, click **Xamarin Account...**.

    ![Xamarin Account in the Tools Menu](Images/vs-xamarin-account-menu.png "Xamarin Account in the Tools Menu")

    _Xamarin Account in the Tools Menu_

1. Click the **Log In** button.

    ![Xamarin Login in Visual Studio](Images/vs-xamarin-account-login.png "Xamarin Login in Visual Studio")

    _Xamarin Login in Visual Studio_  

1. Enter the email address and password that you used for creating your Xamarin account in [Task 1](#Ex1Task1) and press the **Login** button.

    ![Xamarin Credentials in Visual Studio](Images/vs-xamarin-account-creds.png "Xamarin Credentials in Visual Studio")

    _Xamarin Credentials in Visual Studio_  

1. Once activated, you should see your account has been logged in.

    ![Logged in to Xamarin in Visual Studio](Images/vs-xamarin-account-activated.png "Logged in to Xamarin in Visual Studio")

    _Logged in to Xamarin in Visual Studio_

1. You can close the dialog.

<a name="SetupTask5"></a>
#### Setup Task 5 - Activate a Xamarin Business Trial ####

In this task, you will open the Xamarin solution for cross-mobile apps for HealthClinic.biz and activate your Xamarin Business trial license.

1. In Visual Studio, click **File->Open->Project/Solution**. Browse to **c:\buildworkshop\HealthClinic.biz** and open **04_Demos_NativeXamarinApps.sln**.

1. Once the solution is open, you may be prompted to configure a Xamarin Mac agent. Since we don't have one for the workshop, you can check the **Don't show this again** checkbox and click **OK**.

    ![The Xamarin Mac Agent Dialog](Images/vs-mac-agent.png "The Xamarin Mac Agent Dialog")

    _The Xamarin Mac Agent Dialog_

    > **Note**: In order to compile Xamarin.iOS projects, you will need a Mac agent running on a Mac machine. Every time you open the solution, you will be prompted to connect to your Mac agent - since you won't have one for this workshop, you can just close the dialog.

1. Click **Build->Build Solution** to build the solution. (You can also use **Ctrl+Shift+B** or **F6** depending on your Visual Studio Settings). The build should fail. The error message will indicate that the project is too large and requires business or higher license.

    ![Build failure requiring Xamarin Business Edition](Images/vs-xamarin-requires-business.png "Build failure requiring Xamarin Business Edition")

    _Build failure requiring Xamarin Business Edition_

1. **Double click the error**. This will launch the Xamarin license dialog. Click on **Begin a Trial** to begin a trial of the Business edition of Xamarin.

    ![Select Begin a Trial](Images/vs-xamarin-trial.png "Select Begin a Trial")

    _Select Begin a Trial_

1. Once the trial has been activated, you will see a confirmation dialog.

    ![Xamarin Business Trail confirmation](Images/vs-xamarin-trial-success.png "Xamarin Business Trail confirmation")

    _Xamarin Business Trail confirmation_
    
<a name="SetupTask6"></a>
#### Setup Task 6 - Run the Setup Script ####

In order to run the exercises in this module, you will need to set up your environment first. **If you have completed Module 1, skip this section**.

1. Open a **Windows PowerShell** command prompt and `cd` to the Modules **Source** folder.

1. Enter the following command.

	```powershell
	.\Setup.ps1 -vstsUrl https://{youraccount}.visualstudio.com -vstsPat {yourPAT} -xamarinEmail {xamarinEmail} -xamarinPassword {xamarinPassword}
	```

	where:
	- `{youraccount}` is the VSTS account name you created earlier 
	- `{yourPAT}` is the VSTS PAT you created earlier.
	- `{xamarinEmail}` is the email address for your Xamarin account
	- `{xamarinPassword}` is the password for your Xamarin account

	> **Note**: For example, the command should look something like:
	> 
	> ```powershell
	.\Setup.ps1 -vstsUrl https://colbuildworkshop.visualstudio.com -vstsPat pvzgfvhjh5fhsldfh248sl6ifyidfsdisdfs5vbchdsdffksd9hfk3qooh -xamarinEmail myemail@outlook.com -xamarinPassword P@ssw0rd
	> ```

1. Wait until you see a green `Done!` before continuing.

	![The Setup script completed successfully](Images/setup-done.png "The Setup script completed successfully")

	_The Setup script completed successfully_

---

<a name="Exercises" ></a>
## Exercises ##
This module includes the following exercises:

1. [Remove the iOS Projects from the Solution](#Exercise1)
1. [Create a Team Build](#Exercise2)
1. [Queue the Build and Fix the Bug](#Exercise3)
1. [Improve Package Versioning](#Exercise4)

Estimated time to complete this module: **60 minutes**

> **Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.

<a name="Exercise1" ></a>
### Exercise 1: Remove the iOS Projects from the Solution ###

In this exercise, you will remove the iOS applications from the solution. Module 1 covered in detail how Xamarin allows you to code iOS applications using C# on Windows! However, compiling the iOS projects requires Mac hardware, which you do not have for this lab.

> **Note**: You could use easily a service like [MacInCloud](http://www.macincloud.com/) to "rent" Mac hardware for your builds. The VSTS cross-platform agent is itself cross-platform, so it can run on any Mac servers you have as well.

<a name="Ex1Task1"></a>
#### Task 1 - Open the HealthClinic.Biz Xamarin Solution ####

> **Note**: You can skip this task if you have completed Module 1.

In this task, you will open the Xamarin solution for cross-mobile apps for HealthClinic.biz and activate your Xamarin Business trial license.

1. In Visual Studio, click **File->Open->Project/Solution**. Browse to **c:\buildworkshop\HealthClinic.biz** and open **04_Demos_NativeXamarinApps.sln**.

1. Once the solution is open, you may be prompted to configure a Xamarin Mac agent. Since we don't have one for the workshop, you can check the **Don't show this again** checkbox and click **OK**.

    ![The Xamarin Mac Agent Dialog](Images/vs-mac-agent.png "The Xamarin Mac Agent Dialog")

    _The Xamarin Mac Agent Dialog_

    > **Note**: In order to compile Xamarin.iOS projects, you will need a Mac agent running on a Mac machine. Every time you open the solution, you will be prompted to connect to your Mac agent - since you won't have one for this workshop, you can just close the dialog. 

1. You should see 8 projects in the solution.

    ![Xamarin Projects in the Solution](Images/vs-solution-explorer.png "Xamarin Projects in the Solution")

    _Xamarin Projects in the Solution_

1. Click **Build->Build Solution** to build the solution. (You can also use **Ctrl+Shift+B** or **F6** depending on your Visual Studio Settings). The build should fail. The error message will indicate that the project is too large and requires business or higher license.

    ![Build failure requiring Xamarin Business Edition](Images/vs-xamarin-requires-business.png "Build failure requiring Xamarin Business Edition")

    _Build failure requiring Xamarin Business Edition_

1. **Double click the error**. This will launch the Xamarin license dialog. Click on **Begin a Trial** to begin a trial of the Business edition of Xamarin.

    ![Select Begin a Trial](Images/vs-xamarin-trial.png "Select Begin a Trial")

    _Select Begin a Trial_

1. Once the trial has been activated, you will see a confirmation dialog.

    ![Xamarin Business Trail confirmation](Images/vs-xamarin-trial-success.png "Xamarin Business Trail confirmation")

    _Xamarin Business Trail confirmation_

1. Now clean and rebuild the solution. Ensure that the solution builds without errors. This may take a few moments, since Visual Studio will need to restore components and packages that the projects require.

    > **Note**: If you see the error `The name 'InitializeComponent' does not exist in the current context` you may safely ignore it.

    ![Ignore the InitializeComponent error](Images/vs-ignore-error.png "Ignore the InitializeComponent error")

    _Ignore the InitializeComponent error_

<a name="Ex1Task2"></a>
#### Task 2 - Remove the iOS Projects from the Solution ####

In this task, you will remove the iOS applications from the solution.

> **Note**: If did not complete Module 1, take a few moments to browse the iOS projects before you remove them.

1. Make sure the solution is open. In Visual Studio, click **File->Open->Project/Solution**. Browse to **c:\buildworkshop\HealthClinic.biz** and open **04_Demos_NativeXamarinApps.sln**.

    > **Note**: If you have the **04_Demos_NativeXamarinApps.sln** open, then go to the next step.

1. Once the solution is open, you may be prompted to configure a Xamarin Mac agent. Since we don't have one for the workshop, you can check the "Don't show this again" checkbox and click **OK**.

1. Remove the iOS projects from the solution. To do this, in **Team Explorer**, right-click each project ending with iOS and click **Remove**.

    ![Remove the iOS projects from the solution](Images/vs-remove-ios-projects.png "Remove the iOS projects from the solution")

    _Remove the iOS projects from the solution_

1. Once the 3 iOS projects are removed, your solution should look like the following.

    ![Solution Structure without iOS projects](Images/vs-project-structure.png "Solution Structure without iOS projects")

    _Solution Structure without iOS projects_

1. In the **Solution Explorer**, right-click the solution node (the top-most node) and click **Commit**. The only change should be the solution file.

1. Enter `Removing iOS projects` as the commit message. Then, click the drop-down next to the **Commit** button and click  **Commit and Push**.

    ![Commit the Solution changes](Images/vs-commit-solution-change.png "Commit the Solution changes")

    _Commit the Solution changes_

<a name="Exercise2" ></a>
### Exercise 2: Create a Team Build ###

In this exercise, you will create a Team Build to compile the solution, run unit tests and package the Android application. In Module 3, you will see how to put this package into a Continuous Deployment pipeline to distribute the app code to users.

<a name="Ex2Task1"></a>
#### Task 1 - Create the Build Definition ####

In this task, you will log in to VSTS and create a build from the `master` branch in the repo.

1. Log in to the **HealthClinic** team project in VSTS. In order to do this, open a browser and navigate to your VSTS account (something like _https://buildworkshop.visualstudio.com_). Once you are logged in, you may see a list of recently accessed Team Projects. If **HealthClinic** is listed, click the link. If it is not listed, then click the **Browse** link and select the **HealthClinic** team project.

    ![Browsing to HealthClinic Team Project](Images/vsts-browse-to-tp.png "Browsing to HealthClinic Team Project")

    _Browsing to HealthClinic Team Project_

1. In the top menu bar, click **BUILD** to open the build hub. In the menu on the left, click the green **+** button to create a new build definition.

    ![Click the New Build definition button](Images/vsts-click-create-build.png "Click the New Build definition button")

    _Click the New Build definition button_

1. Select **Visual Studio** from the list of build templates and click **Next**.

    ![Select the Visual Studio build template](Images/vsts-build-wiz-vsbuild.png "Select the Visual Studio build template")

    _Select the Visual Studio build template_

    >**Note**: You will see a Xamarin.Android and a Xamarin.iOS build template as well. For this workshop, you will be building multiple projects including the Portable Class Library (PCL) and the unit test project. For this reason, the Visual Studio build template is a better choice. Feel free to come back later and select the Xamarin templates to see how the task orchestration differs for those templates.

1. Make sure that the repo settings are correct (it should be the **master** branch on the **HealthClinic** repo). Check the **Continuous Integration** checkbox and ensure that the **Default agent queue** is set to **Hosted** and click **Create**.

    ![Build Repo and Trigger](Images/vsts-build-wiz-repo.png "Build Repo and Trigger")

    _Build Repo and Trigger_

    >**Note**: The **Continuous Integration** trigger tells VSTS to trigger this build to run each time that code is pushed to the **master** branch.

    >**Note**: The **Hosted Agent** is an agent that is hosted by VSTS. You can also install agents onto your own datacenter (or machine). These are known as _private agents_. When builds are queued onto the hosted agent, a VM is spun up to execute the build. The VM is then torn down again once the build completed. This means that you do not have to maintain your own build infrastructure. However, the hosted build VM image is maintained by VSTS and is preconfigured with software like Visual Studio, npm and other build requirements. If you require software that is not on the hosted agent, you will have to use a private agent. Fortunately for Xamarin projects, the hosted agent has everything required to build and package Xamarin projects. Once again iOS builds are the exception, requiring Mac hardware. You can integrate the hosted build into [MacInCloud](http://www.macincloud.com/) in order to build iOS packages.

<a name="Ex2Task2"></a>
#### Task 2 - Configure the Build Tasks ####

In this task, you will configure the Build tasks.

1. The solution requires several NuGet packages. Typically, the packages are not committed into the source repo. Therefore, the build must _restore_ the list of packages, pulling the packages from a NuGet feed. The NuGet installer task does exactly this.

    Since there are multiple solutions in the repo, you need to configure which solution NuGet should use to get the list of required packages. Click on the NuGet installer task. Click the **...** button to the right of the **Path to Solution** parameter.

    ![Browse to the Solution for NuGet](Images/vsts-build-nuget-browse.png "Browse to the Solution for NuGet")

    _Browse to the Solution for NuGet_

1. Expand the **HealthClinic** node, click  **04_Demos_NativeXamarinApps.sln** and click **OK**.

    ![Selecting the solution](Images/vsts-build-nuget-sln.png "Selecting the solution")

    _Selecting the solution_

1. Before the Xamarin solution can be built, the Xamarin license must be activated on the build server. Click the **Add build step...** in the toolbar to add a new task. In the list of tasks, click **Utility** and then scroll down to the **Xamarin License** task. Click the **Add** button to add this task and then click **Close**.

    ![Add the Xamarin License task](Images/vsts-build-add-xamarin.png "Add the Xamarin License task")

    _Add the Xamarin License task_

1. This adds the **Xamarin License** task at the bottom of the definition. Click and drag the task so that it is just below the **NuGet Installer** task.

1. Make sure that the **Action** parameter is set to **Activate**. Enter `$(XamarinEmail)` and `$(XamarinPassword)` into the **Email** and **Password** parameters.

    ![Configure the Xamarin Activation task](Images/vsts-build-xamarin-task-config.png "Configure the Xamarin Activation task")

    _Configure the Xamarin Activation task_

    >**Note**: You could "hardcode" the username and password parameters. However, you will shortly add another **Xamarin License** task to deactivate the license which will require the same username and password parameters. The `$(name)` notation tells the build to look in the build variables to find a variable called _name_ and use its value.

1. Configure the Xamarin Username and Password Variables. Click the **Variables** tab in the build definition. Click the **Add Variable** button at the bottom of the table and add the two Xamarin parameters. Enter your Xamarin email address and password in the correct value boxes. Finally, click the padlock icon to the right of the password value to make it a _secret_ variable.

    ![Setting the Build Variables](Images/vsts-build-variables.png "Setting the Build Variables")

    _Setting the Build Variables_

    > **Note**: Secret variables can be used by build tasks, but will be masked in the Variables pane. If you unset the padlock, the value is emptied so that no-one can see the value of the variable. The build logs will also mask the secret variable so that it will not be visible in the logs. However, developers could still write tasks or scripts that would expose the value of a secret variable.

1. Configure Building the solution. Back on the **Build** tab, click the **Visual Studio Build** task and update the solution to **04_Demos_NativeXamarinApps.sln** just like you did in the **Nuget Installer** task. This will build all the projects in the solution.

    ![Build the solution](Images/vsts-build-solution.png "Build the solution")

    _Build the solution_

1. Configure the Unit Test run. Click the **Visual Studio Test** task. By default, this task finds any assembly with the word `test` in it and runs the tests in that assembly. Since you have unit tests and UI tests, you will need to constrain the filter to ensure that only unit tests run during the build.

    >**Note**: It is possible to run the UI tests using either [Xamarin Test Cloud](https://xamarin.com/test-cloud) or [Perfecto Mobile](http://www.perfectomobile.com/). These services allow you to run the tests against a myriad of devices, recording results of runs against each device. If you click **Add new task** and browse to the **Test** tasks, you will see a `Xamarin Test Cloud` task. You can install the Perfecto task to your account through the [VSTS Marketplace](https://Marketplace.visualstudio.com/). For this lab, you will not run tests against either Xamarin Test Cloud or Perfecto, but it is possible to do so.

1. In the **Test Assembly** parameter, change the word `test` to `unittest`. Check the **Code Coverage Enabled** checkbox. Your task should look as follows:

    ![Configure Test Run](Images/vsts-build-test-run.png "Configure Test Run")

    _Configure Test Run_

1. Add a Visual Studio Build task to build the Android Package. When the solution builds with the existing **Visual Studio Build** task, it will only compile the Android project. In order to deploy the Android application, an **apk** file must be produced and signed. The **Xamarin.Android** projects can do this if instructed to do so. Click **Add build step...** to add a new task to the build. In the **Build** tab, find the **Visual Studio Build** task and click the Add button. Close the dialog.

1. Again the task is added at the bottom of the task list, so drag it so that it is just below the **Visual Studio Test** task.

1. Click the **...** button next to the **Solution** parameter and browse to **src/MyHealth.Client.Droid/MyHealth.Client.Droid.csproj**. Select the **csproj** file and click OK.

1. Add `/t:SignAndroidPackage` to the **MSBuild Arguments** parameter. This tells Visual Studio to produce an **apk** file and to sign it using a developer keystore.

1. Finally, type `$(BuildConfiguration)` into the **Configuration** parameter. Your task should look as follows.

    ![Configure Android Package Build](Images/vsts-build-android-package.png "Configure Android Package Build")

    _Configure Android Package Build_

    > **Note**: In real life scenarios, you would create a private keystore for signing your Android packages securely. You can read more about this process in this [MSDN article](https://msdn.microsoft.com/en-us/library/vs/alm/build/apps/secure-certs).

1. Configure the Copy Files Task. Click the **Copy Files** task. This task will copy files that we want as outputs of this build to the build artifacts staging directory. Any files in this directory will be published in the **Publish Build Artifacts** step.

1. Change the **Source Folder** parameter to be **src/MyHealth.Client.Droid**. Change the **Contents** parameter to be `**\*-signed.apk`. The **Contents** parameter supports a minimatch filter, so this value tells the task to copy any file ending with `-signed.apk` in any subdirectory of the **Source Folder** to the **Target Folder**. Your task should look as follows.

    ![Configure Copy Files Task](Images/vsts-build-copy-task.png "Configure Copy Files Task")

    _Configure Copy Files Task_

    > **Note**: The UWP application is being built as part of the same build. You could also copy the **appxupload** file - the signed app - to the build outputs so that you can release the UWP app from the same build. You will not do so in this workshop.

1. Deactivate the Xamarin License. Click the **Add build step...** button. Under **Utility**, find the **Xamarin License** task and click **Add**. Then close the dialog.

1. Drag this new task above the **Publish Build Artifacts** task. Change the **Action** parameter to **Deactivate**. Enter `$(XamarinEmail)` and `$(XamarinPassword)` for the email and password parameters respectively. Finally, make sure that you check the **Always Run** checkbox. This will ensure that the Xamarin license is always deactivated, even if a previous step fails (like unit tests for example).

    ![Deactivate the Xamarin License](Images/vsts-build-xamarin-deactivate.png "Deactivate the Xamarin License")

    _Deactivate the Xamarin License_

1. Save the Build Definition. In the toolbar of the build definition, click **Save**. Enter the name `Xamarin CI` and click **OK**.

    ![Save the Build Definition](Images/vsts-build-save-build-def.png "Save the Build Definition")

    _Save the Build Definition_

<a name="Exercise3" ></a>
### Exercise 3: Queue the Build and Fix the Bug ###

In this exercise, you will manually queue the build and analyze the build report. The build will fail, and you will log a bug to fix the problem. You will then
fix the bug and push the fix to the VSTS repo - this will automatically trigger the CI build.

<a name="Ex3Task1"></a>
#### Task 1 - Queue the Build and Analyze the Build Report ####

In this task, you will queue a build, and once complete, examine the build report and log a bug for the failing test.

1. Even though this build will be triggered whenever commits are pushed to the master branch on the repo, you can still manually queue a build at any time. In the toolbar of the build definition, click **Queue build...** and then click **OK**.

    ![Manually queue the build](Images/vsts-build-queue.png "Manually queue the build")

    _Manually queue the build_

1. Once you have queued the build, you will see the log. This is a real-time log from the hosted agent as it runs the tasks you specified in the build definition.

    ![The build log](Images/vsts-build-log.png "The build log")

    _The build log_

1. The build should fail. Looking at the tasks in the left of the log will show you that the **Test Assemblies...** task failed. In Module 1, you have seen that one of the unit tests was failing - the test is failing in the build as well.

1. In the toolbar, click the build number (this will be of the format `Build yyyymmdd.1`). This is the build number that VSTS assigned to the build when it was queued.

    ![Click on the build number](Images/vsts-build-click-buildnum.png "Click on the build number")

    _Click on the build number_

1. This will open the build report.

    ![The build report for the failed build](Images/vsts-build-failed-report.png "The build report for the failed build")

    _The build report for the failed build_

1. In the graphs on the right, you will see that there is a test failure. You will also see trends showing the difference in failures, pass rate and run duration between this build and previous builds (not too useful at the moment since this is the first build). Under Code Coverage you will see the code coverage results.

    >**Note**: If you click **Download Code Coverage results** the results will open in Visual Studio and you will be able to drill down into coverage details.

1. Analyzing the Test Failure. To get more detail about the test failure, click the **Tests** tab at the top of the report.

    ![Open the Test Results page](Images/vsts-build-click-tests.png "Open the Test Results page")

    _Open the Test Results page_

1. In the test list, click the failed test. On the right, you will see the Error message (i.e. _Assert.IsNotNull failed_) as well as the full stack trace.

1. Logging a bug. Logging a bug to track the fix is optional - but can be useful if you're going to defer the bug fix. It is best practice to fix CI build failures as soon as possible, since a failing CI build is signaling that there is a quality issue in the code.

1. Click the **Create bug** button in the toolbar above the test outcome list.

    ![Create a bug for the test failure](Images/vsts-test-results-create-bug.png "Create a bug for the test failure")

    _Create a bug for the test failure_

1. Take a moment to view the information that VSTS automatically fills in on the Bug form. The **Repro Steps** field contains the name of the test, the name of the machine on which the test was running, the build that the test was running in, the error message and the stack trace. This is a rich bug - and it required a single button click to create!

    > **Note**: The stack trace shows the line that the `Assert.IsNotNull failed` on (83). Make a note of this for the next exercise.

1. You can change any of the fields if you need to - for now, just click **Unassingned** at the top of the form and assign the bug to yourself. Click the **Save** button in the toolbar on the upper right.

    ![Assign the bug to yourself](Images/vsts-build-create-bug.png "Assign the bug to yourself")

    _Assign the bug to yourself_

1. Once the Bug is saved, it will obtain an ID from VSTS. Make a note of the ID in the upper left of the form.

    ![Note the Bug ID](Images/vsts-build-bug-id.png "Note the Bug ID")

    _Note the Bug ID_

1. Close the form.

<a name="Ex3Task2"></a>
#### Task 2 - Fix the Bug in Visual Studio ####

In this task, you will open the failing test in Visual Studio and fix the bug.

1. Open Visual Studio and open the **04_Demos_NativeXamarinApps.sln** solution if it not already open.

1. If the Test Window is not open, then click **Test->Windows->Test Explorer**. If the test list is empty, then build the solution to discover the tests.

1. In the Test Window, double-click the **Test_RetrieveAppointments_WhenMoreThanOneAppointments_InitsCorrectly** to open it.

1. In the previous exercise, the stack trace of the failing test showed you that the `Assert.IsNotNull` on line 83 failed. Go to line 83 by pressing **Ctrl+G**, entering **83** and pressing enter. This takes you to the following failing assertion.

    ```csharp
    Assert.IsNotNull(homeviewModel.Appointments);
    ```

    For some reason, when the test gets to this point, the **Appointments** property is null.

1. In the **act** section of the test, the method that is being exercised by this test is called - **HomeViewModel.RetrieveAppointmentsAsync**. Click on the method and then press **F12** to navigate to this method.

1. Fixing the bug. You will immediately see that there is a line of code that has been commented out. Some developer may have been testing locally and commented out the code and committed the change by mistake. Whatever the cause of the bug, the fix is simple: just uncomment the assignment to **Appointments** so that the code looks like the following.

    ```csharp
    internal async Task RetrieveAppointmentsAsync ()
    {
        var appointments = await _myHealthClient.AppointmentsService.GetPatientAppointmentsAsync (AppSettings.DefaultPatientId, AmountOfAppointments);
        if (appointments.Count > 0)
            FirstAppointment = appointments.First ();
        if (appointments.Count >= 2)
        {
            SecondAppointment = appointments.ElementAt(1);
            Appointments = new ObservableCollection<Appointment>(appointments.Skip(2));
        }
    }
    ```

1. Running the test to ensure the fix is valid. In the Test Explorer window, right-click the **Test_RetrieveAppointments_WhenMoreThanOneAppointments_InitsCorrectly** test and select **Run selected tests**. The test should now pass. To make sure that the fix did not break any other code, run all the tests in the **MyHealth.Client.Core.UnitTests** project (you may have to group by project if you have not already done so using the toolbar button at the top of the Test Explorer window).

    ![Run all the tests](Images/vs-run-all-tests.png "Run all the tests")

    _Run all the tests_

1. Committing and pushing the bug fix. Now that you have verified that your bug fix works, go to the **Team Explorer** window and navigate to the **Changes** pane. Enter the message `Fixing Appointments assignment - bug #1` into the message box. (If your bug was not ID 1, then use whatever ID your bug had in the previous exercise). This associates this commit to work item with id **#1** - in this case, the bug.

    > **Note**: The association is only made by VSTS one the commit has been pushed to the server.

1. In the **Changes** pane, pull down the menu of the **Commit** button and press **Commit and Push**.

    ![Commit and push the bug fix](Images/vs-commit-bug-fix.png "Commit and push the bug fix")

    _Commit and push the bug fix_

1. Viewing the triggered build. Once the commit has been pushed, you can open the browser to the **BUILD** hub of your team project again. Click the **Xamarin CI** node in the list of builds and then click the **Queued** tab.

    ![Viewing queued builds](Images/vsts-build-ci-trigger.png "Viewing queued builds")

    _Viewing queued builds_

1. The icon on the very left of the build shows that this is a CI triggered build (it is empty when a build is queued manually). Double click the build to open the log.

1. Once the build has completed, click on the build number to view the build report.

    ![Successful build](Images/vsts-build-report-success.png "Successful build")

    _Successful build_

1. In the **Test Results** section, you will see that all 24 tests are passing. The build has 1 less failure than the previous build and the pass percentage is up by 4.2%. Under **Associated work items** you should see that the bug is associated with this build - this is because the bug is associated with a commit that is included in this build. Click the **Bug #** link to open the bug. In the **Development** section, you will see the commit has been associated with the bug.

    ![The commits associated to this work item](Images/vsts-bug-commit.png "The commits associated to this work item")

    _The commits associated to this work item_

1. Clicking on the commit will open it in the **CODE** hub.

    ![The commit details in VSTS](Images/vsts-bug-commit-details.png "The commit details in VSTS")

    _The commit details in VSTS_

1. Viewing branch policies. For this workshop, you simply fixed the bug on the **master** branch. A more typical workflow is as follows:

    - A lead developer prevents direct pushes to **master**.
    - A team member that wants to make changes creates a local branch off **master** and makes changes in the branch.
    - When ready, the team member publishes the branch to the repo in VSTS and initiates a **Pull Request**.
    - The Pull Request checks if there will be any merge conflicts when merging back into **master**.
    - The Pull Request can be configured to queue a build to ensure that the build succeeds before merging.
    - Reviewers review the code changes and can comment on the code.
    - The team member can make updates to the code in response to the comments, pushing the changes to the branch.
    - One reviewers have signed off on the changes, the reviewers can initiate the merge on the server.

1. To see how to configure this workflow, navigate to the **CODE** hub and click the **HealthClinic** repo in the menu. Click **Manage repositories...**.

    ![Manage the repository](Images/vsts-manage-repos.png "Manage the repository")

    _Manage the repository_

1. Expand the **HealthClinic** repo node in the left menu and click the **master** branch. Then click **Branch Policies** in the menu.

    ![Branch Policies](Images/vsts-build-policies.png "Branch Policies")

    _Branch Policies_

1. Take a look at the various options, but make sure that you click **Undo changes** before exiting the page.

    > **Note**: In the example image, the `Xamarin CI` build will be queued every time a pull request into `master` is initiated. The Pull Request will be blocked unless there is a valid build. The policy also requires that work items be associated with the commits and blocks if there are none. Finally, the policy enforces that at least 2 reviewers must approve the Pull Request before it can be merged.

1. Viewing the Build Artifacts. Go back to the **BUILD** hub. At the top of the build report for the CI build, click the **Artifacts** link.

    ![View the artifacts](Images/vsts-build-artifacts.png "View the artifacts")

    _View the artifacts_

    This build is configured to produce a single artifact called **drop** (via the **Publish Build Artifacts** task in the build definition).

    > **Note**: Builds can have multiple artifacts - in fact, there will be one for every **Publish Build Artifacts** task in the build definition.

1. Click the **Explore** link on the **drop** artifact. Expand the folders until you can see the signed **apk** file.

    ![The Artifacts Explorer](Images/vsts-build-artifacts-explorer.png "The Artifacts Explorer")

    _The Artifacts Explorer_

    > **Note**: In Module 3, you will see how to release this **apk** file to users through a Release Management pipeline.

1. Close the Artifacts Explorer.

<a name="Exercise4" ></a>
### Exercise 4: Improve Package Versioning ###

In this exercise, you will improve the versioning of the Android package by installing a custom build task from the VSTS Marketplace.

<a name="Ex4Task1"></a>
#### Task 1 - Examine the Android Manifest file  ####

Currently the build produces an **apk** file that has the version code **1.0** for every build. When releasing packages, it is best practice to version the package to match the build number so that you can track which build produced which package. In Module 3, you will learn how to release the package (the **apk** file) that the CI build produces using Release Management. It will be important to have unique version codes for audit purposes during the release pipeline. For now, you will simply configure the build to make the version code of the **apk** match the build number in VSTS.

In this task, you will examine the **AndroidManifest.xml** file to see where the **apk** version is defined.

1. Open Visual Studio and open the **04_Demos_NativeXamarinApps.sln** solution if it not already open.

1. In the Solution Explorer, expand the **MyHealth.Client.Droid** project, then expand the **Properties** folder and double click **AndroidManifest.xml** to open it

    ![Open the Android Manifest file](Images/vs-open-android-manifest.png "Open the Android Manifest file")

    _Open the Android Manifest file_

1. In the file, you will see an attribute called **android:versionCode** in the **manifest** element. It is currently set to **1**. This version code is the "minor" version code for the **apk** file that is created when calling the **SignAndroidPackage** target on the **MyHealth.Client.Droid** project. This number will have to be matched to the build number during the build.

    > **Note**: The versionName is the "major" version number and should only be updated manually when a major release of your app is to be published.

    Now that you know where to set the version code for the **apk**, you can configure the build to set it during the build.

<a name="Ex4Task2"></a>
#### Task 2 - Install an Extension from the VSTS Marketplace ####

In this task, you will install a VSTS extension that contains some custom build tasks that will help you set the **android:versionCode** during the build.

> **Note**: You could do this many ways, including writing a PowerShell or bat script. However, it is always a good idea to search the Marketplace to see if someone has created a Task that solves a problem you are facing before you reinvent the wheel.

1. In the browser, open the **HealthClinic** Team Project in your VSTS account. In the upper right corner, click the Basket icon and select **Browse Marketplace**

    ![Browse to the Marketplace](Images/vsts-open-marketplace.png "Browse to the Marketplace")

    _Browse to the Marketplace_

1. In the toolbar at the top, click the magnifying glass icon. Enter the term `version assemblies` and press enter. Click the tile **Colin's ALM Corner Build Tasks**

    ![Search for Version Assemblies in the Marketplace](Images/vsts-marketplace-search.png "Search for Version Assemblies in the Marketplace")

    _Search for Version Assemblies in the Marketplace_

1. This extension contains several build tasks. One of them is **VersionAssemblies** and will allow you to version the **apk** file via the Android manifest file during the build. Click the **Install** button.

1. In the confirmation window, ensure that the correct VSTS account is selected and click the **Continue** button. Once permission has been verified, click the **Confirm** button.

    ![Confirm the install](Images/vsts-install-col-tasks.png "Confirm the install")

    _Confirm the install_

1. You can close the Marketplace tab once the install is complete.

1. Add Version Assembly Task to the Build Definition. Click **CODE** to open the code hub. In the left menu, find the **Xamarin CI** build and click it. Then click the **Edit** link in the main view to open the definition.

1. Click **Add build step...** and scroll down in the Build tasks to the **Version Assembly** task. This task was installed when you installed the Marketplace extension. Click the **Add** button to add it to the definition before closing the Add Task dialog.

    ![Add the Version Assembly task](Images/vsts-build-add-versionassembly.png "Add the Version Assembly task")

    _Add the Version Assembly task_

1. Drag the **Version Assembly** task so that it is just below the first **Xamarin License** task. Expand the **Advanced** group and set the parameters as follows:
    - **Source Path**: `src/MyHealth.Client.Droid` (you can type this or browse to the folder)
    - **File Pattern**: `AndroidManifest.xml`
    - **Build Regex Pattern**: `(?:\d+\.\d+\.)(\d+)`
    - **Fail If No Match Found**: check this checkbox
    - **Regex Replace Pattern**: `versionCode="\d+`
    - **Prefix for Replacements**: `versionCode="`
    - **Build Regex Index**: `1`

    ![Version Assembly task settings](Images/vsts-build-versionassemblies-settings.png "Version Assembly task settings")

    _Version Assembly task settings_

    Let's examine these parameters:
    - **Source Path**: this is the path in which to find files to replace the version number. In your case, it's the **MyHealth.Client.Droid** project folder.
    - **File Pattern**: this specifies the file pattern for files in which to replace the version number - you set it to the Android manifest file.
    - **Build Regex Pattern**: This is going to extract the last number of a 3-digit version string. So if the version is **1.0.5**, then this Regex will extract the **5**.
    - **Fail If No Match Found**: If there is no match for the replacement Regex, then fail this task. This ensures that you know if the version number could not be successfully replaced.
    - **Regex Replace Pattern**: You set this to `versionCode="\d+`. The task will find `versionCode="1` in the target file (note that the ending `"` is not part of the pattern).
    - **Prefix for Replacements**: You set this to `versionCode="` since the replacement pattern will include the string `versionCode="` which we need to prefix to the  version number so that we retain the `versionCode="` string.
    - **Build Regex Index**: The Build Regex Pattern uses group notation. The 0th group ends up being the entire version number, while the 1st group is the actual number that should be used in the replacement - so you set this to 1.

1. The **Version Assembly** task assumes that the build number is of the form `1.0.1` - i.e. 3 digits separated by periods. However, the default format (as you will have seen in the builds that have run so far) is `yyyymmdd.r` where `r` is an incrementing integer. Click on the **General** tab. Change the **Build number format** field to `1.0$(rev:.r)`. This will produce the correct format for the version number.

    ![Set the Build number format](Images/vsts-build-number-format.png "Set the Build number format")

    _Set the Build number format_

1. Save the build. Once it is saved, click **Queue build** to queue a new build. Verify that the build number is `Build 1.0.1` and then immediately cancel the build. Since the Android Manifest is already set to `versionCode="1"`, you want to queue a build where the revision is not a 1. Immediately queue a new build - this time, the build number should be `1.0.2`.

    ![The build number](Images/vsts-build-102.png "The build number")

    _The build number_

1. Verify that the **apk** version code is set correctly. Once the build has completed, you can verify that the version code is set correctly. The first check is to look at the build log and make sure that the **Version Assembly** task has run correctly. Click on the **Version Assemblies** node in the task log to see the logs for that task.

    ![The Version Assemblies log](Images/vsts-build-versionassemblies-working.png "The Version Assemblies log")

    _The Version Assemblies log_

1. You should see the task has used version 2 and processed 1 file. To really be sure though, you must download the **apk** file. Click **Artifacts** and then click **Explore**. Expand the folders until you find the **myHealth.Client.Droid-Signed.apk** file and click **Download** to download it. Save it to **c:\buildworkshop**.

1. In Visual Studio, open the **Android Adb Command Prompt** from the toolbar.

    ![The Android Adb Command Prompt button](Images/vs-toolbar-adb.png "The Android Adb Command Prompt button")

    _The Android Adb Command Prompt button_

1. Enter the following commands.

    ```
    cd \buildworkshop
    "c:\Program Files (x86)\Android\android-sdk\build-tools\19.1.0\aapt.exe" dump badging myHealth.Client.Droid-Signed.apk
    ```

1. This dumps information about the **apk** file - scroll right to the top of the information and you should see that the version code is **2** (since the build is `1.0.2`).

    ![The apk version code](Images/apk-version-code.png "The apk version code")

    _The apk version code_

---

<a name="Summary" ></a>
## Summary ##

By completing this module, you should have:

- Created a Team Build with a CI trigger for the Xamarin solution
- Analyzed test results and code coverage for the build
- Logged a rich bug from test failure
- Triggered a build by committing the bug fix to the repo
- Improved the package versioning via a custom build task from the Marketplace
