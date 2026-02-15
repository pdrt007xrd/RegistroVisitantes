using Microsoft.AspNetCore.Mvc;

namespace VisitasApi.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
