using Microsoft.AspNetCore.Mvc;
using MasoksTech.Web.Data;
using MasoksTech.Web.Models;
using System.Linq;

namespace MasoksTech.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos el contexto real de la base de datos
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listar usuarios desde SQLite
        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        // Agregar un nuevo usuario a SQLite
        [HttpPost]
        public IActionResult Agregar(string nombre, string correo, string rol)
        {
            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(correo))
            {
                var nuevoUsuario = new UsuarioItem
                {
                    Nombre = nombre,
                    Correo = correo,
                    Rol = string.IsNullOrEmpty(rol) ? "Cliente" : rol,
                    Activo = true
                };

                _context.Usuarios.Add(nuevoUsuario);
                _context.SaveChanges(); // 🌟 Guarda de manera permanente
            }

            return RedirectToAction("Index");
        }

        // Eliminar un usuario de SQLite
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges(); // 🌟 Guarda la eliminación permanentemente
            }
            return RedirectToAction("Index");
        }
    }
}