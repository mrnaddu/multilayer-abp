using IVP.AuthServer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IVP.AuthServer.HttpApi;

public abstract class AuthServerController : AbpControllerBase
{
    protected AuthServerController()
    {
        LocalizationResource = typeof(AuthServerResource);
    }
}
