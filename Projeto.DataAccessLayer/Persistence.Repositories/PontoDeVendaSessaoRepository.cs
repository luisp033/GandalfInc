﻿using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class PontoDeVendaSessaoRepository : Repository<PontoDeVendaSessao>, IPontoDeVendaSessaoRepository
    {

        private readonly ProjetoDBContext context;
        public PontoDeVendaSessaoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

    }
}
