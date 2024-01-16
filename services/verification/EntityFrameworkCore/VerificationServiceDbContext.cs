using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IVP.VerificationService.EntityFrameworkCore;

public class VerificationServiceDbContext : AbpDbContext<VerificationServiceDbContext>
    , IVerificationServiceDbContext
{

    public VerificationServiceDbContext(DbContextOptions<VerificationServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureVerificationService();
    }
}
