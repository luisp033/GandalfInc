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
    public class EstoqueController : BaseController
    {
        public EstoqueController(ProjetoDBContext context) : base(context)
        {
        }

        public ActionResult Index()
        {

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<Estoque> model = sistema.GetAllEstoque();

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.VBProdutos = GetProdutos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid produtoId, int quantidade)
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            sistema.InsereEstoque(produtoId, quantidade);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(Guid id)
        {
            Estoque model = new Estoque();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (Estoque)(sistema.ObtemEstoque(id).Objeto);

            //ViewBag.VBMarcas = GetMarcas();
            //ViewBag.VBCategorias = GetCategorias();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Estoque model)
        {
            ModelState.Remove("Produto.Marca");
            ModelState.Remove("Produto.Categoria");

            if (ModelState.IsValid)
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);

                sistema.AlteraEstoque(id, model.NumeroSerie);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            Estoque model = new Estoque();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            model = (Estoque)(sistema.ObtemEstoque(id).Objeto);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                LogicaSistema sistema = new LogicaSistema(_dbContext);
                sistema.ApagaEstoque(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<SelectListItem> GetProdutos()
        {
            List<SelectListItem> result = new List<SelectListItem>();

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            List<Produto> lista = sistema.GetAllProdutos();

            foreach (var item in lista)
            {
                result.Add(new SelectListItem { Text = item.Nome, Value = item.Identificador.ToString() });
            }
            return result;
        }

    }
}
