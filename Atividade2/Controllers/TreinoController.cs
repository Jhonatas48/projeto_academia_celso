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

        // GET: Treinos
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

        // GET: Treinos/Details/5
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

        #region Create

        // GET: Treinos/Create
        public IActionResult Create()
        {
            Treino t = new Treino() { Exercicios = new List<Exercicio>()};

            ViewData["AlunoID"] = new SelectList(_context.Alunos, "AlunoID", "Nome");
            ViewData["PersonalID"] = new SelectList(_context.Personals, "PersonalID", "Nome");
            PopulateDadosExericiosDisponiveis(t);

            return View(t);
        }

        // POST: Treinos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Treino treino, int[] exerciciosSelecionados)
        {
            // Ajeitando as referências do treino
            treino.Aluno = _context.Alunos
                .Where(a => a.AlunoID == treino.AlunoID)
                .Single();
            treino.Personal = _context.Personals
                .Where(p => p.PersonalID == treino.PersonalID)
                .Single();
            treino.Exercicios = new List<Exercicio>();
            foreach (var exercicioID in exerciciosSelecionados)
                treino.Exercicios.Add(_context.Exercicios.Where(e => e.ExercicioID == exercicioID).Single());

            _context.Add(treino);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion Create

        #region Edit

        // GET: Treinos/Edit/5
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

        // POST: Treinos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Treino treinoRecebido, int[] exerciciosSelecionados) // note que o array é de IDs de exercicios selecionados
        {
            if (treinoRecebido == null)
                return NotFound();

            // Ajeitando as referências de treinoRecebido
            treinoRecebido.Aluno = _context.Alunos
                .Where(a => a.AlunoID == treinoRecebido.AlunoID)
                .Single();
            treinoRecebido.Personal = _context.Personals
                .Where(p => p.PersonalID == treinoRecebido.PersonalID)
                .Single();
            treinoRecebido.Exercicios = new List<Exercicio>();
            foreach (var exercicio in exerciciosSelecionados)
                treinoRecebido.Exercicios.Add(_context.Exercicios.Where(e => e.ExercicioID == exercicio).Single());

            var treinoToUpdate = _context.Treinos
                .Include(t => t.Aluno)
                .Include(t => t.Personal)
                .Include(t => t.Exercicios)
                .Where(t => t.TreinoID == treinoRecebido.TreinoID)
                .Single();

            try
            {
                UpdateTreino(treinoRecebido, treinoToUpdate);
                //treinoToUpdate = treinoRecebido;    // será que funfa? nope
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreinoExists(treinoToUpdate.TreinoID))
                    return NotFound();
                else
                    throw;
            }

            PopulateDadosExericiosDisponiveis(treinoToUpdate);
            return View(treinoToUpdate);
        }

        private void UpdateTreino(Treino treinoRecebido, Treino treinoToUpdate)
        {
            treinoToUpdate.Exercicios = treinoRecebido.Exercicios;
            treinoToUpdate.AlunoID = treinoRecebido.AlunoID;
            treinoToUpdate.Aluno = treinoRecebido.Aluno;
            treinoToUpdate.PersonalID = treinoRecebido.PersonalID;
            treinoToUpdate.Personal = treinoRecebido.Personal;
            treinoToUpdate.Hora = treinoRecebido.Hora;
            treinoToUpdate.Data = treinoRecebido.Data;
        }

        #endregion Edit

        #region Delete

        // GET: Treinos/Delete/5
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

        // POST: Treinos/Delete/5
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

        #endregion Delete

        #region código antigo do edit

        //{ ...
        //if (id == null)
        //{
        //    return NotFound();
        //}
        //var treinoToUpdate = _context.Treinos
        //    .Include(t => t.Aluno)
        //    .Include(t => t.Personal)
        //    .Include(t => t.Exercicios)
        //    .Where(t => t.TreinoID == id)
        //    .Single();

        //try
        //{
        //    UpdateExerciciosNoTreino(exerciciosSelecionados, treinoToUpdate);
        //    //_context.Update(treino);
        //    await _context.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException)
        //{
        //    if (!TreinoExists(treinoToUpdate.TreinoID))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        throw;
        //    }
        //}
        //PopulateDadosExericiosDisponiveis(treinoToUpdate);
        //return View(treinoToUpdate);
        //return View();
        //}

        //private void UpdateExerciciosNoTreino(string[] exerciciosSelecionados, Treino treinoToUpdate)
        //{
        //    if (exerciciosSelecionados == null)
        //    {
        //        treinoToUpdate.Exercicios = new List<Exercicio>();
        //        return;
        //    }

        //    var exerciciosSelecionadosHS = new HashSet<string>(exerciciosSelecionados);
        //    var exerciciosNoTreino = new HashSet<int>(treinoToUpdate.Exercicios.Select(e => e.ExercicioID));
        //    foreach (var exercicio in _context.Exercicios)
        //    {
        //        if (exerciciosSelecionadosHS.Contains(exercicio.ExercicioID.ToString()))    //se o ex tá selecionado, mas não tá no treino
        //        {
        //            if (!exerciciosNoTreino.Contains(exercicio.ExercicioID))
        //            {
        //                treinoToUpdate.Exercicios.Add(exercicio);       // adiciona no treino
        //            }
        //        }
        //        else
        //        {
        //            if (exerciciosNoTreino.Contains(exercicio.ExercicioID))     //se o ex NÃO tá selecionado, mas TÁ no treino
        //            {
        //                treinoToUpdate.Exercicios.Remove(exercicio);    // remove do treino
        //            }
        //        }
        //    }
        //}

        #endregion
    }
}
