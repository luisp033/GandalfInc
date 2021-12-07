using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class LojaRepository : Repository<Loja>, ILojaRepository
    {
        public LojaRepository(ProjetoDBContext context) : base(context)
        {
        }
        //public ProjetoDBContext ProjetoDBContext 
        //{
        //    get { return ProjetoDBContext; }
        //}

    }
}
