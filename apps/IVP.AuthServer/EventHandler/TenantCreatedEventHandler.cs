using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace IVP.AuthServer.EventHandler;

public class TenantCreatedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly ILogger<TenantCreatedEventHandler> _logger;
    private readonly IIdentityDataSeeder _identityDataSeeder;

    public TenantCreatedEventHandler(
        ICurrentTenant currentTenant,
        IIdentityDataSeeder identityDataSeeder,
        ILogger<TenantCreatedEventHandler> logger)
    {
        _currentTenant = currentTenant;
        _identityDataSeeder = identityDataSeeder;
        _logger = logger;
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        var tenantId = eventData.Id;
        var userName = eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminEmailPropertyName);
        var password = eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminPasswordPropertyName);

        _logger.LogInformation($"Adding admin user for tenant(${tenantId})");

        using (_currentTenant.Change(tenantId))
        {
            await _identityDataSeeder.SeedAsync(userName, password, tenantId);

        }
    }
}
