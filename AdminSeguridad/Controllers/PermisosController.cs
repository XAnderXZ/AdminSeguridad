using Microsoft.AspNetCore.Mvc;
using AdminSeguridad.Models;
using System.Threading.Tasks;

namespace AdminSeguridad.Controllers
{
    public class PermisosController : Controller
    {
        private readonly AdminSeguridadDbContext _context;

        public PermisosController(AdminSeguridadDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var permisos = _context.Permisos.ToList();
            return View(permisos); // This correctly returns the 'Index' view with the list of 'Permisos'
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                _context.Permisos.Add(permiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permiso);
        }

        public IActionResult Edit(int id)
        {
            var permiso = _context.Permisos.Find(id);
            if (permiso == null)
            {
                return NotFound();
            }
            return View(permiso);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                _context.Permisos.Update(permiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permiso);
        }

        public IActionResult Delete(int id)
        {
            var permiso = _context.Permisos.Find(id);
            if (permiso == null)
            {
                return NotFound();
            }
            return View(permiso);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso != null)
            {
                _context.Permisos.Remove(permiso);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
