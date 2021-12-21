using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        private readonly ProjetoDBContext context;
        public VendaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public Venda GetVendaEmCurso(Guid pontoVendaSessaoId)
        {
            var query = from venda in context.Vendas
                        where !venda.DataHoraVenda.HasValue && venda.PontoDeVendaSessao.Identificador == pontoVendaSessaoId
                        select venda;

            return query.FirstOrDefault();
        }
    }
}
