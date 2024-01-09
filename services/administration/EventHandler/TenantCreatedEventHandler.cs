using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace IVP.AdministrationService.EventHandler;

public class TenantCreatedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly ILogger<TenantCreatedEventHandler> _logger;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IPermissionDataSeeder _permissionDataSeeder;


    public TenantCreatedEventHandler(ICurrentTenant currentTenant,
        ILogger<TenantCreatedEventHandler> logger,
         IUnitOfWorkManager unitOfWorkManager,
        IPermissionDefinitionManager permissionDefinitionManager,
         IPermissionDataSeeder permissionDataSeeder)
    {

        _currentTenant = currentTenant;
        _logger = logger;
        _permissionDefinitionManager = permissionDefinitionManager;
        _unitOfWorkManager = unitOfWorkManager;
        _permissionDataSeeder = permissionDataSeeder;

    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            await GrantPermissionAsync(eventData.Id);
        }
        catch (Exception ex)
        {
            await ExceptionTenantCreatedAsync(eventData, ex);
        }
    }

    private Task ExceptionTenantCreatedAsync(TenantCreatedEto eventData, Exception ex)
    {
        throw new NotImplementedException();
    }

    private async Task GrantPermissionAsync(Guid? tenantId)
    {
        _logger.LogInformation($"Granting Permissions to tenant admin({tenantId})");

        using (_currentTenant.Change(tenantId))
        {
            var uowOptions = new AbpUnitOfWorkOptions { IsTransactional = true };

            using var uow = _unitOfWorkManager.Begin(uowOptions, true);

            var definitions = await _permissionDefinitionManager.GetPermissionsAsync();

            var permissionNames = definitions.Where(p => p.MultiTenancySide.HasFlag(MultiTenancySides.Tenant))
                                           .Where(p => !p.Providers.Any() || p.Providers.Contains(RolePermissionValueProvider.ProviderName))
                                           .Where(p => p.IsEnabled)
                                           .Select(p => p.Name)
                                           .ToArray();

            await _permissionDataSeeder.SeedAsync(
                RolePermissionValueProvider.ProviderName,
                "admin",
                permissionNames,
                tenantId
            );

            _logger.LogInformation($"Granted {permissionNames.Length} permissions to tenant admin");

            await uow.CompleteAsync();
        }
    }
}
