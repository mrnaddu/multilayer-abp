using Onebill.VerificationService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IVP.VerificationService.HttpApi;

public abstract class VerificationServiceController : AbpControllerBase
{
    protected VerificationServiceController()
    {
        LocalizationResource = typeof(VerificationServiceResource);
    }
}
