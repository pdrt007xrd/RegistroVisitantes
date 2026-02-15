using Microsoft.EntityFrameworkCore;
using VisitasApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// AJUSTA TU CONEXIÓN AQUÍ:
var connectionString = "Server=localhost;Database=VisitDb;User Id=sa;Password=705055911Xx;Encrypt=True;TrustServerCertificate=True;";
builder.Services.AddDbContext<VisitasContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
