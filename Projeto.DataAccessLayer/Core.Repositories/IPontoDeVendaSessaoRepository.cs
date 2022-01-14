using Projeto.DataAccessLayer.Auxiliar;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface IPontoDeVendaSessaoRepository : IRepository<PontoDeVendaSessao>
    {
        List<TotalSessao> GetTotalSessao(Guid pontoDeVendaSessaoId);

        PontoDeVendaSessao GetSessaoAbertaByUserEmail(string email);
    }
}
