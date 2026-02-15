using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VisitasApi.Models {
public class Contacto {
public int Id { get; set; }

[Required(ErrorMessage = "El nombre es obligatorio")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
public string Nombre { get; set; } = string.Empty;

[Required(ErrorMessage = "La cédula es obligatoria")]
[RegularExpression(@"^\d{3}-\d{7}-\d{1}$|^\d{11}$", ErrorMessage = "La cédula debe tener 11 dígitos (ej: 001-0029703-5 o 00100297035)")]
public string Cedula { get; set; } = string.Empty;

[Required(ErrorMessage = "La empresa proveniente es obligatoria")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "La empresa debe tener entre 3 y 100 caracteres")]
public string EmpresaProveniente { get; set; } = string.Empty;

[Required(ErrorMessage = "El lugar de visita es obligatorio")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "El lugar de visita debe tener entre 3 y 100 caracteres")]
public string DondeVisita { get; set; } = string.Empty;

public DateTime FechaVisita { get; set; } = DateTime.Now;
}

public class VisitasContext : DbContext {
public VisitasContext(DbContextOptions<VisitasContext> options) : base(options) {}
public DbSet<Contacto> Contactos => Set<Contacto>();
}
}
