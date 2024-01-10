using IVP.TenantService.DomainShared;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace IVP.TenantService.Domain;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpTenantManagementDomainModule),
    typeof(TenantServiceDomainSharedModule)
)]
public class TenantServiceDomainModule : AbpModule
{

}
