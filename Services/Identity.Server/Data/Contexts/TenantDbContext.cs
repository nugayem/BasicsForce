using Finbuckle.MultiTenant.Stores;
using Identity.Server.Constants.Tenancy;
using Microsoft.EntityFrameworkCore;

namespace Identity.Server.Data.Contexts
{
      public class TenantDbContext(DbContextOptions<TenantDbContext> options) 
        : EFCoreStoreDbContext<ServiceTenantInfo>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ServiceTenantInfo>()
                .ToTable("Tenants" );
        }

        //Dbset<ServiceTenantInfo> ServiceTenantInfo {get; set;}
    }

}