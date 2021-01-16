# Initialization for every powershell session

$ErrorActionPreference = "Stop"

Write-Host "@StackBrains" # -ForegroundColor DarkCyan

$dir = (Get-Item $PSScriptRoot).FullName

$aliases = @{
    "tf"    = "terraform"
    "dn"    = "dotnet"
    "ng"    = "nuget"
    "watch" = "$dir/watch.ps1"
}

$aliases.GetEnumerator() | ForEach-Object { 
    Set-Alias $_.Key $_.Value 
}
