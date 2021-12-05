using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
