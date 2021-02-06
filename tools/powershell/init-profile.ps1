# Initialization for every powershell session

$ErrorActionPreference = "Stop"

Write-Host "@StackBrains"

$tools = (Get-Item $PSScriptRoot).Parent.FullName

$aliases = @{
    "tf" = "terraform.exe"
    "dn" = "dotnet.exe"
    "ng" = "nuget.exe"
}

$prefixMap = @{
    "common" = ""
    "dotnet" = "dn-"
}

$prefixMap.GetEnumerator() | ForEach-Object {
    $path = $_.Key
    $prefix = $_.Value
    Get-ChildItem "$tools/$path" | ForEach-Object {
        $name = $_.Name -replace ".ps1", ""
        $aliases.Add("$prefix" + "$name", $_.FullName)
    }
}

$aliases.GetEnumerator() | ForEach-Object { 
    Set-Alias $_.Key $_.Value 
}
