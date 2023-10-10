using Inventory.Data;
using Inventory.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Custom start
// Database connection.
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cs));

// User information.
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register a service for capturing the identity information of a user during the httpcontext thingie (/_Host.cshtml). Located in Services/IdentityInformation.cs
builder.Services.AddScoped<IdentityInformation>();
// Custom end

// Custom start
// Register the service class located in /Services/IdentityValidationProvider.cs
builder.Services.AddScoped<AuthenticationStateProvider, IdentityValidationProvider<IdentityUser>>();

// Register a service for handling users such as the usermanager and the rolemanager.
builder.Services.AddScoped<UserManagerService>();
// Custom end

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

// Custom start
app.UseAuthentication();
app.UseAuthorization();
// Custom end

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
