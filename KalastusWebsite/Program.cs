using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using KalastusWebsite.Data;
using KalastusWebsite.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 1024 * 1024 * 1024; // 1 GB
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Use Scoped for user-specific services
builder.Services.AddScoped<UserSession>();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:5129/");
});

// Add authentication with cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
        options.LoginPath = "/login"; // Path to the login page
        options.LogoutPath = "/logout"; // Optional: Path to logout
        options.AccessDeniedPath = "/access-denied"; // Optional: Path for access denied
    });

// Add controllers for APIs
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<EventService>();

// Configure Kestrel to allow large file uploads
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1024 * 1024 * 1024; // 1 GB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Map API controllers here, after middleware is set up
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
