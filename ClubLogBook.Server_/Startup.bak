using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


using ClubLogBook.Infrastructure.Data;
using ClubLogBook.Infrastructure.Persistence;
using ClubLogBook.Infrastructure.Identity;
using ClubLogBook.Server.Services;
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Infrastructure.Logging;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Services;
using ClubLogBook.Application.Infrastructure.AutoMapper;
using ClubLogBook.Application;
using MediatR;
using AutoMapper;
using FluentValidation.AspNetCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ClubLogBook.Server
{
    public class Startup
    {
		public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
            Environment = environment;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("ClubLogBook.Application");
            services.AddApplication();
            services.AddInfrastructure(Configuration, Environment);
            services.AddScoped<ICurrentUserService,CurrentUserService> ();
            services.AddHttpContextAccessor();
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "ClubLogBook API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the TextBox: Bearer {your JWT token}."
                });
                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            services.AddMediatR(assembly);
            //services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddAutoMapper(typeof(Startup));
			services.AddScoped(typeof(IAsyncRepository<>), typeof(EFRepository<>));
			services.AddScoped<IFlightRepository, FlightRepository>();
			services.AddScoped<IClubService, ClubService>();
			services.AddScoped<ILogbookService, LogbookService>();
			services.AddScoped<IClubRepository, ClubRepository>();
			services.AddScoped<ILogbookRepository, LogbookRepository>();
			services.AddScoped<IAircraftLogBookRepository, AircraftLogBookRepository>();
			services.AddScoped<IClubContactsViewModelService, ClubContactsViewModelService>();
			services.AddScoped<IMemberRepository, MemberRepository>();
			services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
			services.AddScoped<IMemberService, MemberService>();
			services.AddScoped<IRecordViewModelService<FlightRecordIndexViewModel>, FlightRecordViewModelService>();
			services.AddScoped<IRecordViewModelService<RecordsViewModel<FlightReservationViewModel>>, ReservationRecordViewModelService>();
			services.AddScoped<IReservationRepository, ReservationRepository>();
			services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IAircraftManagerService, AircraftManagerService>();
            services.AddScoped<IAircraftRepository, AircraftRepository>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Filename=data.db"));
            //         services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddIdentity<ApplicationUser, IdentityUser<Guid>>()
            //	.AddRoles<IdentityRole<Guid>>()
            //             .AddEntityFrameworkStores<ApplicationDbContext>()
            //             .AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireLowercase = false;
            //    //options.Password.RequiredUniqueChars = 6;

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 10;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings
            //    options.User.RequireUniqueEmail = false;
            //});
            //var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

          //  services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          //.AddJwtBearer(options =>
          //{
          //    options.TokenValidationParameters = new TokenValidationParameters
          //    {
          //        ValidateIssuer = true,
          //        ValidateAudience = true,
          //        ValidateLifetime = true,
          //        ValidateIssuerSigningKey = true,
          //        ValidIssuer = Configuration["JwtIssuer"],
          //        ValidAudience = Configuration["JwtAudience"],
          //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
          //    };
          //});
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(name: default, pattern: "{controller=ClubContacts}/{action=Index}");
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
    }
}
