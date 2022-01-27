using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Dto;
using Projeto.DataAccessLayer.Entidades;
using Projeto.WebApp.Controllers;
using Projeto.WebApp.Filters;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.WebApp.Areas.Gestao.Controllers
{
    [Authorize]
    [Area("Gestao")]
    [ValidaTipoFilter(Tipo = DataAccessLayer.TipoUtilizadorEnum.Gerente)]
    public class GestaoController : BaseController
    {
        public GestaoController(ProjetoDBContext context) : base(context)
        {
        }

        public IActionResult Index()
        {

            var x = User.Identity.Name;

            return View();
        }

        [HttpPost]
        public JsonResult GraficoVendasPorCategoria()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<DataPie> vendasPorCategoria =  sistema.VendasPorCategoria();

            return new JsonResult(new
            {
                mylabels = vendasPorCategoria.Select(x => x.Label).ToList(),
                myCount = vendasPorCategoria.Select(x => x.Value).ToList(),
            });

        }
    }
}


