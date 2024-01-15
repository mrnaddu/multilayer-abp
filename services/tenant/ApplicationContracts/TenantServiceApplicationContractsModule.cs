using IVP.TenantService.DomainShared;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace IVP.TenantService.ApplicationContracts;

[DependsOn(
    typeof(TenantServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpTenantManagementApplicationContractsModule)
)]
public class TenantServiceApplicationContractsModule : AbpModule
{

}
