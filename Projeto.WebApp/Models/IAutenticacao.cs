using Projeto.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.WebApp.Models
{
    public interface IAutenticacao
    {
        string GetConnectionString();
        string RegistrarUsuario(Usuario usuario);
        Resultado ValidarLogin(Usuario usuario);
    }
}
