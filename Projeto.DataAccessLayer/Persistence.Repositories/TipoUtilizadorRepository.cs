using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class TipoUtilizadorRepository : Repository<TipoUtilizador>, ITipoUtilizadorRepository
    {

        private readonly ProjetoDBContext context;
        public TipoUtilizadorRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public TipoUtilizador GetTipoUtilizadorByEnum(TipoUtilizadorEnum tipoUtilizadorEnum)
        {
            return context.TipoUtilizadores.FirstOrDefault(x => x.Id == (int)tipoUtilizadorEnum);
        }
    }
}
