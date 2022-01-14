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


        //TODO -------------------- Estatisticas dos tipos de pagamento (necessario fazer pagamentos primeiro)
        //TODO -------------------- Mensagens no POS
        //TODO -------------------- Pagamento

    }
}
