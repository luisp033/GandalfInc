﻿using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class UtilizadorRepository : Repository<Utilizador>, IUtilizadorRepository
    {

        private readonly ProjetoDBContext context;
        public UtilizadorRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Utilizador> ObtemUtilizadoresActivos()
        {
            return context.Utilizadores.Where(x => x.Ativo).ToList();
        }

    }
}
