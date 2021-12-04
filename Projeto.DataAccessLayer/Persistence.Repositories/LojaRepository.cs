using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class LojaRepository : Repository<Loja>, ILojaRepository
    {
        public LojaRepository(ProjetoDBContext context) : base(context)
        {
        }
        public ProjetoDBContext ProjetoDBContext 
        {
            get { return ProjetoDBContext as ProjetoDBContext; }
        }

    }
}
