using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Server.Data.Contexts;
using Identity.Server.Data.DbInitializer;
using Microsoft.EntityFrameworkCore;

namespace Identity.Server.Extentions
{
    internal static class PersistenceServiceExtensions
    { 
        internal static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            return services
                        .AddDbContext<ApplicationDbContext>(options => options
                            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Identity.Server")))
                            .AddTransient<ITenantDbInitializer, TenantDbInitializer>()
                            .AddTransient<ApplicationDbInitializer>();
        }

        public static async Task AddDatabseInitializerAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            using var scope = serviceProvider.CreateScope();

           await scope.ServiceProvider.GetRequiredService<ITenantDbInitializer>()
                    .InitializeDatabaseAsync(cancellationToken);
        }
    }
    
}