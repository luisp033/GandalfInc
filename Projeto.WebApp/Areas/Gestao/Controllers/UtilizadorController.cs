using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.WebApp.Controllers;
using System;
using System.Collections.Generic;

namespace Projeto.WebApp.Areas.Gestao.Controllers
{
    [Authorize]
    [Area("Gestao")]
    public class UtilizadorController : BaseController
    {

        public UtilizadorController(ProjetoDBContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<Utilizador> model = sistema.GetAllUtilizadores();

            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0
            //var myValor = HttpContext.Session.GetString("MyVariable");

            foreach (var item in model)
            {
                item.Senha = String.Empty;
            }
            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            Utilizador model = new Utilizador();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            if (id != null)
            {
                model = (Utilizador)(sistema.ObtemUtilizador(id.Value).Objeto);
            }

            ViewBag.VBTipoUtilizadores = GetTipoUtilizador();

            return View(model);
        }

        private List<SelectListItem> GetTipoUtilizador()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            var lista = (TipoUtilizadorEnum[])Enum.GetValues(typeof(TipoUtilizadorEnum));

            for (int i = 0; i < lista.Length; i++)
            {
                result.Add(new SelectListItem { Text = lista[i].ToString(), Value = i.ToString() });
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Utilizador model)
        {
            ModelState.Remove("Tipo.Name");

            if (ModelState.IsValid)
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);

                if (id == Guid.Empty)
                {
                    sistema.InsereUtilizador(model.Nome, model.Email, model.Senha, (TipoUtilizadorEnum)model.Tipo.TipoId);
                }
                else
                {
                    sistema.AlteraUtilizador(id, model.Nome, model.Email, model.Senha, (TipoUtilizadorEnum)model.Tipo.TipoId);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.VBTipoUtilizadores = GetTipoUtilizador();
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            Utilizador model = new Utilizador();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (Utilizador)(sistema.ObtemUtilizador(id).Objeto);
            ViewBag.VBTipoUtilizadores = GetTipoUtilizador();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);
                sistema.ApagaUtilizador(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
