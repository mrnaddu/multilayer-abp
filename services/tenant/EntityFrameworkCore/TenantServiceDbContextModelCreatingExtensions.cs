using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace IVP.TenantService.EntityFrameworkCore;

public static class TenantServiceDbContextModelCreatingExtensions
{
    public static void ConfigureTenantService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

    }
}
