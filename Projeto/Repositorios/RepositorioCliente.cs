using Projeto.Lib.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Repositorios
{
    public class RepositorioCliente: IRepositorio<Cliente>
    {
        private readonly List<Cliente> ListaClientes;

        public RepositorioCliente(List<Cliente> dados = null)
        {
            if (dados == null)
            {
                ListaClientes = new List<Cliente> {

                    new Cliente(){ 
                        Nome = "Luis Ponciano",   
                        DataNascimento = new DateTime(1971,01,01),
                        NumeroFiscal = "100000001",
                        Email = "luis@mail.pt",
                        Telefone = "987654321",
                        MoradaEntrega = new Morada(){ Endereco = "Rua da Entrega", CodigoPostal="1000", Localidade="Odivelas"},
                        MoradaFaturacao = new Morada(){ Endereco = "Av da Fatura", CodigoPostal="2000", Localidade="Caneças"},
                    },
                };
            }
            else
            {
                ListaClientes = dados;
            }
        }

        public void Apagar(Cliente t)
        {
            ListaClientes.Remove(t);
        }

        public void Atualizar(Cliente tOld, Cliente tNew)
        {
            throw new NotImplementedException();
        }

        public Cliente Criar(Cliente t)
        {
            ListaClientes.Add(t);
            return t;
        }

        public Cliente ObterPorIdentificador(Guid guid)
        {
            return ListaClientes.Where(x=>x.Identificador == guid).FirstOrDefault();
        }

        public List<Cliente> ObterTodos()
        {
            return ListaClientes;
        }

        public List<Cliente> ObterTodosActivosOrdenadosAlfabeticamente()
        {
            return ListaClientes
                .Where(x=>x.Ativo)
                .OrderBy(x=>x.Nome)
                .ToList();
        }

        public Cliente ObterPorNome(string nome)
        {
            return ListaClientes.Where(x => x.Nome == nome).FirstOrDefault();
        }

        public Cliente ObterPorNif(string nif)
        {
            return ListaClientes.Where(x => x.NumeroFiscal == nif).FirstOrDefault();
        }




    }
}
