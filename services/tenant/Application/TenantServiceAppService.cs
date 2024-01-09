using Volo.Abp.Application.Services;
using Volo.Abp.Localization;

namespace IVP.TenantService.Application;

public abstract class TenantServiceAppService : ApplicationService
{
    protected TenantServiceAppService()
    {
        LocalizationResource = typeof(TenantServiceResource);
        ObjectMapperContext = typeof(TenantServiceApplicationModule);
    }
}
