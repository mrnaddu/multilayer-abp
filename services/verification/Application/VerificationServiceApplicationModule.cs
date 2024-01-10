using IVP.VerificationService.ApplicationContracts;
using IVP.VerificationService.Domain;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace IVP.VerificationService.Application;

[DependsOn(
    typeof(VerificationServiceDomainModule),
    typeof(VerificationServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
)]
public class VerificationServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<VerificationServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<VerificationServiceApplicationModule>(validate: true);
        });
    }
}
