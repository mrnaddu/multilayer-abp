using Volo.Abp.DependencyInjection;

namespace IVP.DbMigrator.Data;

public class NullIVPDbSchemaMigrator : IIVPDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
