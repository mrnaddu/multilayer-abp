using IVP.VerificationService.DomainShared;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace IVP.VerificationService.ApplicationContracts;

[DependsOn(
    typeof(VerificationServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class VerificationServiceApplicationContractsModule : AbpModule
{
}
