using Microsoft.AspNetCore.Mvc;
using MasoksTech.Web.Models;
using MasoksTech.Web.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MasoksTech.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inyectamos únicamente el contexto real de la base de datos de SQLite
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // 🌟 ATAJO DE ADMINISTRADOR DIRECTO (Excelente para tus pruebas rápidas)
            if (model.Correo == "admin@masokstech.com" && model.Password == "admin123")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            // 🌟 INICIO DE SESIÓN DE CLIENTES REAL (Buscando en SQLite)
            var usuarioExistente = _context.Usuarios
                .FirstOrDefault(u => u.Correo.ToLower() == model.Correo.ToLower());

            if (usuarioExistente != null)
            {
                if (!usuarioExistente.Activo)
                {
                    ModelState.AddModelError(string.Empty, "Tu cuenta de usuario está inactiva.");
                    return View(model);
                }

                // Redireccionamiento dinámico según el rol en la base de datos real
                if (usuarioExistente.Rol == "Administrador" || usuarioExistente.Rol == "Soporte" || usuarioExistente.Rol == "Vendedor")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    // Redirige al portal para clientes reales
                    return RedirectToAction("Index", "Cliente");
                }
            }

            // Si llegamos aquí es porque las credenciales no existen en SQLite
            ModelState.AddModelError(string.Empty, "Usuario no registrado en la base de datos.");
            return View(model);
        }

        // 🌟 MÉTODOS REALES PARA REGISTRO DE CUENTA EN SQLITE
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Validar si el correo ya existe en la base de datos de SQLite
            var existe = _context.Usuarios
                .Any(u => u.Correo.ToLower() == model.Correo.ToLower());

            if (existe)
            {
                ModelState.AddModelError(string.Empty, "Este correo electrónico ya está registrado.");
                return View(model);
            }

            // Guardar de forma permanente al nuevo cliente en SQLite
            var nuevoUsuario = new UsuarioItem
            {
                Nombre = model.Nombre,
                Correo = model.Correo,
                Rol = "Cliente", // Rol predeterminado para el registro público
                Activo = true
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges(); // 🌟 Guarda los cambios físicamente en masokstech.db

            // Mensaje de éxito al redireccionar
            TempData["SuccessRegister"] = "¡Cuenta creada exitosamente! Ya puedes iniciar sesión con tus credenciales.";
            return RedirectToAction("Login");
        }
    }
}