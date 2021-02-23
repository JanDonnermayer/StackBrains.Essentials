[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string]$context,
    [Parameter(Mandatory = $true)]
    [string]$command,
    [Parameter(ValueFromRemainingArguments)]
    [string[]]$arguments
)

$location = Get-Item(Get-Location)

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
        # Remove non existing projects
        if (!(Test-Path "$proj")) {
            dn sln remove "$proj"
            continue
        }
        # Add references based on names:
        # If a projects starts with the name of another project, 
        # reference that project
      
        # foreach ($otherProj in $projects) {
        #     $projName = (Split-Path $proj -Leaf).Split(".")[0];
        #     $otherProjName = (Split-Path $otherProj -Leaf).Split(".")[0];
        #     if ("$projName" -eq "$otherProjName") {
        #         continue
        #     }
        #     if ("$projName".StartsWith("$otherProjName")) {
        #         dotnet add "$proj" reference "$otherProj"
        #     }
        # }
        
    }

    Get-ChildItem -Path $dir -Filter "*.csproj" -Recurse
    | Where-Object { !($projects.Contains($_.FullName)) }
    | ForEach-Object { dotnet sln add $_.FullName }

    dotnet restore
    
    Write-Host "Sucessfully updated solution $solution"
}

if ($context -eq "sln") {
    if ($command -eq "sync") {
        SyncSolution($location)
        return
    }
}

if ($context -eq "add") {
    if ($command -eq "ref") {
        dotnet add "$location" reference "$arguments"
        return
    }
    if ($command -eq "pkg") {
        dotnet add "$location" package "$arguments"
        return
    }
    if ($command -eq "brains") {
        dotnet add "$location" package StackBrains.Essentials
        return
    }
}

if ($context -eq "new") {
    if ($command -eq "clslib") {
        dotnet add "$location" reference "$arguments"
        return
    }
}
