using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Atividade2.Models;
using Atividade2.ViewModels;

namespace Atividade2.Controllers
{
    public class TreinoController : Controller
    {
        private readonly Context _context;

        public TreinoController(Context context)
        {
            _context = context;
        }

        // GET: Treinoes
        public async Task<IActionResult> Index(int? treinoID)
        {
            var context = _context.Treinos
                .Include(t => t.Aluno)
                .Include(t => t.Exercicios)
                .Include(t => t.Personal);

            if (treinoID != null)
            {
                ViewBag.TreinoID = treinoID.Value;
            }

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
            ViewData["AlunoID"] = new SelectList(_context.Alunos, "AlunoID", "Nome");
            ViewData["PersonalID"] = new SelectList(_context.Personals, "PersonalID", "Nome");
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

            Treino treino = _context.Treinos.Include(t => t.Exercicios).Where(t => t.TreinoID == id).Single();
            if (treino == null)
            {
                return NotFound();
            }

            //ViewData["AlunoID"] = new SelectList(_context.Alunos, "AlunoID", "AlunoID", treino.AlunoID);
            
            PopulateDadosExericiosDisponiveis(treino);
            return View(treino);
        }

        private void PopulateDadosExericiosDisponiveis(Treino treino)
        {
            ViewData["AlunoID"] = new SelectList(_context.Alunos, "AlunoID", "Nome");
            ViewData["PersonalID"] = new SelectList(_context.Personals, "PersonalID", "Nome");

            var allExercicios = _context.Exercicios;
            var exerciciosNoTreino = new HashSet<int>(treino.Exercicios.Select(e => e.ExercicioID));
            var viewModel = new List<DadosExerciciosDisponiveis>();
            foreach (var exercicio in allExercicios)
            {
                viewModel.Add(new DadosExerciciosDisponiveis
                {
                    ExercicioID = exercicio.ExercicioID,
                    Nome = exercicio.Nome,
                    Descricao = exercicio.Descricao,
                    Incluido = exerciciosNoTreino.Contains(exercicio.ExercicioID)
                });
            }
            ViewBag.Exercicios = viewModel;
        }

        // POST: Treinoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] exerciciosSelecionados)
        {
            if (id == null)
            {
                return NotFound();
            }
            var treinoToUpdate = _context.Treinos
                .Include(t => t.Aluno)
                .Include(t => t.Personal)
                .Include(t => t.Exercicios)
                .Where(t => t.TreinoID == id)
                .Single();

            try
            {
                UpdateExerciciosNoTreino(exerciciosSelecionados, treinoToUpdate);
                //_context.Update(treino);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreinoExists(treinoToUpdate.TreinoID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            PopulateDadosExericiosDisponiveis(treinoToUpdate);
            return View(treinoToUpdate);
        }

        private void UpdateExerciciosNoTreino(string[] exerciciosSelecionados, Treino treinoToUpdate)
        {
            if (exerciciosSelecionados == null)
            {
                treinoToUpdate.Exercicios = new List<Exercicio>();
                return;
            }

            var exerciciosSelecionadosHS = new HashSet<string>(exerciciosSelecionados);
            var exerciciosNoTreino = new HashSet<int>(treinoToUpdate.Exercicios.Select(e => e.ExercicioID));
            foreach (var exercicio in _context.Exercicios)
            {
                if (exerciciosSelecionadosHS.Contains(exercicio.ExercicioID.ToString()))
                {
                    if (!exerciciosNoTreino.Contains(exercicio.ExercicioID))
                    {
                        treinoToUpdate.Exercicios.Add(exercicio);
                    }
                }
                else
                {
                    if (exerciciosNoTreino.Contains(exercicio.ExercicioID))
                    {
                        treinoToUpdate.Exercicios.Remove(exercicio);
                    }
                }
            }
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
