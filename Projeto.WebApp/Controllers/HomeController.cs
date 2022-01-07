using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {

            var contexto = new ProjetoDBContext();
            LogicaSistema sistema = new LogicaSistema(contexto);
            var resultado = sistema.ObtemTipoUtilizadorByEmail(User.Identity.Name);
            TipoUtilizadorEnum tip = (TipoUtilizadorEnum)resultado.Objeto;

            if (tip == TipoUtilizadorEnum.Gerente)
            {
                return RedirectToAction("Index", "Gestao");
            }
            if (tip == TipoUtilizadorEnum.Empregado)
            {
                return RedirectToAction("Index", "Pos");
            }

            return View();
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
    }
}
