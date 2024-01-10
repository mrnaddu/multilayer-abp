using IVP.AdministrationService.Domain;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace IVP.AdministrationService.EntityFrameworkCore;

public class AdministrationServiceMigrationsDbContextFactory : IDesignTimeDbContextFactory<AdministrationServiceDbContext>
{
    public AdministrationServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        var connectionString = configuration.GetConnectionString(AdministrationServiceDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<AdministrationServiceDbContext>()
            .UseNpgsql(connectionString);

        return new AdministrationServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName,"IVP.AdministrationService");

        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
