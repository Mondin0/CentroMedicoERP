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
    public class MedicosController : Controller
    {
        private readonly GestionTurnosContext _context;

        public MedicosController(GestionTurnosContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            return _context.Medicos != null ?
                        View(await _context.Medicos.ToListAsync()) :
                        Problem("Entity set 'GestionTurnosContext.Medicos'  is null.");
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medicos = await _context.Medicos
                .FirstOrDefaultAsync(m => m.MedId == id);
            if (medicos == null)
            {
                return NotFound();
            }

            return View(medicos);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedId,MedNombre,MedApellido,MedEspecialidad,MedMatricula")] Medico medicos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicos);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medicos = await _context.Medicos.FindAsync(id);
            if (medicos == null)
            {
                return NotFound();
            }
            return View(medicos);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedId,MedNombre,MedApellido,MedEspecialidad,MedMatricula")] Medico medicos)
        {
            if (id != medicos.MedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicosExists(medicos.MedId))
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
            return View(medicos);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicos == null)
            {
                return NotFound();
            }

            var medicos = await _context.Medicos
                .FirstOrDefaultAsync(m => m.MedId == id);
            if (medicos == null)
            {
                return NotFound();
            }

            return View(medicos);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicos == null)
            {
                return Problem("Entity set 'GestionTurnosContext.Medicos'  is null.");
            }
            var medicos = await _context.Medicos.FindAsync(id);
            if (medicos != null)
            {
                _context.Medicos.Remove(medicos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicosExists(int id)
        {
            return (_context.Medicos?.Any(e => e.MedId == id)).GetValueOrDefault();
        }
    }
}
