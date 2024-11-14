using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Server.Constants;
using Identity.Server.Constants.Tenancy;
using Identity.Server.Data.Contexts;
using Identity.Server.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Identity.Server.Constants.PermissionConstants;

namespace Identity.Server.Data.DbInitializer
{
       public class ApplicationDbInitializer(
        ApplicationDbContext applicationDbContext,
        ServiceTenantInfo tenantInfo,
        RoleManager<AppRole> roleManager,
        UserManager<AppUser> userManager)
    {
        private readonly ServiceTenantInfo _tenantInfo = tenantInfo;
        private readonly RoleManager<AppRole> _roleManager = roleManager;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task InitializeDatabaseAsync( CancellationToken cancellationToken)
        {
            if(_applicationDbContext.Database.GetMigrations().Any())
            {
                if((await _applicationDbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                    await _applicationDbContext.Database.MigrateAsync(cancellationToken);
                if((await _applicationDbContext.Database.CanConnectAsync(cancellationToken)))
                {
                    await InitializeDefaultRolesAsync(cancellationToken);
                    await InitializeAdminUserAsync();
                }
                
            }
        }
        private async Task InitializeDefaultRolesAsync(CancellationToken cancellationToken)
        {
            foreach(string roleName in RoleConstants.DefaultRoles)
            {
                if(await _roleManager.Roles.SingleOrDefaultAsync(r=>r.Name==roleName, cancellationToken) is not AppRole incommingRole)
                {
                    incommingRole =  new AppRole(){ Name=roleName, Description=$"{roleName} Role"};
                    await _roleManager.CreateAsync(incommingRole);
                }

                //Assign Permission to newly added role
                if (roleName == RoleConstants.Basic)
                {
                    await AssignPermissionToRole ( ServicePermissions.Basic,incommingRole,cancellationToken);
                }
                else if(roleName == RoleConstants.Admin)
                {
                    await AssignPermissionToRole (ServicePermissions.Admin,incommingRole,cancellationToken);
                }
            }
        }
        private async Task InitializeAdminUserAsync()
        {
            if(await _userManager.Users.FirstOrDefaultAsync(u=>u.Email==_tenantInfo.AdminEmail) is not AppUser adminUser)
            {
                if( string.IsNullOrEmpty(_tenantInfo.AdminEmail))
                {
                    return;
                }
                adminUser = new AppUser()
                {
                    FirstName= TenancyConstants.FirstName,
                    LastName=TenancyConstants.LastName,
                    Email=_tenantInfo.AdminEmail,
                    UserName=_tenantInfo.AdminEmail,
                    EmailConfirmed=true,
                    PhoneNumberConfirmed=true,
                    NormalizedEmail=_tenantInfo.AdminEmail.ToUpperInvariant(),
                    NormalizedUserName=_tenantInfo.AdminEmail.ToUpperInvariant(),
                    IsActive = true

                };

                var password = new PasswordHasher<AppUser>();
                adminUser.PasswordHash=password.HashPassword(adminUser, TenancyConstants.DefaultPassword);

                await _userManager.CreateAsync(adminUser);
            } 
            if(!await _userManager.IsInRoleAsync(adminUser, RoleConstants.Admin))
            {
                await _userManager.AddToRoleAsync(adminUser,RoleConstants.Admin);
            }



        }

        private async Task AssignPermissionToRole ( IReadOnlyList<ServicePermission> rolePermissions,
                                                    AppRole currentRole,
                                                    CancellationToken cancellationToken)
        {
            var currentClaims= await _roleManager.GetClaimsAsync(currentRole);
            foreach(ServicePermission rolePermission in rolePermissions)
            {
                if(!currentClaims.Any(c=>c.Type == ClaimConstants.Permission && c.Value == rolePermission.Name))
                {
                    await _applicationDbContext.RoleClaims.AddAsync(new IdentityRoleClaim<string>
                    {
                        RoleId=currentRole.Id,
                        ClaimType=ClaimConstants.Permission,
                        ClaimValue=rolePermission.Name
                    });

                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }

    }
}