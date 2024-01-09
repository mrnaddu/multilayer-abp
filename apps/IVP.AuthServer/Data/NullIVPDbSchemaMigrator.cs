using Volo.Abp.DependencyInjection;

namespace IVP.AuthServer.Data;

public class NullIVPDbSchemaMigrator
    : IIVPDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
