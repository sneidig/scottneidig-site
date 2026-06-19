# Builds the React frontend, drops it into the server's wwwroot, and publishes the
# ASP.NET Core app to server/publish/ — the folder you upload to the host.
#
# Usage (from the repo root):  ./publish.ps1

$ErrorActionPreference = 'Stop'

Write-Host "1/3  Building React frontend..." -ForegroundColor Cyan
Push-Location client
npm install
npm run build
Pop-Location

Write-Host "2/3  Copying build into server/wwwroot..." -ForegroundColor Cyan
$wwwroot = "server/wwwroot"
if (Test-Path $wwwroot) { Remove-Item $wwwroot -Recurse -Force }
New-Item -ItemType Directory -Path $wwwroot | Out-Null
Copy-Item "client/dist/*" $wwwroot -Recurse

Write-Host "3/3  Publishing ASP.NET Core app..." -ForegroundColor Cyan
Push-Location server
dotnet publish -c Release -o ./publish
Pop-Location

Write-Host ""
Write-Host "Done. Upload the contents of server/publish/ to the host." -ForegroundColor Green
