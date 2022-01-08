using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;

namespace Projeto.WebApp.Areas.Gestao.Controllers
{

    [Area("Gestao")]
    public class UtilizadorController : Controller
    {
        public ActionResult Index()
        {
            LogicaSistema sistema = new LogicaSistema(new DataAccessLayer.ProjetoDBContext());
            List<Utilizador> model = sistema.GetAllUtilizadores();

            foreach (var item in model)
            {
                item.Senha = String.Empty;
            }
            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            Utilizador model = new Utilizador();
            LogicaSistema sistema = new LogicaSistema(new DataAccessLayer.ProjetoDBContext());

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
                LogicaSistema sistema = new LogicaSistema(new ProjetoDBContext());

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
            LogicaSistema sistema = new LogicaSistema(new DataAccessLayer.ProjetoDBContext());

            model = (Utilizador)(sistema.ObtemUtilizador(id).Objeto);
            ViewBag.VBTipoUtilizadores = GetTipoUtilizador();

            return View(model);
        }

        // POST: UtilizadorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(new DataAccessLayer.ProjetoDBContext());
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
