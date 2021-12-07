using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class TipoPagamentoRepository : Repository<TipoPagamento>, ITipoPagamentoRepository
    {

        private readonly ProjetoDBContext context;
        public TipoPagamentoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
