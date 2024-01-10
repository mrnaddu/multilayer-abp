using IVP.AuthServer.ApplicationContracts;
using IVP.AuthServer.Domain;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace IVP.AuthServer.Application;

[DependsOn(
    typeof(AuthServerDomainModule),
    typeof(AuthServerApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule)
)]
public class AuthServerApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AuthServerApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AuthServerApplicationModule>(validate: true);
        });
    }
}
