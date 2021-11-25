namespace Projeto.DataAccessLayer.Entidades
{
    public class PontoDeVenda : Entidade
    {
        public string Nome { get; set; }

        public Loja Loja { get; set; }

        public Utilizador UtilizadorLogado{ get; set; }

        public override string ToString()
        {
            return $"Ponto de Venda: {Identificador} - Nome: {Nome} - Ativo: {Ativo} - Loja: {Loja.Nome} - Utilizador logado: {UtilizadorLogado?.Nome}";
        }

    }
}
