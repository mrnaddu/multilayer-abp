using Volo.Abp.Reflection;

namespace IVP.VerificationService.Permissions;

public class VerificationServicePermissions
{
    public const string GroupName = "VerificationService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(VerificationServicePermissions));
    }
}
