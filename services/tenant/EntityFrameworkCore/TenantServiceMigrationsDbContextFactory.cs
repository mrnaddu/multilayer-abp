using IVP.TenantService.Domain;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace IVP.TenantService.EntityFrameworkCore;

public class TenantServiceMigrationsDbContextFactory : IDesignTimeDbContextFactory<TenantServiceDbContext>
{
    public TenantServiceDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString(TenantServiceDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<TenantServiceDbContext>()
            .UseNpgsql(connectionString);

        return new TenantServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
