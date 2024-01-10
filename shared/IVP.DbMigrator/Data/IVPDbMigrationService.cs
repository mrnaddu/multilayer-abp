using IVP.AdministrationService.EntityFrameworkCore;
using IVP.AuthServer.EntityFrameworkCore;
using IVP.TenantService.EntityFrameworkCore;
using IVP.VerificationService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace IVP.DbMigrator.Data;

public class IVPDbMigrationService : ITransientDependency
{

    public ILogger<IVPDbMigrationService> Logger { get; set; }

    private readonly IDataSeeder _dataSeeder;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    public IVPDbMigrationService(
         IDataSeeder dataSeeder,
        ITenantRepository tenantRepository,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _dataSeeder = dataSeeder;
        _tenantRepository = tenantRepository;
        _currentTenant = currentTenant;
        _unitOfWorkManager = unitOfWorkManager;
        Logger = NullLogger<IVPDbMigrationService>.Instance;
    }

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Started database migrations...");

        await MigrateHostAsync(cancellationToken);
        await MigrateTenantsAsync(cancellationToken);
        Logger.LogInformation("Migration completed!");
    }

    private async Task MigrateHostAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Migrating Host side...");
        await MigrateDatabaseSchemaAsync(null, cancellationToken);
        await SeedDataAsync();

        Logger.LogInformation($"Successfully completed host database migrations.");
    }


    public async Task MigrateTenantsAsync(CancellationToken cancellationToken)
    {
        var tenants = await _tenantRepository.GetListAsync(includeDetails: true);

        var migratedDatabaseSchemas = new HashSet<string>();
        foreach (var tenant in tenants)
        {
            using (_currentTenant.Change(tenant.Id))
            {
                if (tenant.ConnectionStrings.Any())
                {
                    var tenantConnectionStrings = tenant.ConnectionStrings
                        .Select(x => x.Value)
                        .ToList();

                    if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                    {
                        Logger.LogInformation($"Migrating tenant database: {tenant.Name} ({tenant.Id})");
                        await MigrateDatabaseSchemaAsync(tenant, cancellationToken);

                        migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
                    }
                }

                Logger.LogInformation($"Seeding tenant data: {tenant.Name} ({tenant.Id})");
                await SeedDataAsync(tenant);
            }

            Logger.LogInformation($"Successfully completed {tenant.Name} tenant database migrations.");
        }

        Logger.LogInformation("Successfully completed all database migrations.");
        Logger.LogInformation("You can safely end this process...");
    }

    private async Task MigrateDatabaseSchemaAsync(Tenant tenant, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Migrating schema for {(tenant == null ? "host" : tenant.Name + " tenant")} database...");

        using (var uow = _unitOfWorkManager.Begin(true))
        {
            if (tenant == null)
            {
                await MigrateDatabaseAsync<TenantServiceDbContext>(cancellationToken);
            }

            await MigrateDatabaseAsync<AuthServerDbContext>(cancellationToken);
            await MigrateDatabaseAsync<AdministrationServiceDbContext>(cancellationToken);
            await MigrateDatabaseAsync<VerificationServiceDbContext>(cancellationToken);

            await uow.CompleteAsync(cancellationToken);
        }
    }

    private async Task MigrateDatabaseAsync<TDbContext>(CancellationToken cancellationToken) where TDbContext : DbContext, IEfCoreDbContext
    {
        Logger.LogInformation($"Migrating {typeof(TDbContext).Name.RemovePostFix("DbContext")} database...");

        var dbContext = await _unitOfWorkManager.Current.ServiceProvider
            .GetRequiredService<IDbContextProvider<TDbContext>>()
            .GetDbContextAsync();

        await dbContext.Database.MigrateAsync(cancellationToken);
    }

    private async Task SeedDataAsync(Tenant tenant = null)
    {
        Logger.LogInformation($"Executing {(tenant == null ? "host" : tenant.Name + " tenant")} database seed...");

        await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
            .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
            .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue)
        );
    }
}
