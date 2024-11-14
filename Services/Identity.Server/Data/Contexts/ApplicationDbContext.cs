using Finbuckle.MultiTenant;
using Identity.Server.Models; 
using Microsoft.EntityFrameworkCore;

namespace Identity.Server.Data.Contexts
{
    public class ApplicationDbContext : BaseDbContext
    {
        //public ApplicationDbContext(ITenantInfo tenantInfo) : base(tenantInfo){}
        public ApplicationDbContext(ITenantInfo tenantInfo, DbContextOptions<ApplicationDbContext> options) : base(tenantInfo, options)
        {
            
        }

        //public DbSet<AppUser>AppUsers {get ; set ;}
    }
} 