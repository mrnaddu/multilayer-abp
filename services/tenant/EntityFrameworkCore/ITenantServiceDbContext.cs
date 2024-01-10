﻿using IVP.TenantService.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace IVP.TenantService.EntityFrameworkCore;

[ConnectionStringName(TenantServiceDbProperties.ConnectionStringName)]
public interface ITenantServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
