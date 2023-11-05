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
    public class TurnosController : Controller
    {
        private readonly GestionTurnosContext _context;

        public TurnosController(GestionTurnosContext context)
        {
            _context = context;
        }

        // GET: Turnoes
        public async Task<IActionResult> Index()
        {
            var gestionTurnosContext = _context.Turnos.Include(t => t.Cli).Include(t => t.Med).Include(t => t.Mut);
            return View(await gestionTurnosContext.ToListAsync());
        }

        // GET: Turnoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Cli)
                .Include(t => t.Med)
                .Include(t => t.Mut)
                .FirstOrDefaultAsync(m => m.TurId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnoes/Create
        public IActionResult Create()
        {

            ViewData["CliId"] = new SelectList(
                _context.Set<Cliente>()
                .Select(cliente => new
                {
                    cliente.CliId,
                    CliNombreCompleto = $"{cliente.CliApellido}, {cliente.CliNombre}"
                }),
                "CliId", "CliNombreCompleto");
            ViewData["MedId"] = new SelectList(
                _context.Set<Medico>()
                .Select(medico => new
                {
                    medico.MedId,
                    MedNombreCompleto = $"{medico.MedApellido}, {medico.MedNombre}"
                }),
                "MedId", "MedNombreCompleto");
            ViewData["MutId"] = new SelectList(_context.Mutuales, "MutId", "MutNombre");

            return View();
        }

        // POST: Turnoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurId,CliId,MedId,MutId,TurFecha,TurValor,TurPagoEfectivo,TurDescuentaPrepaga")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _context.Add(turno);
            await _context.SaveChangesAsync();
            ViewData["CliId"] = new SelectList(_context.Clientes, "CliId", "CliId", turno.CliId);
            ViewData["MedId"] = new SelectList(_context.Medicos, "MedId", "MedId", turno.MedId);
            ViewData["MutId"] = new SelectList(_context.Mutuales, "MutId", "MutId", turno.MutId);
            return View(turno);
        }

        // GET: Turnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewData["CliId"] = new SelectList(_context.Clientes, "CliId", "CliId", turno.CliId);
            ViewData["MedId"] = new SelectList(_context.Medicos, "MedId", "MedId", turno.MedId);
            ViewData["MutId"] = new SelectList(_context.Mutuales, "MutId", "MutId", turno.MutId);
            return View(turno);
        }

        // POST: Turnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurId,CliId,MedId,MutId,TurFecha,TurValor,TurPagoEfectivo,TurDescuentaPrepaga")] Turno turno)
        {
            if (id != turno.TurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.TurId))
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
            ViewData["CliId"] = new SelectList(_context.Clientes, "CliId", "CliId", turno.CliId);
            ViewData["MedId"] = new SelectList(_context.Medicos, "MedId", "MedId", turno.MedId);
            ViewData["MutId"] = new SelectList(_context.Mutuales, "MutId", "MutId", turno.MutId);
            return View(turno);
        }

        // GET: Turnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .Include(t => t.Cli)
                .Include(t => t.Med)
                .Include(t => t.Mut)
                .FirstOrDefaultAsync(m => m.TurId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Turnos == null)
            {
                return Problem("Entity set 'GestionTurnosContext.Turnos'  is null.");
            }
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
          return (_context.Turnos?.Any(e => e.TurId == id)).GetValueOrDefault();
        }
    }
}
