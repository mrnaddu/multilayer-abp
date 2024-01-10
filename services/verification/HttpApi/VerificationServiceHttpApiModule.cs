using IVP.VerificationService.ApplicationContracts;
using Localization.Resources.AbpUi;
using Onebill.VerificationService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace IVP.VerificationService.HttpApi;

[DependsOn(
    typeof(VerificationServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule)
)]
public class VerificationServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(VerificationServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<VerificationServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
