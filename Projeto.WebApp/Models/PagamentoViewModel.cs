using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.DataAccessLayer.Enumerados;
using System.Collections.Generic;

namespace Projeto.WebApp.Models
{
    public class PagamentoViewModel
    {

        public string Nome { get; set; }

        public string NumeroContribuinte { get; set; }

        public string Telefone { get; set; }

        public TipoPagamentoEnum Tipo { get; set; }

        public string Pago { get; set; } 

        


    }
}
