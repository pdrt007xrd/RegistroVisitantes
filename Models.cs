using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VisitasApi.Models {
public class Contacto {
public int Id { get; set; }

[Required(ErrorMessage = "El nombre es obligatorio")]
public string Nombre { get; set; } = string.Empty;

[Required(ErrorMessage = "La cédula es obligatoria")]
public string Cedula { get; set; } = string.Empty;

public string EmpresaProveniente { get; set; } = string.Empty;
public string DondeVisita { get; set; } = string.Empty;
public DateTime FechaVisita { get; set; } = DateTime.Now;
}

public class VisitasContext : DbContext {
public VisitasContext(DbContextOptions<VisitasContext> options) : base(options) {}
public DbSet<Contacto> Contactos => Set<Contacto>();
}
}
