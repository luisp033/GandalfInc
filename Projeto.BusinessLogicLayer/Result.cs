using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.BusinessLogicLayer
{
    public class Resultado
    {
        public Resultado(bool sucesso, string menssagem = "", object objeto = null)
        {
            Sucesso = sucesso;
            Mensagem = menssagem;
            Objeto = objeto;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Objeto { get; set; }

    }
}
