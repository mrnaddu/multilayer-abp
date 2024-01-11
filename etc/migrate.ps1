Invoke-Expression "./etc/setup-infra.ps1"

Set-Location "./shared/IVP.DbMigrator"
dotnet run 
Set-Location "../.."