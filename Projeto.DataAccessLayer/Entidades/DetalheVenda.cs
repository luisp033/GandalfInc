
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.DataAccessLayer.Entidades
{
    public class DetalheVenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Identificador { get; set; }


        public decimal Desconto { get; set; }
        [Required]
        public decimal PrecoFinal { get; set; }
        [Required]
        public Guid EstoqueId { get; set; }
        public Estoque Estoque { get; set; }
        public Guid? VendaId { get; set; }
        public Venda Venda { get; set; }

    }
}