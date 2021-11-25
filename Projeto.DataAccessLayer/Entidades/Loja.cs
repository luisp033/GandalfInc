namespace Projeto.DataAccessLayer.Entidades
{
    public class Loja : Entidade
    {

        public string Nome { get; set; }
        public string NumeroFiscal { get; set; }
        public Morada Morada { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Utilizador Responsavel { get; set; }


        public override string ToString()
        {
            return $"Loja: {base.Identificador} - Nome: {Nome} - Responsavel: {Responsavel.Nome} - Ativo: {Ativo} - CPostal: {Morada.CodigoPostal}";
        }

    }
}
