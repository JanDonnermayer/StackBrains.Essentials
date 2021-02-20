# Initialization for every powershell session

$ErrorActionPreference = "Stop"

Write-Host "@StackBrains"

$tools = (Get-Item $PSScriptRoot).Parent.FullName

$aliases = @{
    "tf" = "terraform.exe"
    "dn" = "dotnet.exe"
}

$toolDirs = @(
    "common" 
    "dotnet"
)

$toolDirs | ForEach-Object {
    Get-ChildItem "$tools/$_" | ForEach-Object {
        $name = $_.Name -replace ".ps1", ""
        $aliases.Add("$name", $_.FullName)
    }
}

$aliases.GetEnumerator() | ForEach-Object { 
    Set-Alias $_.Key $_.Value 
}
