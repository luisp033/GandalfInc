using Projeto.DataAccessLayer.Faturacao;
using Projeto.DataAccessLayer.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Estoque : Entidade
    {

        [Required]
        public Produto Produto { get; set; }
        [MaxLength(50)]
        public string NumeroSerie { get; set; }
        [Required]
        public DateTime DataEntrada { get; set; }

    }
}
