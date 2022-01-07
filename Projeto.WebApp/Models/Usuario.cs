using System.ComponentModel.DataAnnotations;

namespace Projeto.WebApp.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "Informe o login do usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o nome do usuário")]
        [Display(Name = "Nome do usuário :")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o seu email")]
        [Display(Name = "Email do usuário :")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
