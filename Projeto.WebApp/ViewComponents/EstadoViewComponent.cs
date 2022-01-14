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
    public class EstadoViewComponent : ViewComponent
    {
        protected readonly ProjetoDBContext _dbContext;
        public EstadoViewComponent(ProjetoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            EstadoViewModel model = new EstadoViewModel();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            var resultado = sistema.GetVendaEmCursoForUser(User.Identity.Name);
            if (resultado.Sucesso)
            {
                model.LojaNome = ((Venda)resultado.Objeto).PontoDeVendaSessao?.PontoDeVenda?.Loja.Nome;
                model.PosNome = ((Venda)resultado.Objeto).PontoDeVendaSessao.PontoDeVenda.Nome;
                model.UtilizadorNome = ((Venda)resultado.Objeto).PontoDeVendaSessao.Utilizador.Nome;
                model.SessaoData = ((Venda)resultado.Objeto).PontoDeVendaSessao.DataLogin;
            }

            return await Task.FromResult((IViewComponentResult)View("Estado", model));

        }

    }
}
