# Initialization for every powershell session

$ErrorActionPreference = "Stop"

Write-Host "@StackBrains"

$tools = (Get-Item $PSScriptRoot).Parent.FullName

$aliases = @{
    "tf"             = "terraform.exe"
    "dn"             = "dotnet.exe"
    "ng"             = "nuget.exe"
    "watch"          = "$tools\common\watch.ps1"
    "dn-sln-add-all" = "$tools\dotnet\sln-add-all.ps1"
    "dn-add-tests"   = "$tools\dotnet\add-tests.ps1"
}

$aliases.GetEnumerator() | ForEach-Object { 
    Set-Alias $_.Key $_.Value 
}
