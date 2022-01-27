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
    public class MarcaController : BaseController
    {

        public MarcaController(ProjetoDBContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<MarcaProduto> model = sistema.GetAllMarcas();

            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            MarcaProduto model = new MarcaProduto();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            if (id != null)
            {
                model = (MarcaProduto)(sistema.ObtemMarca(id.Value).Objeto);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, MarcaProduto model)
        {
            if (ModelState.IsValid)
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);

                if (id == Guid.Empty)
                {
                    sistema.InsereMarca(model.Nome, model.Origem);
                }
                else
                {
                    sistema.AlteraMarca(id,model.Nome, model.Origem);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            MarcaProduto model = new MarcaProduto();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (MarcaProduto)(sistema.ObtemMarca(id).Objeto);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);
                sistema.ApagaMarca(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
