using Microsoft.AspNetCore.Mvc;
using Projeto.WebApp.Filters;

namespace Projeto.WebApp.Controllers
{

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
