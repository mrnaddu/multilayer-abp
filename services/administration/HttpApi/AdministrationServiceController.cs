using IVP.AdministrationService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IVP.AdministrationService.HttpApi;

public abstract class AdministrationServiceController : AbpControllerBase
{
    protected AdministrationServiceController()
    {
        LocalizationResource = typeof(AdministrationServiceResource);
    }
}
