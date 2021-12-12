using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class PontoDeVendaRepository : Repository<PontoDeVenda>, IPontoDeVendaRepository
    {

        private readonly ProjetoDBContext context;
        public PontoDeVendaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }


        public override IEnumerable<PontoDeVenda> Find(Expression<Func<PontoDeVenda, bool>> predicate)
        {
            return Context.Set<PontoDeVenda>()
                .Include(t => t.Loja)
                .Where(predicate);
        }

        public override IEnumerable<PontoDeVenda> GetAll()
        {
            return Context.Set<PontoDeVenda>()
                .Include(t => t.Loja)
                .ToList();
        }

    }
}
