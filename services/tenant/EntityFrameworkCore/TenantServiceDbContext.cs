using IVP.TenantService.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;

namespace IVP.TenantService.EntityFrameworkCore;

[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName(TenantServiceDbProperties.ConnectionStringName)]
public class TenantServiceDbContext : AbpDbContext<TenantServiceDbContext>,
    ITenantManagementDbContext,
    ITenantServiceDbContext
{
    public TenantServiceDbContext(DbContextOptions<TenantServiceDbContext> options)
        : base(options)
    {

    }

    // Tenants
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTenantManagement();
        builder.ConfigureTenantService();
    }
}
