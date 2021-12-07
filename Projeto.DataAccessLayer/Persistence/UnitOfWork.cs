using Projeto.DataAccessLayer.Core;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Persistence.Repositories;

namespace Projeto.DataAccessLayer.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork 
    {
        private readonly ProjetoDBContext _context;
        public UnitOfWork(ProjetoDBContext context)
        {
            _context = context;
            Utilizadores = new UtilizadorRepository(_context);
            Lojas = new LojaRepository(_context);
            Moradas = new MoradaRepository(_context);
            PontoDeVendas = new PontoDeVendaRepository(_context);
            PontoDeVendaSessoes = new PontoDeVendaSessaoRepository(_context);
            CategoriaProdutos = new CategoriaProdutoRepository(_context);
            Clientes = new ClienteRepository(_context);
            MarcaProdutos = new MarcaProdutoRepository(_context);
            Produtos = new ProdutoRepository(_context);
            TipoPagamentos = new TipoPagamentoRepository(_context);
        }

        public IUtilizadorRepository Utilizadores { get; private set; }

        public ILojaRepository Lojas { get; private set; }

        public IMoradaRepository Moradas { get; private set; }

        public IPontoDeVendaRepository PontoDeVendas { get; private set; }

        public IPontoDeVendaSessaoRepository PontoDeVendaSessoes { get; private set; }

        public ICategoriaProdutoRepository CategoriaProdutos { get; private set; }

        public IClienteRepository Clientes { get; private set; }

        public IProdutoRepository Produtos { get; private set; }
        public IMarcaProdutoRepository MarcaProdutos { get; private set; }

        public ITipoPagamentoRepository TipoPagamentos { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            //_context.Dispose();
            //Dispose();
        }
    }
}
