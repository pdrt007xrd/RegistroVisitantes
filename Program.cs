using Microsoft.EntityFrameworkCore;
using WebApp.RegistroVisitantes.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllersWithViews();

// Configurar DbContext SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=visitantes.db"));

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Visitantes}/{action=Index}/{id?}");

    app.Urls.Add("http://0.0.0.0:5000");

app.Run();
