using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace IVP.AdministrationService.EntityFrameworkCore;

public static class AdministrationServiceDbContextModelCreatingExtensions
{
    public static void ConfigureAdministrationService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

    }
}
