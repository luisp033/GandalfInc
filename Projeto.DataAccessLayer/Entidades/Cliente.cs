using Projeto.DataAccessLayer.Faturacao;
using Projeto.DataAccessLayer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Cliente : Entidade
    {

        private string nome;
        [Required, MaxLength(255)]
        public string Nome
        {
            get { return nome; }
            set { nome = value.ToTitleCase(); }
        }

        [Required, MinLength(9, ErrorMessage ="NIF com tamanho errado"), MaxLength(9, ErrorMessage = "NIF com tamanho errado")]
        public string NumeroFiscal { get; set; }
        public Morada MoradaEntrega { get; set; }
        public Morada MoradaFaturacao { get; set; }
        public string Telefone { get; set; }
        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        //[Range(typeof(DateTime), "1/1/1920", "3/4/2004",
        //ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? DataNascimento { get; set; }
        public virtual List<Venda> Compras { get; set; }

    }
}