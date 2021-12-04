using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Utilizador : Entidade
    {
        [Required, MaxLength(255)]
        public string Nome { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Senha { get; set; }

        public TipoUtilizador Tipo { get; set; }

    }
}
