using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Enumerados;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class TipoPagamentoRepository : Repository<TipoPagamento>, ITipoPagamentoRepository
    {

        private readonly ProjetoDBContext context;
        public TipoPagamentoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public TipoPagamento GetTipoPagamentoByEnum(TipoPagamentoEnum tipoPagamentoEnum)
        {
            return context.TipoPagamentos.FirstOrDefault(x => x.Id == (int)tipoPagamentoEnum);
        }
    }
}
