using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Loja : Entidade
    {
        [Required, MaxLength(255)]
        public string Nome { get; set; }
        
        [MinLength(9), MaxLength(9)]
        public string NumeroFiscal { get; set; }

        public Morada Morada { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        public Utilizador Responsavel { get; set; }

    }
}
