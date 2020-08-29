namespace Presentation.API.Components
{
    using IdentityServer4.Services;
    using IdentityServer4.Validation;
    using Microsoft.Extensions.DependencyInjection;
    using Presentation.API.Configs;

    public static class AuthComponents
    {
        public static IServiceCollection AddAuthComponents(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryClients(Config.GetClients())
                .AddProfileService<Helpers.ProfileService>();

            services.AddTransient<IResourceOwnerPasswordValidator, Helpers.ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, Helpers.ProfileService>();

            return services;
        }
    }
}
