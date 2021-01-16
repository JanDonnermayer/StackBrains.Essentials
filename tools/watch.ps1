# This script provides hot-reload functionality for the specified tool,
# called with the specified arguments,
# by monitoring changes to files in a specified set of directories.

param (
    [Parameter(Mandatory = $true)]
    [String]$tool,
    [Parameter(Mandatory = $true)]
    [String[]]$toolArgs,
    [Parameter(Mandatory = $true)]
    [String[]]$watchDirs,
    [Parameter()]
    [String]$toolWorkDir = (Get-Location)
)

$ErrorActionPreference = "Stop"

[System.EventArgs]$Global:event = $null

function PrintStatus {
    param (
        $outer,
        $color1,
        $inner,
        $color2
    )
    $tab = [char]9

    Write-Host ""
    Write-Host "$tab"           -NoNewline
    Write-Host "["              -NoNewline -ForegroundColor $color2 
    Write-Host " $outer "       -NoNewline -ForegroundColor $color1 
    Write-Host "]"              -NoNewline -ForegroundColor $color2 
    Write-Host "$tab $inner"    -NoNewline -ForegroundColor $color2 
    Write-Host ""
    Write-Host ""
}

function RegisterWatchers {
    PrintStatus '***' Yellow ("Watching: $watchDirs") DarkGray

    $Action = {
        param($s, $e)
        $Global:event = $e
    }

    foreach ($dir in $watchDirs) {
        if (-Not (Test-Path $dir)) {
            continue
        }
    
        $fsw = New-Object System.IO.FileSystemWatcher;
        $fsw.Path = $dir;
        $fsw.Filter = "*";
        $fsw.IncludeSubdirectories = $true;
        $fsw.EnableRaisingEvents = $true;
    
        $handlers = . {
            Register-ObjectEvent -InputObject $fsw -EventName Changed -Action $Action
            Register-ObjectEvent -InputObject $fsw -EventName Created -Action $Action
            Register-ObjectEvent -InputObject $fsw -EventName Deleted -Action $Action
            Register-ObjectEvent -InputObject $fsw -EventName Renamed -Action $Action
        }
    }
}

function StartProcess {
    PrintStatus '>>>' Green "Starting" DarkGray

    $psi = New-Object System.Diagnostics.ProcessStartInfo;
    $psi.FileName = "$tool"
    $psi.Arguments = "$toolArgs"
    $psi.WorkingDirectory = "$toolWorkDir"
    $psi.UseShellExecute = $false; #start the process from it's own executable file
    $psi.RedirectStandardInput = $false; #enable the process to read from standard input

    try {
        $proc = [System.Diagnostics.Process]::Start($psi);
        return $proc.Id
    }
    catch {
        [System.Threading.Thread]::Sleep(2000)
        return StartProcess
    }
    #PrintStatus "↑↑↑" Green "Started" DarkGray
}


function StopProcess {
    param (
        [int] $id 
    )

    PrintStatus '<<<' Red "Stopping" DarkGray
    
    $proc = Get-Process -Id $id -ErrorAction SilentlyContinue
    if (!$proc) {
        return
    } 

    Stop-Process -Id $id
}


try {
    RegisterWatchers
    $procId = StartProcess

    do {
        $ev = $Global:event
        if ($null -ne $ev) {
            $name = $ev.Name
            PrintStatus '***' Yellow ("Detected Change: $name") DarkGray
            
            StopProcess $procId
            Wait-Event -Timeout 3
            $procId = StartProcess
            
            $Global:event = $null
        }
    } while ($true)
    
}
finally {
    foreach ($fsw in $fsws) {
        $fsw.Dispose()
    }
}



