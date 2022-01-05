using Projeto.DataAccessLayer;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {

        private readonly ProjetoDBContext _context;
        public LogicaSistema(ProjetoDBContext context)
        {
            _context = context;
        }




    }
}
