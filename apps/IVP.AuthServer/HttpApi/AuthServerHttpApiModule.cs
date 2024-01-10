using IVP.AuthServer.ApplicationContracts;
using IVP.AuthServer.Localization;
using Localization.Resources.AbpUi;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace IVP.AuthServer.HttpApi;

[DependsOn(
    typeof(AuthServerApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule)
)]
public class AuthServerHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AuthServerHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AuthServerResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
