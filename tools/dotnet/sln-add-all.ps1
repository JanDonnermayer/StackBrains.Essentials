# Parameter help description
$dir = Get-Location

Get-ChildItem -Path $dir -Filter "*.csproj" -Recurse | ForEach-Object {
    dotnet sln add $_.FullName
}