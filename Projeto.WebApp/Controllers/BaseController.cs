using Microsoft.AspNetCore.Mvc;
using Projeto.DataAccessLayer;

namespace Projeto.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ProjetoDBContext _dbContext;
        public BaseController(ProjetoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
