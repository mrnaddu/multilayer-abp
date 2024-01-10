using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using IVP.AuthServer.Domain;

namespace IVP.AuthServer.EntityFrameworkCore;

public class AuthServerMigrationsDbContextFactory : IDesignTimeDbContextFactory<AuthServerDbContext>
{
    public AuthServerDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString(AuthServerDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<AuthServerDbContext>()
            .UseNpgsql(connectionString);

        return new AuthServerDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
