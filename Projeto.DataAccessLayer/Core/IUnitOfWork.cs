using Projeto.DataAccessLayer.Core.Repositories;
using System;

namespace Projeto.DataAccessLayer.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUtilizadorRepository Utilizadores { get; }
        ILojaRepository Lojas { get; }
        IMoradaRepository Moradas { get; }
        IPontoDeVendaRepository PontoDeVendas { get; }
        IPontoDeVendaSessaoRepository PontoDeVendaSessoes { get; }
        ICategoriaProdutoRepository CategoriaProdutos { get; }
        IClienteRepository Clientes { get; }
        IProdutoRepository Produtos { get; }
        IMarcaProdutoRepository MarcaProdutos { get; }
        ITipoPagamentoRepository TipoPagamentos { get; }



        int Complete();
    }
}
