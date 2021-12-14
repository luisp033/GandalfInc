using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {

        private readonly ProjetoDBContext context;
        public EstoqueRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public override IEnumerable<Estoque> Find(Expression<Func<Estoque, bool>> predicate)
        {
            return Context.Set<Estoque>()
                .Include(t => t.Produto)
                .Where(predicate);
        }

        public override IEnumerable<Estoque> GetAll()
        {
            return Context.Set<Estoque>()
                .Include(t => t.Produto)
                .ToList();
        }

    }
}
