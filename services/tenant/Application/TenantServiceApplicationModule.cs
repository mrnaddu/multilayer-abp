using IVP.TenantService.ApplicationContracts;
using IVP.TenantService.Domain;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace IVP.TenantService.Application;

[DependsOn(
    typeof(TenantServiceDomainModule),
    typeof(TenantServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpTenantManagementApplicationModule)
    )]
public class TenantServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TenantServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TenantServiceApplicationModule>(validate: true);
        });
    }
}
