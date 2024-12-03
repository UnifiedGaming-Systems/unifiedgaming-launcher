#Requires -Version 7

param(
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    [string]$OutputPath = (Join-Path $PWD "$($Configuration)SDK"),
    [switch]$SkipBuild = $false,
    [string]$LocalPublish
)

$ErrorActionPreference = "Stop"
& .\common.ps1

# -------------------------------------------
#            Compile SDK
# -------------------------------------------
if (!$SkipBuild)
{
    New-EmptyFolder $OutputPath
    $project = Join-Path $pwd "..\source\UnifiedGL\UnifiedGL.SDK.csproj"
    $msbuildPath = Get-MsBuildPath
    $arguments = "`"$project`" /p:OutputPath=`"$outputPath`";Configuration=$configuration /t:Build"
    $compilerResult = StartAndWait $msbuildPath $arguments
    if ($compilerResult -ne 0)
    {
        throw "Build failed."
    }
}

# -------------------------------------------
#            Create NUGET
# -------------------------------------------
$version = (Get-ChildItem (Join-Path $OutputPath "UnifiedGL.SDK.dll")).VersionInfo.ProductVersion
$version = $version -replace "\.0$", ""
$spec = Get-Content "UnifiedGL.nuspec"
$spec = $spec -replace "{Version}", $version
$spec = $spec -replace "{OutDir}", $OutputPath
$specFile = "nuget.nuspec"

try
{
    $spec | Out-File $specFile
    $packageRes = Invoke-Nuget "pack $specFile -OutputDirectory $OutputPath"
    if ($packageRes -ne 0)
    {
        throw "Nuget packing failed."
    }
}
finally
{
    Remove-Item $specFile -EA 0
}

if ($LocalPublish)
{
    Invoke-Nuget "init `"$pwd`" `"$LocalPublish`""
}

return $true