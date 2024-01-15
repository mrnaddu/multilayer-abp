using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace IVP.AuthServer.Shared;

[ReplaceDbContext(typeof(IIdentityDbContext))]

[ConnectionStringName("Default")]
public class AuthServerDbContext : AbpDbContext<AuthServerDbContext>,
    IIdentityDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
    * public DbSet<Question> Questions { get; set; }
    */
    public AuthServerDbContext(DbContextOptions<AuthServerDbContext> options)
        : base(options)
    {

    }

    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureIdentity();
    }
}
