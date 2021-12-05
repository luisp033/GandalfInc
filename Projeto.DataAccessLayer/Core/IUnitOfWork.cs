using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUtilizadorRepository Utilizadores { get; }

        ILojaRepository Lojas { get; }

        IMoradaRepository Moradas { get; }
        IPontoDeVendaRepository PontoDeVendas { get; }
        IPontoDeVendaSessaoRepository PontoDeVendaSessoes { get; }

        int Complete();
    }
}
