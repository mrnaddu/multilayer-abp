using IVP.AdministrationService.Shared;
using IVP.AuthServer.Shared;
using IVP.TenantService.Shared;
using IVP.VerificationService.Domain;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace IVP.VerificationService.EntityFrameworkCore;

[DependsOn(
    typeof(VerificationServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(TenantServiceSharedModule),
    typeof(AdministrationServiceSharedModule),
    typeof(AuthServerSharedModule)
)]
public class VerificationServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<VerificationServiceDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
    }
}
