using GBC_Travel_Group_90.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GBC_Travel_Group_90.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Filters;
using CGBC_Travel_Group_90.Services;
using Serilog;
using GBC_Travel_Group_90.CustomMiddlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddRazorPages();

// Ensures IEmailSender is injected, then an instance is provided
builder.Services.AddSingleton<IEmailSender, EmailSender>();

//<<<<     >>>>>>

//<<<<   Register Filters   >>>>>>

builder.Services.AddScoped<LoggingFilter>();

builder.Services.AddScoped<ValidateModelFilter>();

//Register global filters for all controllers, actions and razor pages here
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    }); ;

//Inilialize Serilog
//configure Serilog to read from appsetting.json configuartion
//result in calling Serilog() on the Host builder
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

//Register Serilog Service
builder.Services.AddHttpContextAccessor();

// Register Session Service
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    
}
else
{
    app.UseDeveloperExceptionPage();
}


app.UseHsts();

using var scope = app.Services.CreateScope();
var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

try
{
    ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await ContextSeed.SeedRolesAsync(userManager, roleManager);
    await ContextSeed.SeedAdminAsync(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred seeding the Roles in the Database");
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();



app.UseRouting();


//<<<<   Using Custom Logging Middleware   >>>>>>



app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseLoggingMiddleware();





app.UseAuthentication();

app.UseAuthorization();








app.MapRazorPages();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{name?}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
/*
app.MapControllerRoute(
    name: "error",
    pattern: "/Error/{statusCode?}",
    defaults: new { controller = "Error", action = "Error" }
);
*/


app.Run();
