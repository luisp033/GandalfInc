using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class DetalheVendaRepository : Repository<DetalheVenda>, IDetalheVendaRepository
    {

        private readonly ProjetoDBContext context;
        public DetalheVendaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
