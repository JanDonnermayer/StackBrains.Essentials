param (
    [Parameter(Mandatory = $true)]
    [Int32]$sizeMb,
    [Parameter()]
    [Int32]$count = 1,
    [Parameter()]
    [Int32]$depth = 1
)

for ($i = 1; $count -ge $i; $i++) {
    $id = New-Guid
    $name = "(" + "$sizeMb".PadLeft(7, "0") + ")(" + "$id".Substring(0, 8) + ").dummy";
    $size = $sizeMb * 1024 * 1024
    fsutil file createnew "$name" "$size"
}
