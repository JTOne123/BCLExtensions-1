choco install psake
choco install pscx
choco install GitVersion.Portable
choco install nuget.CommandLine

$pscxPath = "C:\Program Files (x86)\PowerShell Community Extensions\Pscx3\Pscx";

if (-not (Test-Path $pscxPath))
{
    $pscxPath = $null;
    Write-Host "Searching for the pscx powershell module.";
    $pscxPath = (Get-ChildItem -Path "C:\Program Files\" -Filter "pscx.dll" -Recurse -ErrorAction SilentlyContinue).FullName;
    if (!$pscxPath) { $pscxPath = (Get-ChildItem -Path "C:\Program Files (x86)\" -Filter "pscx.dll" -Recurse -ErrorAction SilentlyContinue).FullName; }
    $pscxPath = Split-Path $pscxPath;
    Write-Host "Found it at " + $pscxPath;
}

$env:PSModulePath = $env:PSModulePath + ";" + $pscxPath;
