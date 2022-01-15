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
using Projeto.DataAccessLayer.Auxiliar;

namespace Projeto.WebApp.ViewComponents
{

    [Authorize]
    public class TotaisSessaoViewComponent : ViewComponent
    {
        protected readonly ProjetoDBContext _dbContext;
        public TotaisSessaoViewComponent(ProjetoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<TotalSessao> model = new List<TotalSessao>();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            var vendaResult = sistema.GetVendaEmCursoForUser(User.Identity.Name);

            if (!vendaResult.Sucesso)
            {
                //    EscreveMensagem(vendaResult.Mensagem, vendaResult.Sucesso);
                return null;
            }

            model = sistema.GetTotaisSessao(((Venda)vendaResult.Objeto).PontoDeVendaSessao.Identificador);

            return await Task.FromResult((IViewComponentResult)View("TotaisSessao", model));

        }

    }
}
