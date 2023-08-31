using DataAccessLayer;
using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using VideoSite;
using VideoSite.Hubs;
using VideoSite.Subscription;
using VideoSite.Subscription.Base;
using VideoSite.Subscription.Middleware;

namespace VideoSite
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static bool IsInUnitTest
        {
            get
            {
                var a = Environment.GetEnvironmentVariable("IsInUnitTest");
                return a != null && int.Parse(a) == 1;
            }
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Configuration = builder.Configuration;
            ConfigureServices(builder.Services);
            var app = builder.Build();
            Configure(app, app.Environment);
            app.Run();
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddMvc().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<BusinessLayer.AssemblyMarkUp>()).AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                x.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddSignalR();
            services.AddDbContext<ADC>(x =>
            {
                if (!IsInUnitTest)
                    x.UseSqlServer(Configuration.GetConnectionString("Default"));
            });


            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ADC>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services.AddCors();

            services.AddSpaStaticFiles(x =>
            {
                x.RootPath = "dist";
            });

            services.AddSingleton<MessageDatabaseSubscription>();
        }
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDatabaseSubscription<MessageDatabaseSubscription>();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapHub<VerifyEmailHub>("hub/verifyEmail");
                endpoint.MapHub<MessageHub>("hub/Message");
                endpoint.MapControllerRoute(name: "default", pattern: "api/{controller}/{action=Index}/{id?}");
            });
            app.UseSpaStaticFiles();
            if (!IsInUnitTest)
                app.UseSpa(x =>
                {
                    if (env.IsDevelopment())
                    {
                        x.UseProxyToSpaDevelopmentServer("http://localhost:5173/");
                    }
                });

        }
    }
}