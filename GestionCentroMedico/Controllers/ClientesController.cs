using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionCentroMedico.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionCentroMedico.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly GestionTurnosContext _context;

        public ClientesController(GestionTurnosContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return _context.Clientes != null ?
                        View(await _context.Clientes.ToListAsync()) :
                        Problem("Entity set 'GestionTurnosContext.Clientes'  is null.");

        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.CliId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
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

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CliId,CliNombre,CliApellido,CliEmail,MedId,MutId,CliActivo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            ViewData["MedId"] = new SelectList(_context.Medicos, "MedId", "MedId", cliente.MedId);
            ViewData["MutId"] = new SelectList(_context.Mutuales, "MutId", "MutId", cliente.MutId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["MedId"] = new SelectList(
              _context.Set<Medico>()
              .Select(medico => new
              {
                  medico.MedId,
                  MedNombreCompleto = $"{medico.MedApellido}, {medico.MedNombre}"
              }),
              "MedId", "MedNombreCompleto");
            ViewData["MutId"] = new SelectList(_context.Mutuales, "MutId", "MutNombre");

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CliId,CliNombre,CliApellido,CliEmail,MedId,MutId,CliActivo")] Cliente cliente)
        {
            //if (id != cliente.CliId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.CliId))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.CliId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'GestionTurnosContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.CliId == id)).GetValueOrDefault();
        }
    }
}
