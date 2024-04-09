using GBC_Travel_Group_90.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GBC_Travel_Group_90.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using GBC_Travel_Group_90.Filters;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Ensures IEmailSender is injected, then an instance is provided
builder.Services.AddSingleton<IEmailSender, EmailSender>();



//Register Filters
builder.Services.AddScoped<LoggingFilter> ();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller:exists}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//Custom Route
app.MapGet("/CustomRouteError", async context => {
    await context.Response.WriteAsync("Error StatusCode and Message Here");
}) ;

//Dynamic Route
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;
    //resolve path to content identifier
    if (path.StartsWith("/TravelManagement/Flight", StringComparison.OrdinalIgnoreCase))
    {
        var slug = path.Substring("/TravelManagement/Flight".Length).Trim('/');
        //resolve slug to article Id and set route values
        context.Request.RouteValues["Controller"] = "Flights";
        context.Request.RouteValues["action"] = "Details";
        //context.Request.RouteValues["id"] = ResolveSlugToId(slug);

    }
    await next();
});

app.Run();
