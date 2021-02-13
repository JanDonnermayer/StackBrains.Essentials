# Initialization for every powershell session

$ErrorActionPreference = "Stop"

Write-Host "@StackBrains"

$tools = (Get-Item $PSScriptRoot).Parent.FullName

$aliases = @{
    "tf" = "terraform.exe"
    "dn" = "dotnet.exe"
}

$prefixes = @{
    "common" = ""
    "dotnet" = "dn-"
}

$prefixes.GetEnumerator() | ForEach-Object {
    $subDir = $_.Key
    $prefix = $_.Value
    Get-ChildItem "$tools/$subDir" | ForEach-Object {
        $name = $_.Name -replace ".ps1", ""
        $aliases.Add("$prefix" + "$name", $_.FullName)
    }
}

$aliases.GetEnumerator() | ForEach-Object { 
    Set-Alias $_.Key $_.Value 
}
