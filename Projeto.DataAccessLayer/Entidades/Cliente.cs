using Projeto.DataAccessLayer.Faturacao;
using Projeto.DataAccessLayer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Cliente : Entidade
    {

        [Required, MaxLength(255)]
        public string Nome { get; set; }

        [MinLength(9, ErrorMessage ="NIF com tamanho errado"), MaxLength(9, ErrorMessage = "NIF com tamanho errado")]
        public string NumeroFiscal { get; set; }

        public Morada MoradaEntrega { get; set; }

        public Morada MoradaFaturacao { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }

        [EmailAddress, MaxLength(255)]
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }

    }
}