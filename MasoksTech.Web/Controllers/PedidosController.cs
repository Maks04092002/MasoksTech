using Microsoft.AspNetCore.Mvc;

namespace MasoksTech.Web.Controllers
{
    [Route("Pedidos")] // Forzamos a que escuche esta ruta exacta
    public class PedidosController : Controller
    {
        [HttpGet("")] // Escucha la ruta raíz de "Pedidos"
        public IActionResult Index()
        {
            return Content("<h2>Gestión de Pedidos</h2>", "text/html");
        }
    }
}