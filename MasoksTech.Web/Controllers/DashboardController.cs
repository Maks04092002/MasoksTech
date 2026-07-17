using Microsoft.AspNetCore.Mvc;
using MasoksTech.Web.Data;
using System.Linq;

namespace MasoksTech.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        // 1. Recibimos el contexto de la base de datos por inyección de dependencias
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 🌟 2. CRÍTICO: Pasamos '_context' a la vista para que 'Model' no sea null
            return View(_context);
        }
    }
}