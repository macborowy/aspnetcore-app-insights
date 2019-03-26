$armTemplate = "azuredeploy.json"
$resourceGroup = "web_app_insights"
$webProjectPath = "src\WebAppInsigths.Web"
$publishFolder = Join-Path -Path $PSScriptRoot -ChildPath "$webProjectPath/publish/artifacts"
$destination = Join-Path -Path $PSScriptRoot -ChildPath "$webProjectPath/publish/artifacts.zip"

# stopwatch for debugging
$stopwatch =  [system.diagnostics.stopwatch]::StartNew()

# deploy ARM template

$webAppName = az group deployment create -g $resourceGroup --template-file $armTemplate --verbose --query "properties.outputs.webAppName.value"

$stopwatch

# publish artifacts & make *.zip package

if (Test-Path $publishFolder) { Remove-Item -Path $publishFolder -Recurse -Force }

dotnet publish $webProjectPath -c release -o $publishFolder

if (Test-Path $destination) { Remove-Item -Path $destination -Force }
Add-Type -AssemblyName "system.io.compression.filesystem"
[io.compression.zipfile]::CreateFromDirectory($publishFolder, $destination)

if (Test-Path $publishFolder) { Remove-Item -Path $publishFolder -Recurse -Force }

# deploy to app service

az webapp deployment source config-zip -g $resourceGroup -n $webAppName --src $destination

# open deployed website in default browser

$site = az webapp show -n $webAppName -g $resourceGroup --query "defaultHostName" -o tsv
Start-Process https://$site

$stopwatch.Stop();
$stopwatch