$ErrorActionPreference = "Stop"
function Abort { param ( $value )  If ($null -eq $value) { Write-Output("`n"); exit } }

$enter = Read-Host "`nLet's first configure the Postgres connection-string. Press enter to continue"; Abort $enter;

$connectionString = "Host={0};Port={1};Database={2};User ID={3};Password={4};"

$defaultIp = "localhost"
$defaultPort = 5432
$defaultUser = "postgres"
$defaultPassword = ""

if (!($ip = Read-Host "Enter server address [$defaultIp]")) { Abort $ip; $ip = $defaultIp } 
if (!($port = Read-Host "Enter server port [$defaultPort]")) { Abort $port; $port = $defaultPort } 
if (!($username = Read-Host "Enter Username [$defaultUser]")) { Abort $username; $username = $defaultUser }  
if (!($password = Read-Host "Enter Password [$defaultPassword]")) { Abort $password; $password = $defaultPassword } 


Get-ChildItem "**/appsettings.json" -recurse |
ForEach-Object { 
    $settings = Get-Content -Raw -LiteralPath $_.FullName | ConvertFrom-Json

    ForEach ($value in $settings.ConnectionStrings.PSObject.Properties) {
        $db = "Onebill" + ($value.Name);
        $string = $connectionString -f $ip, $port, $db, $username, $password
        $settings.ConnectionStrings.($value.Name) = $string
    }

    $settings | ConvertTo-Json -Depth 8 | Out-File $_.FullName
}
