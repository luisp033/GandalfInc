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
    public class ProdutosViewComponent: ViewComponent
    {
        protected readonly ProjetoDBContext _dbContext;
        public ProdutosViewComponent(ProjetoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid? id)
        {
            List<Produto> model = new List<Produto>();
            LogicaSistema sistema = new LogicaSistema(_dbContext);

            if (id != null && id != Guid.Empty) 
            {
                var resultado =sistema.ObtemProdutosPorCategoria(id.Value);
                if (resultado.Sucesso)
                { 
                    model = (List<Produto>)resultado.Objeto;
                }
            }
            else
            {
                model = sistema.GetAllProdutos();
            }
            return await Task.FromResult((IViewComponentResult)View("Produtos", model));


        }

    }
}
