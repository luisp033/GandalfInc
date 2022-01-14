using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.WebApp.Filters;
using Projeto.WebApp.Models;
using System;

namespace Projeto.WebApp.Controllers
{
    [Authorize]
    [ValidaTipoFilter(Tipo = DataAccessLayer.TipoUtilizadorEnum.Empregado)]
    public class PosController : BaseController
    {

        private readonly ILogger<HomeController> _logger;

        public PosController(ILogger<HomeController> logger, ProjetoDBContext context) : base(context)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region ViewComponents   
        public IActionResult ReloadProdutosByCategoria(Guid id)
        {
            return ViewComponent("Produtos", new { id = id });
        }

        public IActionResult AddProduto(Guid id)
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            var vendaResult = sistema.GetVendaEmCursoForUser(User.Identity.Name);

            if (!vendaResult.Sucesso)
            {
                Mensagem(vendaResult);
                return null;
            }
            var produtoAdicionado = sistema.AddProdutoVenda(((Venda)vendaResult.Objeto).Identificador, id);

            Mensagem(produtoAdicionado);
            return ViewComponent("Carrinho");
        }

        public IActionResult DelProduto(Guid id)
        {

            LogicaSistema sistema = new LogicaSistema(_dbContext);

            var produtoRemovido = sistema.DeleteDetalheVenda(id);

            Mensagem(produtoRemovido);

            return ViewComponent("Carrinho");
        }

        public IActionResult Totais()
        {
            return ViewComponent("Totais");
        }

        public IActionResult MensagemPos()
        {
            return ViewComponent("MensagemPos");
        }

        #endregion

        private void Mensagem(Resultado resultado)
        {
            if (resultado.Sucesso)
            {
                TempData["MensagemPosSucesso"] = resultado.Mensagem;
            }
            else
            {
                TempData["MensagemPosErro"] = resultado.Mensagem;
            }
        }

        public IActionResult FecharSessao()
        {

            LogicaSistema sistema = new LogicaSistema(_dbContext);

            var vendaResult = sistema.GetVendaEmCursoForUser(User.Identity.Name);

            var resultado = sistema.FechaSessaoVenda(((Venda)vendaResult.Objeto).PontoDeVendaSessao.Identificador);

            if (!resultado.Sucesso)
            {
                Mensagem(resultado);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult CancelarCompra()
        {

            LogicaSistema sistema = new LogicaSistema(_dbContext);

            var vendaResult = sistema.GetVendaEmCursoForUser(User.Identity.Name);

            var detalheVendaResult = sistema.DeleteAllDetalheVendasPorCompra(((Venda)vendaResult.Objeto).Identificador);

            Mensagem(detalheVendaResult);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FinalizarCompra(PagamentoViewModel model)
        {
            model.Pago = String.Empty;
            if (model.Tipo == DataAccessLayer.Enumerados.TipoPagamentoEnum.MbWay && String.IsNullOrEmpty(model.Telefone))
            {
                ModelState.AddModelError("Telefone", "Telefone obrigatório para pagamento com MB WAY");
                return PartialView("_DetalhePagamento", model);
            }

            LogicaSistema sistema = new LogicaSistema(_dbContext);
            var vendaResult = sistema.GetVendaEmCursoForUser(User.Identity.Name);
            var resultado = sistema.Pagamento(((Venda)vendaResult.Objeto).Identificador, model.Nome, model.NumeroContribuinte, model.Telefone, model.Tipo);

            if (resultado.Sucesso)
            {
                ModelState.Clear();
                model.Pago = "PagamentoEfetuadoComSucesso"; //serve para fechar a modal
            }
            else
            { 
                ModelState.AddModelError("Pago",resultado.Mensagem);
            }

            return PartialView("_DetalhePagamento",model);
        }

        //TODO -------------------- Estatisticas dos tipos de pagamento (necessario fazer pagamentos primeiro)
        //TODO -------------------- Embonecar o POS (logotipo)
        //TODO -------------------- Recibos
        //TODO -------------------- Graficos na Gestão
        //TODO -------------------- Estoque com datatables ou pagaincao serverside

    }
}
