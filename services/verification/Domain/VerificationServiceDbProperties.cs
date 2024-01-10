namespace IVP.VerificationService.Domain;

public static class VerificationServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "VerificationService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "VerificationService";
}
