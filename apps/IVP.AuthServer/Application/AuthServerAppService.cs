using IVP.AuthServer.Localization;
using Volo.Abp.Application.Services;

namespace IVP.AuthServer.Application;

public abstract class AuthServerAppService : ApplicationService
{
    protected AuthServerAppService()
    {
        LocalizationResource = typeof(AuthServerResource);
        ObjectMapperContext = typeof(AuthServerApplicationModule);
    }
}
