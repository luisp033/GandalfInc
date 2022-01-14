using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.WebApp.Controllers
{
  
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ProjetoDBContext context) : base(context)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            var resultado = sistema.ObtemTipoUtilizadorByEmail(User.Identity.Name);
            TipoUtilizadorEnum tip = (TipoUtilizadorEnum)resultado.Objeto;

            if (tip == TipoUtilizadorEnum.Gerente)
            {
                return RedirectToAction("Index", "Gestao");
            }

            ViewBag.VBLojasComPos = GetLojas();


            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Guid lojaId, Guid pontoDeVendasId)
        {

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            var pontoVendaResult = sistema.ObtemPontoDeVenda(pontoDeVendasId);
            if (!pontoVendaResult.Sucesso)
            {
                //MessageBox.Show(pontoVendaResult.Mensagem);
                TempData["AberturaPosFalhou"] = pontoVendaResult.Mensagem;
                return RedirectToAction("Index", "Home");
            }

            var resultadoUtilizador = sistema.ObtemUtilizador(User.Identity.Name);

            var aberturaResult = sistema.AberturaSessao((Utilizador)resultadoUtilizador.Objeto, (PontoDeVenda)pontoVendaResult.Objeto);
            if (!aberturaResult.Sucesso)
            {
                TempData["AberturaPosFalhou"] = aberturaResult.Mensagem;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Pos");
        }

        public IActionResult Sobre()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<SelectListItem> GetLojas()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<Loja> lista = sistema.GetAllLojasComPontosDeVenda();

            foreach (var item in lista)
            {
                result.Add(new SelectListItem { Text = item.Nome, Value = item.Identificador.ToString() });
            }
            return result;
        }

        public JsonResult GetPosByLoja(Guid lojaId)
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<PontoDeVenda> lista = sistema.GetAllPontoDeVendasByLoja(lojaId);

            return Json(new SelectList(lista,"Identificador","Nome"));

        }

    }
}
