using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Atividade2.Models;

namespace Atividade2.Controllers
{
    public static class Verify
    {
        public static bool Session(HttpContext httpCtx)
        {
            var acesso = httpCtx.Session.GetString("usuario_session");
            if (acesso != null)
                    return false;
            else
                return true;
        }
    }
}
