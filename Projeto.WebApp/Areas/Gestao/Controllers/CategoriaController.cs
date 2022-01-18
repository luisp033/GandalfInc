using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Projeto.WebApp.Areas.Gestao.Controllers
{

    [Area("Gestao")]
    public class CategoriaController : BaseController
    {

        public CategoriaController(ProjetoDBContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<CategoriaProduto> model = sistema.GetAllCategorias();

            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            CategoriaProduto model = new CategoriaProduto();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            if (id != null)
            {
                model = (CategoriaProduto)(sistema.ObtemCategoria(id.Value).Objeto);

                ViewBag.ImageDataUrl = Util.Util.GetUrlImageFormByteArray(model.ImageData);

            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, CategoriaProduto model)
        {
            if (ModelState.IsValid)
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);

                foreach (var file in Request.Form.Files)
                {
                    var ImageTitle = file.FileName;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        model.ImageData = ms.ToArray();
                        ms.Close();
                    }
                }

                if (id == Guid.Empty)
                {
                    sistema.InsereCategoria(model.Nome, model.OrdemApresentacao, model.ImageData);
                }
                else
                {
                    sistema.AlteraCategoria(id, model.Nome, model.OrdemApresentacao, model.ImageData);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            CategoriaProduto model = new CategoriaProduto();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (CategoriaProduto)(sistema.ObtemCategoria(id).Objeto);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);
                sistema.ApagaCategoria(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
