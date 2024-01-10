using IVP.AuthServer.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace IVP.AuthServer.EntityFrameworkCore;

[ConnectionStringName(AuthServerDbProperties.ConnectionStringName)]
public interface IAuthServerDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
    * DbSet<Question> Questions { get; }
    */
}
