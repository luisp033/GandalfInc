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
    public class ProdutoController : BaseController
    {

        public ProdutoController(ProjetoDBContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<Produto> model = sistema.GetAllProdutos();

            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            Produto model = new Produto();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            if (id != null)
            {
                model = (Produto)(sistema.ObtemProduto(id.Value).Objeto);
            }

            ViewBag.VBMarcas = GetMarcas();
            ViewBag.VBCategorias = GetCategorias();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Produto model)
        {
            ModelState.Remove("Categoria.Nome");
            ModelState.Remove("Marca.Nome");

            if (ModelState.IsValid)
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);

                if (id == Guid.Empty)
                {
                    sistema.InsereProduto(model.Nome, model.Categoria.Identificador, model.Marca.Identificador,model.Ean, model.PrecoUnitario);
                }
                else
                {
                    sistema.AlteraProduto(id,model.Nome, model.Categoria.Identificador, model.Marca.Identificador, model.Ean, model.PrecoUnitario);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.VBMarcas = GetMarcas();
            ViewBag.VBCategorias = GetCategorias();

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            Produto model = new Produto();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (Produto)(sistema.ObtemProduto(id).Objeto);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);
                sistema.ApagaProduto(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<SelectListItem> GetMarcas()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<MarcaProduto> lista = sistema.GetAllMarcas();

            foreach (var item in lista)
            {
                result.Add(new SelectListItem { Text = item.Nome, Value = item.Identificador.ToString() });
            }
            return result;
        }

        private List<SelectListItem> GetCategorias()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<CategoriaProduto> lista = sistema.GetAllCategorias();

            foreach (var item in lista)
            {
                result.Add(new SelectListItem { Text = item.Nome, Value = item.Identificador.ToString() });
            }
            return result;
        }

    }
}
