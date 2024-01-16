using IVP.AdministrationService.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace IVP.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public interface IAdministrationServiceDbContext : IEfCoreDbContext
{

}
