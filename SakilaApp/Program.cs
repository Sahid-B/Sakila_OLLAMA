using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Data;
using SakilaApp.Services;
using SakilaApp.Settings;
using SakilaApp.Services.Payments;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SakilaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailSender, GmailEmailSender>();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        // Parte 8
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "DUMMY_ID";
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "DUMMY_SECRET";
    });

builder.Services.Configure<PayPhoneSettings>(
    builder.Configuration.GetSection("PayPhone"));

builder.Services.AddHttpClient<PayPhoneApiLinkService>();


builder.Services.Configure<PayPalSettings>(
    builder.Configuration.GetSection("PayPal"));

builder.Services.AddHttpClient<PayPalService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();



