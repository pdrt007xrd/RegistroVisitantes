using Microsoft.EntityFrameworkCore;
using WebApp.RegistroVisitantes.Models;

namespace WebApp.RegistroVisitantes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Visitante> Visitantes { get; set; }
    }
}
