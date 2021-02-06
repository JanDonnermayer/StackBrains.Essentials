$dir = Get-Item (Get-Location)

$solution = Join-Path -Path "$dir" -ChildPath ($dir.Name + ".sln")
if (!(Test-Path $solution)) {
    Write-Warning "No solution found!"
    return
}

dotnet sln list `
| Where-Object { "$_".Contains(".csproj") }
| ForEach-Object { Join-Path -Path "$dir" -ChildPath "$_" }
