using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace IVP.TenantService.Shared;

[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("TenantService")]
public class TenantServiceDbContext : AbpDbContext<TenantServiceDbContext>,
    ITenantManagementDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public TenantServiceDbContext(DbContextOptions<TenantServiceDbContext> options)
        : base(options)
    {

    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTenantManagement();
    }
}
