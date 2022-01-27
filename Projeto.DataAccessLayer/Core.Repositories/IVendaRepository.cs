using Projeto.DataAccessLayer.Dto;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface IVendaRepository : IRepository<Venda>
    {
        Venda GetVendaEmCurso(Guid pontoVendaSessaoId);

        Venda GetVendaCompleta(Guid vendaId);

        List<DataPie> GetVendasPorCategoria();
    }
}
