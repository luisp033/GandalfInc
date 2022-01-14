using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Projeto.WebApp.Models;

namespace Projeto.WebApp.ViewComponents
{

    [Authorize]
    public class TotaisViewComponent : ViewComponent
    {
        protected readonly ProjetoDBContext _dbContext;
        public TotaisViewComponent(ProjetoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            TotaisViewModel model = new TotaisViewModel();
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
                model.ItemsCompra = ((List<DetalheVenda>)resultado.Objeto).Count;
                model.TotalCompra = ((List<DetalheVenda>)resultado.Objeto).Sum(x => x.PrecoFinal);
            }

            return await Task.FromResult((IViewComponentResult)View("Totais", model));

        }

    }
}
