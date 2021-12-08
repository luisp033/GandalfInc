//using Projeto.DataAccessLayer.Entidades;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Projeto.DataAccessLayer.Repositorios
//{
//    public class RepositorioCategoriaProduto: IRepositorio<CategoriaProduto>
//    {
//        private readonly List<CategoriaProduto> ListaCategoriasProduto;

//        public RepositorioCategoriaProduto(List<CategoriaProduto> dados = null)
//        {
//            if (dados == null)
//            {
//                ListaCategoriasProduto = new List<CategoriaProduto> {

//                    new CategoriaProduto(){ Nome = "Música", OrdemApresentacao = 5, Ativo = true},
//                    new CategoriaProduto(){ Nome = "Livros", OrdemApresentacao = 6, Ativo = true},
//                    new CategoriaProduto(){ Nome = "Jogos", OrdemApresentacao = 1, Ativo = true},
//                    new CategoriaProduto(){ Nome = "Bebidas", OrdemApresentacao = 2, Ativo = true},
//                    new CategoriaProduto(){ Nome = "Filmes", OrdemApresentacao = 3, Ativo = true},
//                    new CategoriaProduto(){ Nome = "Brinquedos", OrdemApresentacao = 4, Ativo = false},
//                };
//            }
//            else
//            {
//                ListaCategoriasProduto = dados;
//            }
//        }

//        public void Apagar(CategoriaProduto t)
//        {
//            ListaCategoriasProduto.Remove(t);
//        }

//        public void Atualizar(CategoriaProduto tOld, CategoriaProduto tNew)
//        {
//            throw new NotImplementedException();
//        }

//        public CategoriaProduto Criar(CategoriaProduto t)
//        {
//            ListaCategoriasProduto.Add(t);
//            return t;
//        }

//        public CategoriaProduto ObterPorIdentificador(Guid guid)
//        {
//            return ListaCategoriasProduto.FirstOrDefault(x=>x.Identificador == guid);
//        }

//        public List<CategoriaProduto> ObterTodos()
//        {
//            return ListaCategoriasProduto;
//        }

//        public List<CategoriaProduto> ObterTodosActivosOrdenados()
//        {
//            return ListaCategoriasProduto
//                .Where(x=>x.Ativo)
//                .OrderBy(x=>x.OrdemApresentacao)
//                .ToList();
//        }

//        public CategoriaProduto ObterPorNome(string nome)
//        {
//            return ListaCategoriasProduto.FirstOrDefault(x => x.Nome == nome);
//        }



//    }
//}
