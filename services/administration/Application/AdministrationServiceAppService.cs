using Volo.Abp.Application.Services;
using Volo.Abp.Localization;

namespace IVP.AdministrationService.Application;

public abstract class AdministrationServiceAppService : ApplicationService
{
    protected AdministrationServiceAppService()
    {
        LocalizationResource = typeof(AdministrationServiceResource);
        ObjectMapperContext = typeof(AdministrationServiceApplicationModule);
    }
}
