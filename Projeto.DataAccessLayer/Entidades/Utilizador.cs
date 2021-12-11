using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Utilizador : Entidade
    {
        [Required, MaxLength(255)]
        public string Nome { get; set; }

        [Required, EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Senha { get; set; }
        [Required]
        public TipoUtilizador Tipo { get; set; }

    }
}
