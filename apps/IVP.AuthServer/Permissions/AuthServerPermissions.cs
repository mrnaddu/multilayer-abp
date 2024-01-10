using Volo.Abp.Reflection;

namespace IVP.AuthServer.Permissions;

public class AuthServerPermissions
{
    public const string GroupName = "AuthServer";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AuthServerPermissions));
    }
}
