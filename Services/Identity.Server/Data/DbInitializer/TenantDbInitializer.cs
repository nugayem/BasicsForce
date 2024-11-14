using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Identity.Server.Constants.Tenancy;
using Identity.Server.Data.Contexts; 
using Microsoft.EntityFrameworkCore;

namespace Identity.Server.Data.DbInitializer
{
    internal class TenantDbInitializer(TenantDbContext tenantDbContext,  IServiceProvider serviceProvider) : ITenantDbInitializer
    {
        private readonly TenantDbContext _tenantDbContext =  tenantDbContext; 
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
        {
            await InitializeDatabaseWithTenantAsync(cancellationToken);

            foreach(var tenant in await _tenantDbContext.TenantInfo.ToListAsync(cancellationToken))
            {
                await InitializeApplicationDbForTenantAsync(tenant, cancellationToken);

            }
        }
     
        private async Task InitializeDatabaseWithTenantAsync(CancellationToken cancellationToken)
        {
            //Check if RootTenant Existi
            if (await _tenantDbContext.TenantInfo.FindAsync([TenancyConstants.Root.Id], cancellationToken) is null)
            {
                //Create root tenant
                var rootTenant =  new ServiceTenantInfo
                {
                    Id= TenancyConstants.Root.Id,
                    Identifier = TenancyConstants.Root.Name,
                    Name=TenancyConstants.Root.Name,
                    AdminEmail=TenancyConstants.Root.Email,
                    IsActive=true,
                    ValidUpTo=DateTime.UtcNow.AddYears(1),
                    ConnectionString="."
                };

                await _tenantDbContext.TenantInfo.AddAsync(rootTenant, cancellationToken);
                await _tenantDbContext.SaveChangesAsync(cancellationToken);

            }
            // -- Skip 
            
        }

        private async Task InitializeApplicationDbForTenantAsync(ServiceTenantInfo tenant, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            _serviceProvider.GetRequiredService<IMultiTenantContextAccessor>()
                            .MultiTenantContext = new MultiTenantContext<ServiceTenantInfo>()
                            {
                                TenantInfo = tenant
                            };
            await _serviceProvider.GetRequiredService<ApplicationDbInitializer>()
                                    .InitializeDatabaseAsync(cancellationToken);
        }

      
    }

}