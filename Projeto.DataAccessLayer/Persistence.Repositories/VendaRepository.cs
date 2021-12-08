using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        private readonly ProjetoDBContext context;
        public VendaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
