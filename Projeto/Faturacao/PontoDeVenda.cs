using Projeto.Lib.Entidades;
using Projeto.Lib.Repositorios;
using System;
using System.Collections.Generic;

namespace Projeto.Lib.Faturacao
{
    public class PontoDeVenda : Entidade
    {

        public string Nome { get; set; }

        public Loja Loja { get; set; }

        public Utilizador UtilizadorLogado{ get; set; }

        //TODO Pesquisar produtos por Categoria, Marca, nome


        //TODO  AdicionarProdutosNaCompra
        //TODO  RemoverProdutosNaCompra
        //TODO  CancelarCompra
        //TODO  Pagar Compra

        public override string ToString()
        {
            return $"Ponto de Venda: {Identificador} - Nome: {Nome} - Ativo: {Ativo} - Loja: {Loja.Nome} - Utilizador logado: {UtilizadorLogado?.Nome}";
        }

    }
}
