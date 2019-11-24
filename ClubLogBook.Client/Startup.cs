using ClubLogBook.Client.Services.Contracts;
using ClubLogBook.Client.Services.Implementations;
using ClubLogBook.Client.States;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ClubLogBook.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddScoped<IdentityAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
            services.AddScoped<IAuthorizeApi, AuthorizeApi>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandlerOptions.DefaultCredentials = FetchCredentialsOption.Include;
            app.AddComponent<App>("app");
        }
    }
}
