using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Dto;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<DataPie> GetVendasPorCategoria()
        {
            //var query = from vd in context.DetalheVendas
            //            join v in context.Vendas on vd.VendaId equals v.Identificador
            //        join e in context.Estoques on vd.EstoqueId equals e.Identificador
            //        join p in context.Produtos on e.Produto.Identificador equals p.Identificador
            //        join c in context.CategoriaProdutos on p.Categoria.Identificador equals c.Identificador
            //        where v.DataHoraVenda != null
            //        group context.CategoriaProdutos.AsQueryable() by c.Nome into g
            //        select new 
            //        {
            //            Label = g.Key,
            //            Value = g.Count(),
            //        };

            var query = (from vd in context.DetalheVendas
                         join v in context.Vendas on vd.VendaId equals v.Identificador
                         join e in context.Estoques on vd.EstoqueId equals e.Identificador
                         join p in context.Produtos on e.Produto.Identificador equals p.Identificador
                         join c in context.CategoriaProdutos on p.Categoria.Identificador equals c.Identificador
                         where v.DataHoraVenda != null
                         select new { c.Nome })
                        .ToList()
                        .GroupBy(cc => cc.Nome)
                        .Select(cl => new DataPie
                         {
                                Label = cl.First().Nome,
                                Value = cl.Count(),
                          }).ToList();

            return query.ToList(); 
        }
    }
}
