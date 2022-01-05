using Projeto.DataAccessLayer.Entidades;
using System;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface IVendaRepository : IRepository<Venda>
    {
        Venda GetVendaEmCurso(Guid pontoVendaSessaoId);
    }
}
