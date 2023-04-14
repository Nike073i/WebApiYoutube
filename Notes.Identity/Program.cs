using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Notes.Identity;
using Notes.Identity.Data;
using Notes.Identity.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;
var env = builder.Environment;

var connectionString = config.GetConnectionString("DbConnection");

services.AddDbContext<AuthDbContext>(options => options.UseSqlite(connectionString));
services.AddIdentity<AppUser, IdentityRole>(config =>
{
    var password = config.Password;
    password.RequiredLength = 4;
    password.RequireDigit = false;
    password.RequireNonAlphanumeric = false;
    password.RequireUppercase = false;
}).AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();
services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Notes.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});
services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Styles")),
    RequestPath = "/styles"
});
app.UseIdentityServer();

app.MapDefaultControllerRoute();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var context = serviceProvider.GetRequiredService<AuthDbContext>();
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured while app initialization");
}

app.Run();
