using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class PontoDeVendaRepository : Repository<PontoDeVenda>, IPontoDeVendaRepository
    {

        private readonly ProjetoDBContext context;
        public PontoDeVendaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
