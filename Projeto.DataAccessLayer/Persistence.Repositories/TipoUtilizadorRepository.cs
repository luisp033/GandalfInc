using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class TipoUtilizadorRepository : Repository<TipoUtilizador>, ITipoUtilizadorRepository
    {

        private readonly ProjetoDBContext context;
        public TipoUtilizadorRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
