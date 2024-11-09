using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using AdminSeguridad.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class AccountController : Controller
{
    private readonly AdminSeguridadDbContext _context;

    // Constructor para inyectar el contexto de base de datos
    public AccountController(AdminSeguridadDbContext context)
    {
        _context = context;
    }

    // Acción GET para la vista de Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // Acción POST para procesar el login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Buscar usuario en la base de datos e incluir la propiedad de navegación Rol
        var user = _context.Usuarios
            .Include(u => u.Rol) // Ensure Rol is loaded
            .FirstOrDefault(u => u.Email == email && u.Clave == password);

        if (user != null)
        {
            // Crear las reclamaciones (claims) para el usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email), // Usamos el correo electrónico del usuario
                new Claim(ClaimTypes.Role, user.Rol.NombreRol) // Usamos el rol del usuario (debe estar cargado en el contexto)
            };

            // Crear la identidad con las reclamaciones y el esquema de autenticación (cookie)
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Establecer propiedades de autenticación, como la persistencia (recordar al usuario)
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // El usuario permanecerá autenticado entre sesiones
            };

            // Iniciar sesión y almacenar las cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Redirigir al usuario a la página principal después del login exitoso
            return RedirectToAction("Index", "Home");
        }

        // Si el usuario no existe o la contraseña es incorrecta, agregar un error
        ModelState.AddModelError(string.Empty, "Email o contraseña inválidos.");
        return View(); // Retornar a la vista de login con el error
    }

    // Acción para cerrar sesión
    public async Task<IActionResult> Logout()
    {
        // Cerrar la sesión del usuario y eliminar las cookies
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Redirigir a la página de login
        return RedirectToAction("Login", "Account");
    }
}