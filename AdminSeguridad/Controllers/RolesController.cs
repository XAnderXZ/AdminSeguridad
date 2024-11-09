using Microsoft.AspNetCore.Mvc;
using AdminSeguridad.Models;
using System.Threading.Tasks;

namespace AdminSeguridad.Controllers
{
    public class RolesController : Controller
    {
        private readonly AdminSeguridadDbContext _context;

        public RolesController(AdminSeguridadDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        public IActionResult Edit(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Update(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        public IActionResult Delete(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
