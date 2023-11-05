using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionCentroMedico.Models;

namespace GestionCentroMedico.Controllers
{
    public class MutualesController : Controller
    {
        private readonly GestionTurnosContext _context;

        public MutualesController(GestionTurnosContext context)
        {
            _context = context;
        }

        // GET: Mutuales
        public async Task<IActionResult> Index()
        {
            return _context.Mutuales != null ?
                        View(await _context.Mutuales.ToListAsync()) :
                        Problem("Entity set 'GestionTurnosContext.Mutuales'  is null.");
        }

        // GET: Mutuales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mutuales == null)
            {
                return NotFound();
            }

            var mutuales = await _context.Mutuales
                .FirstOrDefaultAsync(m => m.MutId == id);
            if (mutuales == null)
            {
                return NotFound();
            }

            return View(mutuales);
        }

        // GET: Mutuales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mutuales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MutId,MutNombre,MutDescripcion,MutValor")] Mutual mutuales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mutuales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mutuales);
        }

        // GET: Mutuales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mutuales == null)
            {
                return NotFound();
            }

            var mutuales = await _context.Mutuales.FindAsync(id);
            if (mutuales == null)
            {
                return NotFound();
            }
            return View(mutuales);
        }

        // POST: Mutuales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MutId,MutNombre,MutDescripcion,MutValor")] Mutual mutuales)
        {
            if (id != mutuales.MutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mutuales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MutualesExists(mutuales.MutId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mutuales);
        }

        // GET: Mutuales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mutuales == null)
            {
                return NotFound();
            }

            var mutuales = await _context.Mutuales
                .FirstOrDefaultAsync(m => m.MutId == id);
            if (mutuales == null)
            {
                return NotFound();
            }

            return View(mutuales);
        }

        // POST: Mutuales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mutuales == null)
            {
                return Problem("Entity set 'GestionTurnosContext.Mutuales'  is null.");
            }
            var mutuales = await _context.Mutuales.FindAsync(id);
            if (mutuales != null)
            {
                _context.Mutuales.Remove(mutuales);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MutualesExists(int id)
        {
            return (_context.Mutuales?.Any(e => e.MutId == id)).GetValueOrDefault();
        }
    }
}
