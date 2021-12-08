//using Projeto.DataAccessLayer.Entidades;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Projeto.DataAccessLayer.Repositorios
//{
//    public class RepositorioProduto: IRepositorio<Produto>
//    {
//        private readonly List<Produto> ListaProdutos;

//        public RepositorioProduto(RepositorioCategoriaProduto categorias, RepositorioMarcaProduto marcas, List<Produto> dados = null)
//        {
//            if (dados == null)
//            {
//                ListaProdutos = new List<Produto> {

//                    new Produto(){ Nome = "U2",  Ativo = true , Categoria = categorias.ObterPorNome("Música"), Marca = marcas.ObterPorNome("Sony"), Ean = "10001", PrecoUnitario= 12.0m },
//                    new Produto(){ Nome = "Queen",  Ativo = true , Categoria = categorias.ObterPorNome("Música"), Marca = marcas.ObterPorNome("Sony"), Ean = "10002", PrecoUnitario= 15.0m },
//                    new Produto(){ Nome = "Harry Potter",  Ativo = true , Categoria = categorias.ObterPorNome("Livros"), Marca = marcas.ObterPorNome("Betrand"), Ean = "10003", PrecoUnitario= 20.0m },
//                    new Produto(){ Nome = "Os Maias",  Ativo = true , Categoria = categorias.ObterPorNome("Livros"), Marca = marcas.ObterPorNome("Betrand"), Ean = "10004", PrecoUnitario= 10.0m },
//                    new Produto(){ Nome = "World of Warcraft",  Ativo = true , Categoria = categorias.ObterPorNome("Jogos"), Marca = marcas.ObterPorNome("Blizzard"), Ean = "10005", PrecoUnitario= 50.0m },
//                    new Produto(){ Nome = "Fifa 22",  Ativo = true , Categoria = categorias.ObterPorNome("Jogos"), Marca = marcas.ObterPorNome("EASports"), Ean = "10006", PrecoUnitario= 60.0m },
//                    new Produto(){ Nome = "Super Bock Média",  Ativo = true , Categoria = categorias.ObterPorNome("Bebidas"), Marca = marcas.ObterPorNome("Super Bock"), Ean = "10007", PrecoUnitario= 1.5m },
//                    new Produto(){ Nome = "Sagres Green",  Ativo = true , Categoria = categorias.ObterPorNome("Bebidas"), Marca = marcas.ObterPorNome("Sagres"), Ean = "10008", PrecoUnitario= 1.5m },
//                    new Produto(){ Nome = "Rambo",  Ativo = true , Categoria = categorias.ObterPorNome("Filmes"), Marca = marcas.ObterPorNome("Sony"), Ean = "10009", PrecoUnitario= 8.0m },
//                    new Produto(){ Nome = "Matrix",  Ativo = true , Categoria = categorias.ObterPorNome("Filmes"), Marca = marcas.ObterPorNome("Sony"), Ean = "10010", PrecoUnitario= 9.0m },
//                    new Produto(){ Nome = "Amália Rodrigues",  Ativo = true , Categoria = categorias.ObterPorNome("Música"), Marca = marcas.ObterPorNome("Valentim de Carvalho"), Ean = "10011", PrecoUnitario= 10.0m },
//                    new Produto(){ Nome = "Quim barreiros",  Ativo = true , Categoria = categorias.ObterPorNome("Música"), Marca = marcas.ObterPorNome("Valentim de Carvalho"), Ean = "10012", PrecoUnitario= 5.0m },
//                    new Produto(){ Nome = "Lusiadas",  Ativo = true , Categoria = categorias.ObterPorNome("Livros"), Marca = marcas.ObterPorNome("Betrand"), Ean = "10013", PrecoUnitario= 7.5m },
//                    new Produto(){ Nome = "C# OOP",  Ativo = true , Categoria = categorias.ObterPorNome("Livros"), Marca = marcas.ObterPorNome("Betrand"), Ean = "10014", PrecoUnitario= 12.0m },
//                    new Produto(){ Nome = "Pokemon",  Ativo = true , Categoria = categorias.ObterPorNome("Jogos"), Marca = marcas.ObterPorNome("EASports"), Ean = "10015", PrecoUnitario= 32.0m },
//                    new Produto(){ Nome = "Heartstone",  Ativo = true , Categoria = categorias.ObterPorNome("Jogos"), Marca = marcas.ObterPorNome("Blizzard"), Ean = "10016", PrecoUnitario= 0.0m },
//                    new Produto(){ Nome = "Coca Cola",  Ativo = true , Categoria = categorias.ObterPorNome("Bebidas"), Marca = marcas.ObterPorNome("Super Bock"), Ean = "10017", PrecoUnitario= 2.0m },
//                    new Produto(){ Nome = "Vodka",  Ativo = true , Categoria = categorias.ObterPorNome("Bebidas"), Marca = marcas.ObterPorNome("Sagres"), Ean = "10018", PrecoUnitario= 4.5m },
//                    new Produto(){ Nome = "Star Wars",  Ativo = true , Categoria = categorias.ObterPorNome("Filmes"), Marca = marcas.ObterPorNome("Sony"), Ean = "10019", PrecoUnitario= 18.0m },
//                    new Produto(){ Nome = "Psyco",  Ativo = true , Categoria = categorias.ObterPorNome("Filmes"), Marca = marcas.ObterPorNome("Sony"), Ean = "10020", PrecoUnitario= 6.0m },


//                };
//            }
//            else
//            {
//                ListaProdutos = dados;
//            }
//        }

//        public void Apagar(Produto t)
//        {
//            ListaProdutos.Remove(t);
//        }

//        public void Atualizar(Produto tOld, Produto tNew)
//        {
//            var atual = ObterPorIdentificador(tOld.Identificador);
//            atual.Ativo = tNew.Ativo;
//            atual.DataUltimaAlteracao = DateTime.Now;
//            atual.Nome = tNew.Nome;
//        }

//        public Produto Criar(Produto t)
//        {
//            ListaProdutos.Add(t);
//            return t;
//        }

//        public Produto ObterPorIdentificador(Guid guid)
//        {
//            return ListaProdutos.FirstOrDefault(x=>x.Identificador == guid);
//        }

//        public List<Produto> ObterTodos()
//        {
//            return ListaProdutos;
//        }

//        public List<Produto> ObterTodosActivosOrdenadosAlfabeticamente()
//        {
//            return ListaProdutos
//                .Where(x=>x.Ativo)
//                .OrderBy(x=>x.Nome)
//                .ToList();
//        }

//        public Produto ObterPorNome(string nome)
//        {
//            return ListaProdutos.FirstOrDefault(x => x.Nome == nome);
//        }

//        public Produto ObterPorEan(string ean)
//        {
//            return ListaProdutos.FirstOrDefault(x => x.Ean == ean);
//        }
//        public List<Produto> ObterProdutosPor(string query)
//        {
//            return ListaProdutos.Where(x => x.Nome.Contains(query) ||
//                                            x.Categoria.Nome.Contains(query) ||
//                                            x.Marca.Nome.Contains(query))
//                                .OrderBy(x=>x.Nome)
//                                .ToList();
//        }




//    }
//}
