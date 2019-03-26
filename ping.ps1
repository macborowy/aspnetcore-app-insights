[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

$i = 0

while ($i -lt 1000) {
    $hour_minute = Get-Date -UFormat "%H%M"
    $url = 'https://webapp-tkotobafsw6aq.azurewebsites.net/?i=' + $hour_minute + $i
    # $url = 'https://localhost:5001/?i=' + $hour_minute + $i

    Write-Host $url

    $response = Invoke-WebRequest $url

    $i = $i + 1

    sleep 3
}