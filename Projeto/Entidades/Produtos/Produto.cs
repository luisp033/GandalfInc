﻿using Projeto.Lib.Entidades.Pessoas;

namespace Projeto.Lib.Entidades.Produtos
{
    public class Produto : Entidade
    {
        public string Ean { get; set; } 
        public string Nome { get; set; }
        public CategoriaProduto Categoria{ get; set; }
        public MarcaProduto Marca { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string NumeroSerie { get; set; }


        public override string ToString()
        {
            return $"Ean: {Ean} - Nome: {Nome} - Categoria: {Categoria?.Nome} - Marca: {Marca?.Nome} - Preço: {PrecoUnitario}";
        }

    }
}
