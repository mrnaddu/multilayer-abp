using IVP.TenantService.ApplicationContracts;
using IVP.TenantService.Localization;
using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace IVP.TenantService.HttpApi;

[DependsOn(
    typeof(TenantServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpTenantManagementHttpApiModule)
)]
public class TenantServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TenantServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TenantServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
