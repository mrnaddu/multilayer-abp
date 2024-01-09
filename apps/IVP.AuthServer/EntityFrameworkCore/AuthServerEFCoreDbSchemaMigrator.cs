using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace IVP.AuthServer.EntityFrameworkCore;

public class AuthServerEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public AuthServerEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the CoreServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AuthServerDbContext>()
            .Database
            .MigrateAsync();
    }
}
