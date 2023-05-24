using Atividade2.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Atividade2.Controllers
{
    public class SessionController : Controller
    {
        public Context _ctx;

        public SessionController(Context ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var exists = _ctx.Alunos.Where(a => a.Email.Equals(email)).FirstOrDefault();
            if (exists != null)
            {
                var confirm = BCrypt.Net.BCrypt.Verify(senha, exists.Senha);
                if (confirm)
                {
                    HttpContext.Session.SetString("usuario_session", exists.Nome);
                    HttpContext.Session.SetString("usuario_tipo", "aluno");
                    return RedirectToAction(nameof(Success));
                }
            }
            var exists2 = _ctx.Personals.Where(p => p.Email.Equals(email)).FirstOrDefault();
            if (exists2 != null)
            {
                var confirm = BCrypt.Net.BCrypt.Verify(senha, exists2.Senha);
                if (confirm)
                {
                    HttpContext.Session.SetString("usuario_session", exists2.Nome);
                    HttpContext.Session.SetString("usuario_tipo", "personal");
                    return RedirectToAction(nameof(Success));
                }
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult CreateAluno()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAluno(Aluno aluno)
        {
            aluno.Senha = BCrypt.Net.BCrypt.HashPassword(aluno.Senha);
            _ctx.Alunos.Add(aluno);
            _ctx.SaveChanges();
            return RedirectToAction(nameof(AlunoController.Index), "Aluno");
        }

        public IActionResult CreatePersonal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePersonal(Personal personal)
        {
            personal.Senha = BCrypt.Net.BCrypt.HashPassword(personal.Senha);
            _ctx.Personals.Add(personal);
            _ctx.SaveChanges();
            return RedirectToRoute(nameof(PersonalController.Index), "Personal");
        }
    }
}
