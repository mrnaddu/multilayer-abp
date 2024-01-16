using IVP.AuthServer.DomainShared;
using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;

namespace IVP.AuthServer.Domain;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AuthServerDomainSharedModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule)
)]
public class AuthServerDomainModule : AbpModule
{

}
