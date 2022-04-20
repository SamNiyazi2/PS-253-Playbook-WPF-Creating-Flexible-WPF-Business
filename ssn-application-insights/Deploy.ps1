
# 04/18/2022 05:26 am - SSN

param(
    
	
    [parameter(Mandatory)]
    $version,

    [parameter(Mandatory)]
    $option,

    [switch]
    $override = $false
    
)

$ErrorActionPreference = "stop"


$fullProjectNameSpec = $psscriptroot + "\*.csproj"

. "C:\Sams\PS\NuGet\NuGet_Deploy_Framework_4_7_2.ps1" -fullProjectNameSpec $fullProjectNameSpec -version $version   -option $option   -override:$override 


