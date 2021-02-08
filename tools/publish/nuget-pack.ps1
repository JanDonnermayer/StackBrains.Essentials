$root = git rev-parse --show-toplevel

$nuspec = "$root/.nuget/StackBrains.Essentials.nuspec"
$csproj = "$root/src/StackBrains.Essentials/StackBrains.Essentials.csproj"

dotnet pack "$csproj" -p:NuspecFile="$nuspec"