using IVP.VerificationService.DomainShared;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace IVP.VerificationService.Domain;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(VerificationServiceDomainSharedModule)
)]
public class VerificationServiceDomainModule : AbpModule
{
}
