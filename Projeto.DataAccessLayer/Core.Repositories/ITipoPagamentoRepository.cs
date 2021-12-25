using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Enumerados;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface ITipoPagamentoRepository : IRepository<TipoPagamento>
    {
        TipoPagamento GetTipoPagamentoByEnum(TipoPagamentoEnum tipoPagamentoEnum);
    }
}
