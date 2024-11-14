using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Server.Models;

namespace Identity.Server.Extentions
{
    internal static class IdentityServerServiceExtentions
    {
        internal static IServiceCollection AddIdentityServerService(this IServiceCollection services)
        {
            services.AddIdentityServer(options=>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddAspNetIdentity<AppUser>()
            .AddDeveloperSigningCredential();
            

            return services;
            
        }
    }
}