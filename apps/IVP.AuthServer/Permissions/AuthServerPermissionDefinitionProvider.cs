using IVP.AuthServer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace IVP.AuthServer.Permissions;

public class AuthServerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AuthServerPermissions.GroupName, L("Permission:AuthServer"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AuthServerResource>(name);
    }
}
