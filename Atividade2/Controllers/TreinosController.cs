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
    public class TreinosController : Controller
    {
        private readonly Context _context;

        public TreinosController(Context context)
        {
            _context = context;
        }

        // GET: Treinoes
        public async Task<IActionResult> Index()
        {
            var context = _context.Treinos.Include(t => t.Aluno);
            return View(await context.ToListAsync());
        }

        // GET: Treinoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Treinos == null)
            {
                return NotFound();
            }

            var treino = await _context.Treinos
                .Include(t => t.Aluno)
                .FirstOrDefaultAsync(m => m.TreinoID == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // GET: Treinoes/Create
        public IActionResult Create()
        {
            ViewData["AlunoID"] = new SelectList(_context.Alunos, "AlunoID", "AlunoID");
            return View();
        }

        // POST: Treinoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Treino treino)
        {
            _context.Add(treino);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["AlunoID"] = new SelectList(_context.Alunos, "AlunoID", "AlunoID", treino.AlunoID);
            return View(treino);
        }

        // GET: Treinoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Treinos == null)
            {
                return NotFound();
            }

            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            ViewData["AlunoID"] = new SelectList(_context.Alunos, "AlunoID", "AlunoID", treino.AlunoID);
            return View(treino);
        }

        // POST: Treinoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Treino treino)
        {
            if (treino == null)
            {
                return NotFound();
            }

            try
            {
                _context.Update(treino);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreinoExists(treino.TreinoID))
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

        // GET: Treinoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Treinos == null)
            {
                return NotFound();
            }

            var treino = await _context.Treinos
                .Include(t => t.Aluno)
                .FirstOrDefaultAsync(m => m.TreinoID == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // POST: Treinoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Treinos == null)
            {
                return Problem("Entity set 'Context.Treinos'  is null.");
            }
            var treino = await _context.Treinos.FindAsync(id);
            if (treino != null)
            {
                _context.Treinos.Remove(treino);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinoExists(int id)
        {
          return (_context.Treinos?.Any(e => e.TreinoID == id)).GetValueOrDefault();
        }
    }
}
