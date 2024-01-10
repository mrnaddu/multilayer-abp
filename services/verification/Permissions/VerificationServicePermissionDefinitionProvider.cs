using Onebill.VerificationService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace IVP.VerificationService.Permissions;

public class VerificationServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(VerificationServicePermissions.GroupName, L("Permission:VerificationService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<VerificationServiceResource>(name);
    }
}
