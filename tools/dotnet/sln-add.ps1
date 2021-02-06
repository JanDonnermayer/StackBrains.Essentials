param(
    [string]$filter = "*.csproj"
)

Get-ChildItem -Path (Get-Location) -Filter $filter -Recurse `
| ForEach-Object { dotnet sln add $_.FullName }