using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class UtilizadorRepository : Repository<Utilizador>, IUtilizadorRepository
    {
        public UtilizadorRepository(ProjetoDBContext context) : base(context)
        {
        }
        public ProjetoDBContext ProjetoDBContext 
        {
            get { return ProjetoDBContext as ProjetoDBContext; }
        }

        public IEnumerable<Utilizador> ObtemUtilizadoresActivos()
        {
            return ProjetoDBContext.Utilizadores.Where(x => x.Ativo).ToList();
        }

    }
}
