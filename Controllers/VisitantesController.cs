using Microsoft.AspNetCore.Mvc;
using WebApp.RegistroVisitantes.Models;
using WebApp.RegistroVisitantes.Data;

namespace WebApp.RegistroVisitantes.Controllers
{
    public class VisitantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var lista = _context.Visitantes.ToList();
            return Json(new { data = lista });
        }

        [HttpPost]
public IActionResult Agregar([FromBody] Visitante visitante)
{
    // Validar ModelState
    if (!ModelState.IsValid)
    {
        // Devuelve los errores como JSON
        var errores = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return Json(new { success = false, errores });
    }

    visitante.FechaHora = DateTime.Now;
    _context.Visitantes.Add(visitante);
    _context.SaveChanges();

    return Json(new { success = true });
}

    }
}
