using IVP.TenantService.Domain;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace IVP.TenantService.EntityFrameworkCore;

public class TenantServiceMigrationsDbContextFactory : IDesignTimeDbContextFactory<TenantServiceDbContext>
{
    public TenantServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        var connectionString = configuration.GetConnectionString(TenantServiceDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<TenantServiceDbContext>()
            .UseNpgsql(connectionString);

        return new TenantServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, "IVP.TenantService");

        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
