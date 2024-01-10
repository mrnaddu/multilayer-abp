namespace IVP.DbMigrator.Data;

public interface IIVPDbSchemaMigrator
{
    Task MigrateAsync();
}
