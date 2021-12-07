using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class CategoriaProdutoRepository : Repository<CategoriaProduto>, ICategoriaProdutoRepository
    {

        private readonly ProjetoDBContext context;
        public CategoriaProdutoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
