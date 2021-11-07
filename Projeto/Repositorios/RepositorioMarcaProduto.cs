using Projeto.Lib.Entidades;
using Projeto.Lib.Entidades.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Repositorios
{
    public class RepositorioMarcaProduto: IRepositorio<MarcaProduto>
    {
        private readonly List<MarcaProduto> ListaMarcasProduto;

        public RepositorioMarcaProduto(MarcaProduto dados = null)
        {
            if (dados == null)
            {
                ListaMarcasProduto = new List<MarcaProduto> {

                    new MarcaProduto(){ Nome = "Sony",  Ativo = true, Origem= "Japão"},
                    new MarcaProduto(){ Nome = "Valentim de Carvalho", Ativo = true, Origem= "Portugal"},
                    new MarcaProduto(){ Nome = "Betrand",  Ativo = true, Origem= "Portugal"},
                    new MarcaProduto(){ Nome = "Sagres",  Ativo = true, Origem= "Portugal"},
                    new MarcaProduto(){ Nome = "Super Bock",  Ativo = true, Origem= "Portugal"},
                    new MarcaProduto(){ Nome = "Blizzard",  Ativo = true, Origem= "USA"},
                    new MarcaProduto(){ Nome = "EASports",  Ativo = true, Origem= "Europe"},
                    new MarcaProduto(){ Nome = "WarnerBros",  Ativo = true, Origem= "USA"},
                    new MarcaProduto(){ Nome = "Disney",  Ativo = true, Origem= "USA"},
                    new MarcaProduto(){ Nome = "ToysRus",  Ativo = true, Origem= "China"},
                };
            }
        }

        public void Apagar(MarcaProduto t)
        {
            ListaMarcasProduto.Remove(t);
        }

        public void Atualizar(MarcaProduto tOld, MarcaProduto tNew)
        {
            throw new NotImplementedException();
        }

        public void Criar(MarcaProduto t)
        {
            ListaMarcasProduto.Add(t);
        }

        public MarcaProduto ObterPorIdentificador(Guid guid)
        {
            return ListaMarcasProduto.Where(x=>x.Identificador == guid).FirstOrDefault();
        }

        public List<MarcaProduto> ObterTodos()
        {
            return ListaMarcasProduto;
        }

        public List<MarcaProduto> ObterTodosActivosOrdenadosAlfabeticamente()
        {
            return ListaMarcasProduto
                .Where(x=>x.Ativo)
                .OrderBy(x=>x.Nome)
                .ToList();
        }

        public MarcaProduto ObterPorNome(string nome)
        {
            return ListaMarcasProduto.Where(x => x.Nome == nome).FirstOrDefault();
        }



    }
}
