using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;

namespace Projeto.DataAccessLayer.Persistence.Repositories
{
    public class MoradaRepository : Repository<Morada>, IMoradaRepository
    {
        public MoradaRepository(ProjetoDBContext context) : base(context)
        {
        }

    }
}
