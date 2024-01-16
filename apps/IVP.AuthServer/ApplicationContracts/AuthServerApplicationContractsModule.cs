using IVP.AuthServer.DomainShared;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace IVP.AuthServer.ApplicationContracts;

[DependsOn(
    typeof(AuthServerDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule)
)]
public class AuthServerApplicationContractsModule : AbpModule
{

}
