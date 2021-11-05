namespace Projeto.Lib.Entidades
{
    public class Loja : Entidade
    {
        public string NumeroFiscal { get; set; }
        public Morada Morada { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string NomeResponsavel { get; set; } //Gerente
    }
}
