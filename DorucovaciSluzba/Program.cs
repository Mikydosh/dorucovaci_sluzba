using Microsoft.EntityFrameworkCore;
using DorucovaciSluzba.Infrastructure.Database;
using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Application.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Databázové připojení
string connectionString = builder.Configuration.GetConnectionString("MySQL");
var serverVersion = ServerVersion.AutoDetect(connectionString);

// Registrace AppDbContext
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
    optionsBuilder.UseMySql(connectionString, serverVersion));

// DŮLEŽITÉ: Registruj AppDbContext také jako DbContext (pro Application služby)
builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<AppDbContext>());

// Registrace Application Services
builder.Services.AddScoped<IPackageAppService, PackageAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Area routing
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();