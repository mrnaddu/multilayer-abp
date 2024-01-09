using Volo.Abp.Authorization.Permissions;

namespace IVP.TenantService.Permissions;

public class TenantServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TenantServicePermissions.GroupName, L("Permission:TenantService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TenantServiceResource>(name);
    }
}
