using Onebill.VerificationService.Localization;
using Volo.Abp.Application.Services;

namespace IVP.VerificationService.Application;

public abstract class VerificationServiceAppService : ApplicationService
{
    protected VerificationServiceAppService()
    {
        LocalizationResource = typeof(VerificationServiceResource);
        ObjectMapperContext = typeof(VerificationServiceApplicationModule);
    }
}

