
$localProfile = "$env:USERPROFILE/documents/powershell/microsoft.powershell_profile.ps1"

$addedProfile = Join-Path -Path (Get-Item $PSScriptRoot).FullName -ChildPath "init-profile.ps1"
$initProfile = ". $addedProfile"

function WriteProfile {
    Write-Host "Adding ""$addedProfile"" to ""$localProfile""."
    "$initProfile" >> $localProfile
}

if (!(Test-Path $localProfile)) {
    WriteProfile
    return
} 

if (!(Get-Content $localProfile)?.Contains($initProfile)) {
    WriteProfile
    return
}
