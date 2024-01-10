using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IVP.VerificationService.EntityFrameworkCore;

public class VerificationServiceDbContext : AbpDbContext<VerificationServiceDbContext>
    , IVerificationServiceDbContext
{

    /* Add DbSet for each Aggregate Root here. Example:
    * public DbSet<Question> Questions { get; set; }
     */


    public VerificationServiceDbContext(DbContextOptions<VerificationServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureVerificationService();
    }
}
