$dir = Get-Location
$type = "csproj"

function CreateTestProject {
    param(  $srcFile )

    $srcDir = $srcFile.Directory
    
    $testDir = "$srcDir.Test" -replace "src". "test"

    if (Test-Path $testDir) {
        Write-Host "Skipping existing: $testDir"
        return
    } 
    New-Item -Path $testDir -ItemType "directory"

    Set-Location $testDir
    dotnet new nunit --no-restore
    dotnet add reference "$srcDir" 
    Set-Location $dir
}

$jobs = Get-ChildItem -Path "$dir/src" -Filter "*.$type" -Recurse `
| ForEach-Object { Start-Job -ScriptBlock ${function:CreateTestProject} -ArgumentList $_ } 

$jobs | ForEach-Object {
    Receive-Job $_ -ErrorAction "Continue"
}