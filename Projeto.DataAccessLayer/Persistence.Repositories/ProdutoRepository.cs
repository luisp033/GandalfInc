using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        private readonly ProjetoDBContext context;
        public ProdutoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }


        public override IEnumerable<Produto> Find(Expression<Func<Produto, bool>> predicate)
        {
            return Context.Set<Produto>()
                .Include(t => t.Categoria)
                .Include(t => t.Marca)
                .Where(predicate);
        }

        public override IEnumerable<Produto> GetAll()
        {
            return Context.Set<Produto>()
                .Include(t => t.Categoria)
                .Include(t => t.Marca)
                .ToList();
        }

    }
}
