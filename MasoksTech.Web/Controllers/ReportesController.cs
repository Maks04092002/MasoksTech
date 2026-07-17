using Microsoft.AspNetCore.Mvc;

namespace MasoksTech.Web.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Index()
        {
            // Devolvemos un HTML simple para que la prueba reciba su OK (200)
            return Content("<h2>Módulo de Reportes</h2>", "text/html");
        }
    }
}