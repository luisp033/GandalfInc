using Projeto.DataAccessLayer.Entidades;
using System.Collections.Generic;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface ILojaRepository : IRepository<Loja>
    {
        List<Loja> GetAllLojasComPontosDeVenda();

    }
}
