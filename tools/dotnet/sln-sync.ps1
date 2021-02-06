$dir = Get-Item (Get-Location)

$solution = Join-Path -Path "$dir" -ChildPath ($dir.Name + ".sln")
if (!(Test-Path $solution)) {
    dn new sln
}

$projects = dotnet sln list `
| Where-Object { "$_".Contains(".csproj") } `
| ForEach-Object { Join-Path -Path "$dir" -ChildPath "$_" }

if ($null -eq $projects) {
    $projects = @()
}

$projects | ForEach-Object {
    if (!(Test-Path "$_")) {
        dn sln remove "$_"
    }
}

Get-ChildItem -Path $dir -Filter "*.csproj" -Recurse
| Where-Object { !($projects.Contains($_.FullName)) }
| ForEach-Object { dotnet sln add $_.FullName }

dn restore

Write-Host "Sucessfully updated solution $solution"