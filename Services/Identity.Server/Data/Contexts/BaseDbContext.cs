using System.Reflection; 
using Finbuckle.MultiTenant;
using Identity.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Server.Data.Contexts
{ 
    public abstract class BaseDbContext
            : MultiTenantIdentityDbContext<AppUser, 
                    AppRole, string, 
                    IdentityUserClaim<string>,
                    IdentityUserRole<string>, 
                    IdentityUserLogin<string>,
                    IdentityRoleClaim<string>, 
                    IdentityUserToken<string>>
    {
        //        protected BaseDbContext(ITenantInfo tenantInfo): base(tenantInfo){}
        protected BaseDbContext(ITenantInfo tenantInfo,DbContextOptions options) : base(tenantInfo, options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           /* if(!string.IsNullOrEmpty(TenantInfo.ConnectionString))
            {
                optionsBuilder.UseSqlServer(TenantInfo.ConnectionString, options=>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            };         */   
        }
    }
    
}