[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string]$context,
    [Parameter(Mandatory = $true)]
    [string]$command,
    [Parameter(ValueFromRemainingArguments)]
    [string[]]$arguments
)

function SyncSolution {
    param($dir)

    $solution = Join-Path -Path "$dir" -ChildPath ($dir.Name + ".sln")
    if (!(Test-Path $solution)) {
        dn new sln
    }

    [string[]]$projects = dotnet sln list `
    | Where-Object { "$_".Contains(".csproj") } `
    | ForEach-Object { Join-Path -Path "$dir" -ChildPath "$_" }

    
    foreach ($proj in $projects) {
        # Remove non existing
        if (!(Test-Path "$proj")) {
            dn sln remove "$proj"
            continue
        }
        # Add dependencies
        foreach ($otherProj in $projects) {
            $projName = (Split-Path $proj -Leaf).Split(".")[0];
            $otherProjName = (Split-Path $otherProj -Leaf).Split(".")[0];

            if ("$otherProjName" -eq "$projName") {
                continue
            }

            if ("$otherProjName".StartsWith("$projName")) {
                dotnet add "$proj" reference "$otherProj"
            }
        }
    }

    Get-ChildItem -Path $dir -Filter "*.csproj" -Recurse
    | Where-Object { !($projects.Contains($_.FullName)) }
    | ForEach-Object { dotnet sln add $_.FullName }

    dotnet restore
    
    Write-Host "Sucessfully updated solution $solution"
}

if ($context -eq "sln") {
    if ($command -eq "sync") {
        SyncSolution(Get-Item (Get-Location))
    }
}

if ($context -eq "add") {
    if ($command -eq "ref") {
        dotnet add reference $arguments
    }
    if ($command -eq "pkg") {
        dotnet add package $arguments
    }
    if ($command -eq "brains") {
        dotnet add package StackBrains.Essentials
    }
}