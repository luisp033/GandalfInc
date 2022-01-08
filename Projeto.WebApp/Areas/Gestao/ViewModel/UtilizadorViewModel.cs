using Projeto.DataAccessLayer.Entidades;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projeto.WebApp.Areas.Gestao.ViewModel
{
    public class UtilizadorViewModel
    {

        public Guid Identificador { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; }

        [Required, EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public TipoUtilizador Tipo { get; set; }

    }
}
