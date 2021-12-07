using System;
using System.ComponentModel.DataAnnotations;

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
