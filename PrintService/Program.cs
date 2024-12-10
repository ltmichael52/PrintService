using Microsoft.EntityFrameworkCore;
using PrintService.Extentions;
using PrintService.Models;
using PrintService.Payments;
using PrintService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PrintDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrintService")));

builder.Services.AddDistributedMemoryCache(); // Required for session storage.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout.
    options.Cookie.HttpOnly = true; // Make the cookie accessible only via HTTP.
    options.Cookie.IsEssential = true; // Mark the session cookie as essential.
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRechargeService, RechargeService>();

builder.Services.AddTransient<PayPalService>();
builder.Services.AddTransient<VnPayService>();
builder.Services.AddTransient<MomoService>();
builder.Services.AddScoped<PaymentContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<PrintDbContext>();
UserExtension.Initialize(context);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
