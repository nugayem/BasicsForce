using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Server.Data.Contexts;
using Identity.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Server.Extentions
{   
    internal static class IdentityServiceExtensions
    {

        internal static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            return services.AddIdentity<AppUser,AppRole>(option=>
            {
                option.Password.RequiredLength= 8;
                option.Password.RequireDigit=false;
                option.Password.RequireLowercase=false;
                option.Password.RequireUppercase=false;
                option.Password.RequireNonAlphanumeric=false;
                option.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .Services;
           /* .AddTransient<ITokenService,TokenService>()
            .AddTransient<IRoleService,RoleService>()
            .AddTransient<IUserService,UserService>() 
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<CurrentUserMiddleware>();*/
        }
        /*
        internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CurrentUserMiddleware>();
        }

        internal static IServiceCollection AddPermissions(this IServiceCollection services)
        {
            return services
                    .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                    .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }

        internal static IServiceCollection AddJwtAuthentications(this IServiceCollection services)
        {
            services
                .AddOptions<JwtSettings>()
                .BindConfiguration("JwtSettings");
            services
                .AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            
            services
                    .AddAuthentication(auth=>
                    {
                        auth.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options=>
                    {
                        var jwtSettings =  services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;
                        byte[] key = Encoding.ASCII.GetBytes(jwtSettings.Key);
                        options.TokenValidationParameters =  new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey=true,
                            IssuerSigningKey= new SymmetricSecurityKey(key),
                            ValidateIssuer=false,
                            ValidateAudience=false,
                            ClockSkew = TimeSpan.Zero,
                            RoleClaimType= ClaimTypes.Role,
                            ValidateLifetime=false
                        };
                    });
                return services;
                
        }

*/

    }
    
}