namespace IVP.AdministrationService.Domain;

public static class AdministrationServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "AdministrationService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AdministrationService";
}
