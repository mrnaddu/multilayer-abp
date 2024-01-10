namespace IVP.AuthServer.Domain;

public static class AuthServerDbProperties
{
    public static string DbTablePrefix { get; set; } = "AuthServer";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AuthServer";
}
