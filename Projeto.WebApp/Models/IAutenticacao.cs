using Projeto.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.WebApp.Models
{
    public interface IAutenticacao
    {
        Resultado ValidarLogin(Usuario usuario);
    }
}
