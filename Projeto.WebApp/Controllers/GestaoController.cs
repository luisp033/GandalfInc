using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.WebApp.Filters;

namespace Projeto.WebApp.Controllers
{
    [Authorize]
    [ValidaTipoFilter(Tipo = DataAccessLayer.TipoUtilizadorEnum.Gerente)]
    public class GestaoController : Controller
    {
        public IActionResult Index()
        {

            var x = User.Identity.Name;

            return View();
        }
    }
}
