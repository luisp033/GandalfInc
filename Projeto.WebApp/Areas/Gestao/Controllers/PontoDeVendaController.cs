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

    [Area("Gestao")]
    public class PontoDeVendaController : BaseController
    {

        public PontoDeVendaController(ProjetoDBContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<PontoDeVenda> model = sistema.GetAllPontoDeVendas();

            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            PontoDeVenda model = new PontoDeVenda();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            if (id != null)
            {
                model = (PontoDeVenda)(sistema.ObtemPontoDeVenda(id.Value).Objeto);
            }

            ViewBag.VBLojas = GetLojas();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, PontoDeVenda model)
        {
            ModelState.Remove("Loja.Nome");

            if (ModelState.IsValid)
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);

                if (id == Guid.Empty)
                {
                    sistema.InserePontoDeVenda(model.Nome,model.Loja);
                }
                else
                {
                    sistema.AlteraPontoDeVenda(id,model.Nome, model.Loja.Identificador);
                }

                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.VBLojas = GetLojas();
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            PontoDeVenda model = new PontoDeVenda();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (PontoDeVenda)(sistema.ObtemPontoDeVenda(id).Objeto);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);
                sistema.ApagaPontoDeVenda(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<SelectListItem> GetLojas()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<Loja> lista = sistema.GetAllLojas();

            foreach (var item in lista)
            {
                result.Add(new SelectListItem { Text = item.Nome, Value = item.Identificador.ToString() });
            }
            return result;
        }

    }
}
