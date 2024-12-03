﻿using Microsoft.AspNetCore.Mvc;
using AdminSeguridad.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

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
        public async Task<IActionResult> Create(Usuario usuarioCreate, IFormFile fotoPerfil)
        {
            if (ModelState.IsValid)
            {
                if (fotoPerfil != null)
                {
                    var filePath = Path.Combine("wwwroot/images", fotoPerfil.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fotoPerfil.CopyToAsync(stream);
                    }
                    usuarioCreate.FotoPerfil = $"/images/{fotoPerfil.FileName}";
                }

                usuarioCreate.FechaCreacion = DateTime.Now;
                usuarioCreate.FechaActualizacion = DateTime.Now;
                _context.Usuarios.Add(usuarioCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuarioCreate);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            ViewBag.Roles = _context.Roles.ToList();
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuarioEditar, IFormFile fotoPerfil)
        {
            if (ModelState.IsValid)
            {
                if (fotoPerfil != null)
                {
                    var filePath = Path.Combine("wwwroot/images", fotoPerfil.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fotoPerfil.CopyToAsync(stream);
                    }
                    usuarioEditar.FotoPerfil = $"/images/{fotoPerfil.FileName}";
                }

                usuarioEditar.FechaActualizacion = DateTime.Now;
                _context.Usuarios.Update(usuarioEditar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(usuarioEditar);
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
