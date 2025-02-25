using Blazor.Extensions.Logging;
using ClubLogBook.Client.Models;
using ClubLogBook.Client.Services.Contracts;
using ClubLogBook.Client.Services.Implementations;
using ClubLogBook.Client.States;
using ClubLogBook.Client.ViewModel;
using Microsoft.AspNetCore.Blazor.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddLogging(builder => builder
            .AddBrowserConsole() // Add Blazor.Extensions.Logging.BrowserConsoleLogger
            .SetMinimumLevel(LogLevel.Trace));
            services.AddTransient<IFetchDataViewModel, FetchDataViewModel>();
            services.AddTransient<IFetchDataModel, FetchData_Model>();
            services.AddTransient<IBasicForecastViewModel, BasicForecastViewModel>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            WebAssemblyHttpMessageHandlerOptions.DefaultCredentials = FetchCredentialsOption.Include;
            app.AddComponent<App>("app");
        }
    }
}
