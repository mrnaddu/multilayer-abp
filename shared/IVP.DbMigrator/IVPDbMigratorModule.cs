using IVP.AdministrationService.ApplicationContracts;
using IVP.AdministrationService.EntityFrameworkCore;
using IVP.TenantService.ApplicationContracts;
using IVP.TenantService.EntityFrameworkCore;
using IVP.VerificationService.ApplicationContracts;
using IVP.VerificationService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace IVP.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(AdministrationServiceEntityFrameworkCoreModule),
    typeof(AdministrationServiceApplicationContractsModule),
    typeof(TenantServiceEntityFrameworkCoreModule),
    typeof(TenantServiceApplicationContractsModule),
    typeof(VerificationServiceEntityFrameworkCoreModule),
    typeof(VerificationServiceApplicationContractsModule)
)]
public class IVPDbMigratorModule : AbpModule
{
}
