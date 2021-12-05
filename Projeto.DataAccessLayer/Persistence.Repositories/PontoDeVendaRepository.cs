﻿using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class PontoDeVendaRepository : Repository<PontoDeVenda>, IPontoDeVendaRepository
    {

        private readonly ProjetoDBContext context;
        public PontoDeVendaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}