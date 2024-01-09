$name = $args[0]

dotnet new web -n "$name.AuthServer" -o "apps\$name.AuthServer"
dotnet new classlib -n "$name.Shared.Hosting" -o "shared\$name.Shared.Hosting"
dotnet new console -n "$name.DbMigrator" -o "shared\$name.DbMigrator"
dotnet new webapi -n "$name.AdministrationService" -o services\administration
dotnet new webapi -n "$name.VerificationService" -o services\verification
dotnet new webapi -n "$name.TenantService" -o services\tenant
dotnet new sln -n "$name"
dotnet sln ".\$name.sln" add (Get-ChildItem -r **/*.csproj)