using Microsoft.EntityFrameworkCore;
using web.net_project.Data;
using web.net_project.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Database ──────────────────────────────────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// ── Application Services ──────────────────────────────────────────────────────
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// StoreService is Scoped because it depends on the scoped ApplicationDbContext
builder.Services.AddScoped<StoreService>();

var app = builder.Build();

// ── Database Seed ─────────────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Ensure database schema is up to date
    await db.Database.MigrateAsync();

    // Seed default products if the table is empty
    var storeService = scope.ServiceProvider.GetRequiredService<StoreService>();
    await storeService.SeedIfEmptyAsync();
}

// ── Middleware Pipeline ───────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
