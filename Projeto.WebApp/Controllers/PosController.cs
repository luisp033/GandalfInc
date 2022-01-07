using Microsoft.AspNetCore.Mvc;
using Projeto.WebApp.Filters;

namespace Projeto.WebApp.Controllers
{

    [ValidaTipoFilter(Tipo = DataAccessLayer.TipoUtilizadorEnum.Empregado)]
    public class PosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
