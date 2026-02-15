using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitasApi.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;

namespace VisitasApi.Controllers {
[ApiController]
[Route("api/[controller]")]
public class VisitasController : ControllerBase {
private readonly VisitasContext _context;

public VisitasController(VisitasContext context) {
_context = context;
}

[HttpGet]
public async Task<ActionResult> GetContactos([FromQuery] string buscar = "", [FromQuery] int page = 1, [FromQuery] string fechaDesde = "", [FromQuery] string fechaHasta = "") {
int pageSize = 5;
var query = _context.Contactos.AsQueryable();

if (!string.IsNullOrEmpty(buscar)) {
query = query.Where(c => c.Nombre.Contains(buscar) || c.EmpresaProveniente.Contains(buscar));
}

if (!string.IsNullOrEmpty(fechaDesde) && DateTime.TryParse(fechaDesde, out var desde)) {
query = query.Where(c => c.FechaVisita.Date >= desde.Date);
}

if (!string.IsNullOrEmpty(fechaHasta) && DateTime.TryParse(fechaHasta, out var hasta)) {
query = query.Where(c => c.FechaVisita.Date <= hasta.Date);
}

var total = await query.CountAsync();
var contactos = await query.OrderByDescending(c => c.FechaVisita).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

return Ok(new { data = contactos, total });
}

[HttpPost]
public async Task<ActionResult> CreateContacto([FromBody] Contacto contacto) {
if (!ModelState.IsValid) {
return BadRequest(ModelState);
}

contacto.FechaVisita = DateTime.Now;
_context.Contactos.Add(contacto);
await _context.SaveChangesAsync();

return CreatedAtAction(nameof(GetContactos), new { id = contacto.Id }, contacto);
}

[HttpGet("reporte")]
public async Task<FileResult> GenerarReporte([FromQuery] string buscar = "", [FromQuery] string fechaDesde = "", [FromQuery] string fechaHasta = "") {
var query = _context.Contactos.AsQueryable();

if (!string.IsNullOrEmpty(buscar)) {
query = query.Where(c => c.Nombre.Contains(buscar) || c.EmpresaProveniente.Contains(buscar));
}

if (!string.IsNullOrEmpty(fechaDesde) && DateTime.TryParse(fechaDesde, out var desde)) {
query = query.Where(c => c.FechaVisita.Date >= desde.Date);
}

if (!string.IsNullOrEmpty(fechaHasta) && DateTime.TryParse(fechaHasta, out var hasta)) {
query = query.Where(c => c.FechaVisita.Date <= hasta.Date);
}

var contactos = await query.OrderByDescending(c => c.FechaVisita).ToListAsync();

QuestPDF.Settings.License = LicenseType.Community;

var pdf = Document.Create(container => {
container.Page(page => {
page.Size(PageSizes.A4);
page.Margin(20);

page.Content().Column(col => {
col.Item().Text("REPORTE DE VISITAS").Bold().FontSize(16);
col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").FontSize(10).Italic();
col.Item().PaddingVertical(10);

col.Item().Table(table => {
table.ColumnsDefinition(columns => {
columns.RelativeColumn(1f);
columns.RelativeColumn(1f);
columns.RelativeColumn(1.5f);
columns.RelativeColumn(1.5f);
columns.RelativeColumn(1.2f);
});

table.Header(header => {
header.Cell().Background("2C3E50").Text("Nombre").FontColor("FFFFFF").Bold();
header.Cell().Background("2C3E50").Text("Cédula").FontColor("FFFFFF").Bold();
header.Cell().Background("2C3E50").Text("Empresa").FontColor("FFFFFF").Bold();
header.Cell().Background("2C3E50").Text("Lugar Visita").FontColor("FFFFFF").Bold();
header.Cell().Background("2C3E50").Text("Fecha").FontColor("FFFFFF").Bold();
});

foreach (var contacto in contactos) {
table.Cell().Text(contacto.Nombre);
table.Cell().Text(contacto.Cedula);
table.Cell().Text(contacto.EmpresaProveniente);
table.Cell().Text(contacto.DondeVisita);
table.Cell().Text(contacto.FechaVisita.ToString("dd/MM/yyyy"));
}
});

col.Item().PaddingVertical(10);
col.Item().Text($"Total de registros: {contactos.Count}").FontSize(10).Bold();
});
});
});

var pdfBytes = pdf.GeneratePdf();
Response.Headers["Content-Disposition"] = "inline";
Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
Response.Headers["Pragma"] = "no-cache";
Response.Headers["Expires"] = "0";
return File(pdfBytes, "application/pdf");
}
}
}
