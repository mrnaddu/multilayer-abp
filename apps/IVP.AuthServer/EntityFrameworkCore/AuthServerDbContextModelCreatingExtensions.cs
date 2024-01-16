using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace IVP.AuthServer.EntityFrameworkCore;

public static class AuthServerDbContextModelCreatingExtensions
{
    public static void ConfigureAuthServer(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

    }
}
