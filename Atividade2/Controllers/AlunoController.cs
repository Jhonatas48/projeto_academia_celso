using Microsoft.AspNetCore.Mvc;
using Atividade2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Atividade2.Controllers
{
    public class AlunoController : Controller
    {
        public Context context;
        public AlunoController(Context ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return View(context.Alunos.Include(p => p.Personal));
        }

        #region Create

        public IActionResult Create()
        {
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(p => p.Nome), "PersonalID", "Nome");
            //ainda tenho que dar um jeito de mandar os treinos
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aluno aluno)
        {
            context.Add(aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion Create

        #region Edit

        public IActionResult Edit(int id)
        {
            Aluno aluno = context.Alunos.Find(id);
            ViewBag.FabricanteID = new SelectList(context.Personals);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Aluno aluno)
        {
            context.Entry(aluno).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion Edit

        #region Delete

        public IActionResult Delete(int id)
        {
            var aluno = context.Alunos.Find(id);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Aluno aluno)
        {
            context.Remove(aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion Delete

        #region Details

        public IActionResult Details(int id)
        {
            var aluno = context.Alunos.Find(id);
            return View(aluno);
        }

        #endregion Details
    }
}
