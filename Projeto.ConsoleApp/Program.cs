using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;

namespace Projeto.ConsoleApp
{
    public static class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\n\t\t\t\t * * * * * * * * [GANDALF INC...] * * * * * * * * \n");

            Console.WriteLine("\n-------------------------------------------- [Lojas da Gandalf Inc]\n");

            //GestaoUtilizadores();

            //RepositorioUtilizador utilizadores = new RepositorioUtilizador(dbContext);

            //utilizadores.Criar(new Utilizador { 
            //    Nome = "User 1",
            //    Tipo = DataAccessLayer.TipoUtilizador.Empregado,
            //    Email = "user1@mail.pt",
            //    Senha = "123",
            //    Ativo = true,
            //    DataCriacao = DateTime.Now,
            //    DataUltimaAlteracao =DateTime.Now
            //});





            //RepositorioLoja lojas = new RepositorioLoja(utilizadores);
            //RepositorioPontoDeVenda pontosDeVenda = new RepositorioPontoDeVenda(utilizadores, lojas);
            //RepositorioCategoriaProduto categorias = new RepositorioCategoriaProduto();
            //RepositorioMarcaProduto marcas = new RepositorioMarcaProduto();
            //RepositorioProduto produtos = new RepositorioProduto(categorias,marcas);
            //RepositorioEstoque estoques = new RepositorioEstoque(produtos);
            //RepositorioCliente clientes = new RepositorioCliente();
            //RepositorioVenda vendas = new RepositorioVenda();


            //var listaLojas = lojas.ObterTodos();
            //foreach (var item in listaLojas)
            //{
            //    Console.WriteLine(item);
            //}

            //Loja lojaSelecionada;
            //do
            //{
            //    Console.WriteLine("\n Escolha uma loja activa (exemplo: \"Loja 1\")");
            //    var nomeLoja =Console.ReadLine();
            //    lojaSelecionada = lojas.ObterPorNomeActivo(nomeLoja);

            //} while (lojaSelecionada == null);

            //Console.WriteLine($"\n[Loja selecionada] {lojaSelecionada} \n");

            //Console.WriteLine($"\n-------------------------------------------- [Pontos de venda da {lojaSelecionada.Nome}]\n");
            //var listaPontosDeVenda = pontosDeVenda.ObterPorLojaActiva(lojaSelecionada.Identificador);
            //foreach (var item in listaPontosDeVenda)
            //{
            //    Console.WriteLine(item);
            //}

            //PontoDeVenda pontoDeVendaSelecionado;
            //do
            //{
            //    Console.WriteLine("\n Escolha um ponto de venda (exemplo: \"POS 1\")");
            //    var nomePontoDeVenda = Console.ReadLine();
            //    pontoDeVendaSelecionado = pontosDeVenda.ObterPorNomeActivo(nomePontoDeVenda);

            //} while (pontoDeVendaSelecionado == null);

            //Console.WriteLine($"\n[Ponto de Venda selecionado] {pontoDeVendaSelecionado} \n");

            //Utilizador utilizadorLogado;
            //do
            //{
            //    Console.WriteLine("\n ----- Login ------\n");
            //    Console.WriteLine("\n Digite o email : (exemplo: \"elsa@mail.pt\") ");
            //    var emailLogin = Console.ReadLine();
            //    Console.WriteLine("\n Digite a senha : (exemplo: \"1234\") ");
            //    var senhaLogin = Console.ReadLine();
            //    utilizadorLogado = pontosDeVenda.Login(pontoDeVendaSelecionado, emailLogin, senhaLogin);

            //    if (utilizadorLogado == null)
            //    {
            //        Console.WriteLine("\t\t\t\t Email ou senha desconhecidos, volte a digitar, sff.");
            //    }

            //} while (utilizadorLogado == null);

            //Console.WriteLine($"\n Utilizador <{utilizadorLogado.Nome}> logado com sucesso no <{pontoDeVendaSelecionado.Nome}> da <{pontoDeVendaSelecionado.Loja.Nome}> \n");

            //Console.WriteLine($"\nDigite uma palavra para pesquisar produtos\n");
            //var pesquisa = Console.ReadLine();

            //Console.WriteLine($"\nProdutos encontrados para a pesquisa {pesquisa}\n");

            //var listaProdutos = produtos.ObterProdutosPor(pesquisa); 
            //foreach (var item in listaProdutos)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine($"\nListagem do estoque\n");
            //var listaEstoques = estoques.ObterTodos();
            //foreach (var item in listaEstoques)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine($"\nListagem de clientes existentes\n");
            //var listaClientes = clientes.ObterTodos();
            //foreach (var item in listaClientes)
            //{
            //    Console.WriteLine(item);
            //}

            //Processa duas vendas
            //for (int i = 1; i < 3; i++)  //TODO Arranjar
            //{
            //    Console.WriteLine($"\nInicio da Nova Venda\n");
            //    var pontoDeVendaSelecionado = pontosDeVenda.ObterPorNomeActivo("POS 1");
            //    var utilizadorLogado = pontosDeVenda.Login(pontoDeVendaSelecionado, "elsa@mail.pt", "1234");
            //    Venda venda = vendas.Criar(new Venda());
            //    LogicaVenda.IniciarNovaVenda(pontoDeVendaSelecionado, utilizadorLogado);
            //    var msg = venda.AdicionarProduto("10001", i);
            //    Console.WriteLine(msg);
            //    Cliente cliente = clientes.Criar(new Cliente() { Nome = "Maria das dores", NumeroFiscal = "888888888" });
            //    msg = venda.FinalizarPagamento(TipoPagamento.Multibanco, cliente);
            //    Console.WriteLine(msg);
            //    venda.GerarRecibo();
            //    Console.WriteLine("--------------------------------------------------");
            //}
        }

        /// <summary>
        /// Insere 2 utilizadores para teste
        /// </summary>
        private static void GestaoUtilizadores()
        {
            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.SqlServer)))
            {


                Console.WriteLine("-----------------------------------------{Utilizadores}-");
                var users = unitOfWork.Utilizadores.GetAll();
                foreach (var item in users)
                {
                    Console.WriteLine($"{item.Identificador} - {item.Nome}");
                }
                Console.WriteLine("-----------------------------------------{Inserindo 2 users}-");



                unitOfWork.Utilizadores.Add(new Utilizador
                {
                    Nome = "Elias Empregado",
                    Email = "elias@mail.pt",
                    Tipo = new TipoUtilizador { Id = 1, Name = "Empregado"},
                    Senha = "123",
                }); ;

                unitOfWork.Utilizadores.Add(new Utilizador
                {
                    Nome = "Garcia Gerente",
                    Email = "garcia@mail.pt",
                    Tipo = new TipoUtilizador { Id = 2, Name = "Gerente" },
                    Senha = "123",
                });
                unitOfWork.Complete();

                Console.WriteLine("-----------------------------------------{Utilizadores}-");
                users = unitOfWork.Utilizadores.GetAll();
                foreach (var item in users)
                {
                    Console.WriteLine($"{item.Identificador} - {item.Nome}");
                }
            }
        }
    }
}
