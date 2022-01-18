using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
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
                        .Include(t=>t.PontoDeVendaSessao.PontoDeVenda.Loja)
                        .Include(t => t.DetalheVendas)
                        where !venda.DataHoraVenda.HasValue && venda.PontoDeVendaSessao.Identificador == pontoVendaSessaoId
                        select venda;

            return query.FirstOrDefault();
        }
        public Venda GetVendaCompleta(Guid vendaId)
        {
            var query = from venda in context.Vendas
                        .Include(t=>t.PontoDeVendaSessao.PontoDeVenda.Loja)
                        .Include(t => t.DetalheVendas).ThenInclude(c => c.Estoque).ThenInclude(p => p.Produto)
                        where venda.Identificador == vendaId
                        select venda;

            return query.FirstOrDefault();
        }


    }
}
