Invoke-Expression "./etc/setup-infra.ps1"

Set-Location "./shared/host/Onebill.DbMigrator"
dotnet run 
Set-Location "../../.."