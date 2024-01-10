using IVP.AdministrationService.DomainShared;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace IVP.AdministrationService.ApplicationContracts;

[DependsOn(
    typeof(AdministrationServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule)
    )]
public class AdministrationServiceApplicationContractsModule : AbpModule
{

}
