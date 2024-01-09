using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Ui.Branding;

namespace IVP.AuthServer;

[Dependency(ReplaceServices = true)]
public class IVPBrandingProvider : DefaultBrandingProvider
{
    private readonly ICurrentTenant _currentTenant;
    public IVPBrandingProvider(
        ICurrentTenant currentTenant) => _currentTenant = currentTenant;
    public override string AppName => _currentTenant.Name ?? "IVP";
}
