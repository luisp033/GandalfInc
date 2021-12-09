using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface ITipoUtilizadorRepository : IRepository<TipoUtilizador>
    {
        TipoUtilizador GetTipoUtilizadorByEnum(TipoUtilizadorEnum tipoUtilizadorEnum);
    }
}
