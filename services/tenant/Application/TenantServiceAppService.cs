using IVP.TenantService.Localization;
using Volo.Abp.Application.Services;

namespace IVP.TenantService.Application;

public abstract class TenantServiceAppService : ApplicationService
{
    protected TenantServiceAppService()
    {
        LocalizationResource = typeof(TenantServiceResource);
        ObjectMapperContext = typeof(TenantServiceApplicationModule);
    }
}
