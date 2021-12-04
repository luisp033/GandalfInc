using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface IUtilizadorRepository : IRepository<Utilizador>
    {
        IEnumerable<Utilizador> ObtemUtilizadoresActivos();

    }
}
