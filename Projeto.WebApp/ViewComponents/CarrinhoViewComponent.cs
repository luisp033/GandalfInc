using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.WebApp.ViewComponents
{

    [Authorize]
    public class CarrinhoViewComponent : ViewComponent
    {
        protected readonly ProjetoDBContext _dbContext;
        public CarrinhoViewComponent(ProjetoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<DetalheVenda> model = new List<DetalheVenda>();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            var vendaResult = sistema.GetVendaEmCursoForUser(User.Identity.Name);

            if (!vendaResult.Sucesso)
            {
                //    EscreveMensagem(vendaResult.Mensagem, vendaResult.Sucesso);
                return null;
            }

            var resultado = sistema.GetDetalheVendasPorCompra(((Venda)vendaResult.Objeto).Identificador);
            if (resultado.Sucesso)
            {
                model = (List<DetalheVenda>)resultado.Objeto;
            }

            return await Task.FromResult((IViewComponentResult)View("Carrinho", model));

        }

    }
}
