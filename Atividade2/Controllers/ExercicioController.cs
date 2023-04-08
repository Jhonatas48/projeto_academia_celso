using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Atividade2.Models;

namespace Atividade2.Controllers
{
    public class ExercicioController : Controller
    {
        private readonly Context _context;

        public ExercicioController(Context context)
        {
            _context = context;
        }

        // GET: Exercicios
        public async Task<IActionResult> Index()
        {
              return _context.Exercicios != null ? 
                          View(await _context.Exercicios.ToListAsync()) :
                          Problem("Entity set 'Context.Exercicios'  is null.");
        }

        // GET: Exercicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exercicios == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios
                .FirstOrDefaultAsync(m => m.ExercicioID == id);
            if (exercicio == null)
            {
                return NotFound();
            }

            return View(exercicio);
        }

        // GET: Exercicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exercicio exercicio)
        {
            _context.Add(exercicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Exercicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exercicios == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null)
            {
                return NotFound();
            }
            return View(exercicio);
        }

        // POST: Exercicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Exercicio exercicio)
        {
            if (exercicio == null)
            {
                return NotFound();
            }

            try
            {
                _context.Update(exercicio);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExercicioExists(exercicio.ExercicioID))
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

        // GET: Exercicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exercicios == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios
                .FirstOrDefaultAsync(m => m.ExercicioID == id);
            if (exercicio == null)
            {
                return NotFound();
            }

            return View(exercicio);
        }

        // POST: Exercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exercicios == null)
            {
                return Problem("Entity set 'Context.Exercicios'  is null.");
            }
            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio != null)
            {
                _context.Exercicios.Remove(exercicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExercicioExists(int id)
        {
          return (_context.Exercicios?.Any(e => e.ExercicioID == id)).GetValueOrDefault();
        }
    }
}
