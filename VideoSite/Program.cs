using DataAccessLayer;
using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using VideoSite;
using VideoSite.Hubs;
using VideoSite.Subscription;
using VideoSite.Subscription.Base;
using VideoSite.Subscription.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc().AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<BusinessLayer.AssemblyMarkUp>()).AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
builder.Services.AddSignalR();
builder.Services.AddDbContext<ADC>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ADC>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddDefaultTokenProviders();

builder.Services.AddCors();

builder.Services.AddSpaStaticFiles(x =>
{
    x.RootPath = "dist";
});

builder.Services.AddSingleton<MessageDatabaseSubscription>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    endpoint.MapControllerRoute(name: "default", pattern: "api/{controller=Home}/{action=Index}/{id?}");
});
app.UseSpaStaticFiles();
app.UseSpa(x =>
{
    if (app.Environment.IsDevelopment())
    {
        x.UseProxyToSpaDevelopmentServer("http://localhost:5173/");
    }
});
app.Run();
