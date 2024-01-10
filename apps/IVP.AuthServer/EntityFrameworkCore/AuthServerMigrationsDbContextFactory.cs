using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using IVP.AuthServer.Domain;

namespace IVP.AuthServer.EntityFrameworkCore;

public class AuthServerMigrationsDbContextFactory : IDesignTimeDbContextFactory<AuthServerDbContext>
{
    public AuthServerDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var connectionString = configuration.GetConnectionString(AuthServerDbProperties.ConnectionStringName);

        var builder = new DbContextOptionsBuilder<AuthServerDbContext>()
            .UseNpgsql(connectionString);

        return new AuthServerDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName, "IVP.AuthServer");

        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
