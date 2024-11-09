using Microsoft.AspNetCore.Mvc;
using AdminSeguridad.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdminSeguridad.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AdminSeguridadDbContext _context;

        public UsuariosController(AdminSeguridadDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.Include(u => u.Rol).ToListAsync();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.FechaCreacion = DateTime.Now;
                usuario.FechaActualizacion = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.FechaActualizacion = DateTime.Now;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
