namespace Identity.Server.Extentions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            return services
                .AddIdentityServices()
                .AddMultitenancyServices(configuration)
                .AddPersistenceServices(configuration) 
                .AddIdentityServerService();
        }
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            return app 
                .UseAuthentication()
                //.UseCurrentUser()
                .UseMultiTenant()
                .UseAuthorization();
                //.UseOpenApiDocumentation();
        }
    }
}