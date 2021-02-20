[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string]$command,
    [Parameter(Mandatory = $true)]
    [string]$package
)

if (!"$package".EndsWith(".spkg")) {
    throw "Only support .spkg files!"
}

if ($command -eq "install") {
    InstallServicePackage $package
    return
}

throw "unsupported command $command"

function New-TemporaryDirectory {
    $parent = [System.IO.Path]::GetTempPath()
    [string] $name = [System.Guid]::NewGuid()
    return New-Item -ItemType Directory -Path (Join-Path $parent $name)
}

function InstallServicePackage {
    param ( $package )
   
    $src = New-TemporaryDirectory
    
    Expand-Archive "$package" "$src"
    
    $manifest = "$src/manifest.json" | Get-Content | ConvertFrom-Json -AsHashtable
    $packageDirectory = "$src/package"
    $executableName = $manifest["service"]["exe"];
    $serviceName = $manifest["service"]["name"];
    $serviceDirectory = $manifest["host"]["directory"];
    
    if (!(Test-Path $serviceDirectory)) {
        New-Item -Path "$serviceDirectory" -ItemType "directory"
    }
    
    $service = Get-Service $serviceName -ErrorAction SilentlyContinue
    
    if ($service) {
        Write-Host "Stopping service $serviceName"
        Stop-Service $service
    }
    
    Write-Host "Copying files to service-directory"
    Copy-Item "$packageDirectory" $serviceDirectory -Recurse -Force
     
    if (!$service) {
        Write-Host "Creating service $serviceName"
        $binPath = (Get-ChildItem –Path $serviceDirectory -Filter "$executableName" -File)[0].FullName
        $service = New-Service -Name $serviceName -BinaryPathName $binPath
    }
    
    Write-Host "Starting service $serviceName"
    Start-Service -Name $serviceName
}

