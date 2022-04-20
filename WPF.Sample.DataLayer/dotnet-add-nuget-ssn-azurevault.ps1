

# 04/13/2022 08:11 am - SSN 
 

$erroractionpreference = "stop"

$error.clear()
0..10|%{""}
get-date 
""


$projectName = "$psscriptroot\WPF.Sample.datalayer.csproj"

$packageName = "ssn_AzureKeyVault"


. C:\Sams\PS\NuGet\add-nuget-package-util.ps1

 add-nugetPackage -projectName  $projectName -packageName $packageName


