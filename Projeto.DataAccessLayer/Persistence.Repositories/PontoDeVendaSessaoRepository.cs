using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Auxiliar;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class PontoDeVendaSessaoRepository : Repository<PontoDeVendaSessao>, IPontoDeVendaSessaoRepository
    {

        private readonly ProjetoDBContext context;
        public PontoDeVendaSessaoRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public override IEnumerable<PontoDeVendaSessao> Find(Expression<Func<PontoDeVendaSessao, bool>> predicate)
        {
            return Context.Set<PontoDeVendaSessao>()
                .Include(t => t.PontoDeVenda)
                .Include(t => t.Utilizador)
                .Where(predicate);
        }

        public override IEnumerable<PontoDeVendaSessao> GetAll()
        {
            return Context.Set<PontoDeVendaSessao>()
                .Include(t => t.PontoDeVenda)
                .Include(t => t.Utilizador)
                .ToList();
        }

        public PontoDeVendaSessao GetSessaoAbertaByUserEmail(string email)
        {

            var utl = (from u in context.Utilizadores
                      where u.Email == email
                      select u.Identificador).FirstOrDefault();

            var query = from s in context.PontoDeVendaSessoes
                        .Include(t => t.PontoDeVenda.Loja)
                        .Include(t => t.Utilizador)
                        where s.DataLogout == null && s.Utilizador.Identificador == utl 
                        select s;

            return query.FirstOrDefault();
        }

        public List<TotalSessao> GetTotalSessao(Guid pontoDeVendaSessaoId)
        {
            var query = from v in context.Vendas.AsEnumerable()
                        join p in context.TipoPagamentos on v.TipoPagamento?.Id equals p.Id
                        join vs in context.PontoDeVendaSessoes on v.PontoDeVendaSessao.Identificador equals vs.Identificador
                        where v.PontoDeVendaSessao.Identificador == pontoDeVendaSessaoId
                        group v by v.TipoPagamento.Id into pg
            select new TotalSessao
            {
                TipoPagamento = pg.FirstOrDefault().TipoPagamento.Name,
                Total = pg.Sum(m => m.ValorPagamento)

            };
            return query.ToList();
        }
    }
}
