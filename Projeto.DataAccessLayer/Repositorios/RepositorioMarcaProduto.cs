//using Projeto.DataAccessLayer.Entidades;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Projeto.DataAccessLayer.Repositorios
//{
//    public class RepositorioMarcaProduto: IRepositorio<MarcaProduto>
//    {
//        private readonly List<MarcaProduto> ListaMarcasProduto;

//        public RepositorioMarcaProduto(List<MarcaProduto> dados = null)
//        {
//            if (dados == null)
//            {
//                ListaMarcasProduto = new List<MarcaProduto> {

//                    new MarcaProduto(){ Nome = "Sony",  Ativo = true, Origem= "Japão"},
//                    new MarcaProduto(){ Nome = "Valentim de Carvalho", Ativo = true, Origem= "Portugal"},
//                    new MarcaProduto(){ Nome = "Betrand",  Ativo = true, Origem= "Portugal"},
//                    new MarcaProduto(){ Nome = "Sagres",  Ativo = true, Origem= "Portugal"},
//                    new MarcaProduto(){ Nome = "Super Bock",  Ativo = true, Origem= "Portugal"},
//                    new MarcaProduto(){ Nome = "Blizzard",  Ativo = true, Origem= "USA"},
//                    new MarcaProduto(){ Nome = "EASports",  Ativo = true, Origem= "Europe"},
//                    new MarcaProduto(){ Nome = "WarnerBros",  Ativo = true, Origem= "USA"},
//                    new MarcaProduto(){ Nome = "Disney",  Ativo = true, Origem= "USA"},
//                    new MarcaProduto(){ Nome = "ToysRus",  Ativo = true, Origem= "China"},
//                };
//            }
//            else
//            {
//                ListaMarcasProduto = dados;
//            }
//        }

//        public void Apagar(MarcaProduto t)
//        {
//            ListaMarcasProduto.Remove(t);
//        }

//        public void Atualizar(MarcaProduto tOld, MarcaProduto tNew)
//        {
//            throw new NotImplementedException();
//        }

//        public MarcaProduto Criar(MarcaProduto t)
//        {
//            ListaMarcasProduto.Add(t);
//            return t;
//        }

//        public MarcaProduto ObterPorIdentificador(Guid guid)
//        {
//            return ListaMarcasProduto.FirstOrDefault(x=>x.Identificador == guid);
//        }

//        public List<MarcaProduto> ObterTodos()
//        {
//            return ListaMarcasProduto;
//        }

//        public List<MarcaProduto> ObterTodosActivosOrdenadosAlfabeticamente()
//        {
//            return ListaMarcasProduto
//                .Where(x=>x.Ativo)
//                .OrderBy(x=>x.Nome)
//                .ToList();
//        }

//        public MarcaProduto ObterPorNome(string nome)
//        {
//            return ListaMarcasProduto.FirstOrDefault(x => x.Nome == nome);
//        }



//    }
//}
