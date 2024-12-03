using Microsoft.AspNetCore.Mvc;
using AdminSeguridad.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AdminSeguridad.Controllers
{
    public class LoginController : Controller
    {
        // Acción para mostrar la página de inicio de sesión
        public IActionResult Index()
        {
            return View();
        }

        // Acción para manejar el inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aquí iría la lógica de autenticación
                // Por ejemplo, verificar usuario y clave en la base de datos.
                var usuario = model.Usuario;
                var clave = model.Clave;

                // Simulando una validación de usuario
                if (usuario == "admin" && clave == "1234") // Validación simplificada, en producción debes verificar con la base de datos
                {
                    // Aquí creamos una identidad de usuario y la autenticamos
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario),
                        new Claim(ClaimTypes.Role, "Admin") // Asignar el rol correspondiente
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true // Si deseas que el usuario se mantenga autenticado
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home"); // Redirige al Home después de iniciar sesión correctamente
                }
                else
                {
                    // Si las credenciales son incorrectas, se muestra un mensaje de error
                    model.ErrorMessage = "El nombre de usuario o la contraseña son incorrectos.";
                    return View(model);
                }
            }

            // Si el modelo es inválido, vuelve a mostrar la vista de login
            return View(model);
        }

        // Acción para cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login"); // Redirige al login
        }
    }
}
