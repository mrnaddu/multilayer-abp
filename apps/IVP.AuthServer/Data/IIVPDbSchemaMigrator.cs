namespace IVP.AuthServer.Data;

public interface IIVPDbSchemaMigrator
{
    Task MigrateAsync();
}
