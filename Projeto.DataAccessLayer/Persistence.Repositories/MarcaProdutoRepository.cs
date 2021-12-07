using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class MarcaProdutoRepository : Repository<MarcaProduto>, IMarcaProdutoRepository
    {

        private readonly ProjetoDBContext context;
        public MarcaProdutoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
