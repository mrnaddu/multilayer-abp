using IVP.VerificationService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IVP.VerificationService.EntityFrameworkCore;

public class VerificationServiceMigrationsDbContextFactory : IDesignTimeDbContextFactory<VerificationServiceDbContext>
{
    public VerificationServiceDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString(VerificationServiceDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<VerificationServiceDbContext>()
            .UseNpgsql(connectionString);

        return new VerificationServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
