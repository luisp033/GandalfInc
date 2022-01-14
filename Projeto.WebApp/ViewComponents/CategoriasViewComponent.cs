using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using System.Threading.Tasks;

namespace Projeto.WebApp.ViewComponents
{
    [Authorize]
    public class CategoriasViewComponent: ViewComponent
    {
        protected readonly ProjetoDBContext _dbContext;
        public CategoriasViewComponent(ProjetoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LogicaSistema sistema = new LogicaSistema(_dbContext);
            var model = sistema.GetAllCategorias();

            return await Task.FromResult((IViewComponentResult)View("Categorias", model));

        }

    }
}
