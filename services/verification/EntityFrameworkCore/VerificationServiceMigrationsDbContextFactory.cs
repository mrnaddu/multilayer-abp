using IVP.VerificationService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IVP.VerificationService.EntityFrameworkCore;

public class VerificationServiceMigrationsDbContextFactory : IDesignTimeDbContextFactory<VerificationServiceDbContext>
{
    public VerificationServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        var connectionString = configuration.GetConnectionString(VerificationServiceDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<VerificationServiceDbContext>()
            .UseNpgsql(connectionString);

        return new VerificationServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, $"IVP.VerificationService");

        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
