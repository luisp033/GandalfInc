using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class UtilizadorRepository : Repository<Utilizador>, IUtilizadorRepository
    {

        private readonly ProjetoDBContext context;
        public UtilizadorRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public override IEnumerable<Utilizador> Find(Expression<Func<Utilizador, bool>> predicate)
        {
            return Context.Set<Utilizador>()
                .Include(t => t.Tipo)
                .Where(predicate);
        }

        public override IEnumerable<Utilizador> GetAll()
        {
            return Context.Set<Utilizador>()
                .Include(t => t.Tipo)
                .ToList();
        }
    }
}
