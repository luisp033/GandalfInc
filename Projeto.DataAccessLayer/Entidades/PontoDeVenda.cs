using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class PontoDeVenda : Entidade
    {

        [Required, MaxLength(255)]
        public string Nome { get; set; }

        public Loja Loja { get; set; }

    }
}
