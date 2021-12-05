using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.DataAccessLayer.Entidades
{
    public class PontoDeVendaSessao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Identificador { get; set; }
        [Required]
        public PontoDeVenda PontoDeVenda { get; set; }
        [Required]
        public Utilizador Utilizador { get; set; }
        [Required]
        public DateTime DataLogin { get; set; }
        public DateTime? DataLogout { get; set; }


    }
}