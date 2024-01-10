using IVP.TenantService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;

namespace IVP.TenantService.HttpApi;

public abstract class TenantServiceController : AbpControllerBase
{
    protected TenantServiceController()
    {
        LocalizationResource = typeof(TenantServiceResource);
    }
}
