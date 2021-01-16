# Initialization for every powershell session

$ErrorActionPreference = "Stop"

Write-Host "@StackBrains"

$dir = (Get-Item $PSScriptRoot).FullName

$aliases = @{
    "tf"    = "terraform.exe"
    "dn"    = "dotnet.exe"
    "ng"    = "nuget.exe"
    "watch" = "$dir\watch.ps1"
}

$aliases.GetEnumerator() | ForEach-Object { 
    Set-Alias $_.Key $_.Value 
}
