using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class DetalheVendaRepository : Repository<DetalheVenda>, IDetalheVendaRepository
    {

        private readonly ProjetoDBContext context;
        public DetalheVendaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public override IEnumerable<DetalheVenda> Find(Expression<Func<DetalheVenda, bool>> predicate)
        {
            return Context.Set<DetalheVenda>()
                .Include(t => t.Estoque)
                .Include(t => t.Estoque.Produto)
                .Include(t => t.Venda)
                .Where(predicate);
        }

        public override IEnumerable<DetalheVenda> GetAll()
        {
            return Context.Set<DetalheVenda>()
                .Include(t => t.Estoque)
                .Include(t => t.Estoque.Produto)
                .Include(t => t.Venda)
                .ToList();
        }

    }
}
