using IVP.VerificationService.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace IVP.VerificationService.EntityFrameworkCore;

[ConnectionStringName(VerificationServiceDbProperties.ConnectionStringName)]
public interface IVerificationServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
