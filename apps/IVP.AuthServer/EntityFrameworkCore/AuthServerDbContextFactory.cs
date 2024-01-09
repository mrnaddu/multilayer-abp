using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace IVP.AuthServer.EntityFrameworkCore;

public class AuthServerDbContextFactory : IDesignTimeDbContextFactory<AuthServerDbContext>
{
    public AuthServerDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AuthServerDbContext>()
            .UseNpgsql(configuration.GetConnectionString("AuthServer"));

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
