using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Server.Constants.Tenancy;
using Identity.Server.Data.Contexts; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace Identity.Server.Extentions
{
    public static class TenancyServiceExtensions
    {
        public static IServiceCollection AddMultitenancyServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<TenantDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Identity.Server")))
                .AddMultiTenant<ServiceTenantInfo>()
                    .WithEFCoreStore<TenantDbContext, ServiceTenantInfo>()
                    //.WithStore<BaseDbContext>(ServiceLifetime.Scoped, new ServiceTenantInfo { ConnectionString = configuration.GetConnectionString("DefaultConnection")})

                    .WithHeaderStrategy(TenancyConstants.TenantIdName)
                    .WithClaimStrategy("Tenant")
                    .WithCustomQueryStringStrategy(TenancyConstants.TenantIdName)
                    .Services;
                    ///.AddScoped<ITenantService, TenantService>();
        }

        internal static IApplicationBuilder UseMultitenancy(this IApplicationBuilder app)
        {
            return app
                .UseMultiTenant();
        }

        private static FinbuckleMultiTenantBuilder<ServiceTenantInfo> WithCustomQueryStringStrategy( 
            this FinbuckleMultiTenantBuilder<ServiceTenantInfo> builder, string customQueryStringStrategy)
        {
            return builder
                .WithDelegateStrategy(context =>
                {
                    if (context is not HttpContext httpContext)
                    {
                        return Task.FromResult((string)null);
                    }

                    httpContext.Request.Query.TryGetValue(customQueryStringStrategy, out StringValues tenantIdParam);

                    return Task.FromResult(tenantIdParam.ToString());
                });
        }
    }
}