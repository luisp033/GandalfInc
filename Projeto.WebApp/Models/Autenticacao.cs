using Microsoft.Extensions.Configuration;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Projeto.WebApp.Models
{
    public class Autenticacao : IAutenticacao
    {
        private readonly ProjetoDBContext context;
        public Autenticacao(ProjetoDBContext dbContext)
        {
            context = dbContext;
        }

        public Resultado ValidarLogin(Usuario usuario)
        {

            LogicaSistema sistema = new LogicaSistema(context);

            return sistema.Login(usuario.Login, usuario.Senha);

        }
    }
}
