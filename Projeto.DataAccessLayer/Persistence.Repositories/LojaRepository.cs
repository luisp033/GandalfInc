using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class LojaRepository : Repository<Loja>, ILojaRepository
    {
        private readonly ProjetoDBContext context;
        public LojaRepository(ProjetoDBContext context) : base(context)
        {
            this.context = context;
        }

        public List<Loja> GetAllLojasComPontosDeVenda()
        {
            var query = from loja in context.Lojas
                        join pos in context.PontoDeVendas on loja.Identificador equals pos.Loja.Identificador
                        select loja;

            return query.ToList();

        }





    }
}
