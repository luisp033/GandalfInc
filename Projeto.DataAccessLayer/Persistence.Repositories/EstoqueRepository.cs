using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {

        private readonly ProjetoDBContext context;
        public EstoqueRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
