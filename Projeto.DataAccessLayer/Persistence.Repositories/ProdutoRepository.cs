using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        private readonly ProjetoDBContext context;
        public ProdutoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
