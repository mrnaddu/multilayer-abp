using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace IVP.VerificationService.EntityFrameworkCore;

public static class VerificationServiceDbContextModelCreatingExtensions
{
    public static void ConfigureVerificationService(
    this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
