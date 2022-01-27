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
    public class LojaController : BaseController
    {

        public LojaController(ProjetoDBContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<Loja> model = sistema.GetAllLojas();

            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            Loja model = new Loja();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            if (id != null)
            {
                model = (Loja)(sistema.ObtemLoja(id.Value).Objeto);
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Loja model)
        {

            if (ModelState.IsValid)
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);

                if (id == Guid.Empty)
                {
                    sistema.InsereLoja(model.Nome, model.NumeroFiscal, model.Email,model.Telefone,null);
                }
                else
                {
                    sistema.AlteraLoja(id,model.Nome, model.NumeroFiscal, model.Email, model.Telefone, null);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            Loja model = new Loja();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (Loja)(sistema.ObtemLoja(id).Objeto);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);
                sistema.ApagaLoja(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
