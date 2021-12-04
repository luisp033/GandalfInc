using Projeto.DataAccessLayer.Core;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjetoDBContext _context;
        public UnitOfWork(ProjetoDBContext context)
        {
            _context = context;
            Utilizadores = new UtilizadorRepository(_context);
            Lojas = new LojaRepository(_context);
        }

        public IUtilizadorRepository Utilizadores { get; private set; }

        public ILojaRepository Lojas { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
