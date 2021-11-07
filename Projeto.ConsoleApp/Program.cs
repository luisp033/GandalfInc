using Projeto.Lib;
using Projeto.Lib.Entidades;
using Projeto.Lib.Faturacao;
using Projeto.Lib.Repositorios;
using System;

namespace Projeto.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Classe Utilizadores
            //Console.WriteLine("-------------------------------------------- [Lista todos os utilizadores] \n");
            //RepositorioUtilizador utilizadores = new RepositorioUtilizador();
            //var listaUtilizadores = utilizadores.ObterTodos();
            //foreach (var item in listaUtilizadores)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("\n-------------------------------------------- [Inserir novo utilizador (Nuno)] \n");
            //utilizadores.Criar(new Utilizador { Tipo = TipoUtilizador.Empregado, Nome = "Nuno", Senha = "1234", Email = "Nuno@mail.pt" });

            //Console.WriteLine("-------------------------------------------- [Lista todos os utilizadores] \n");
            //listaUtilizadores = utilizadores.ObterTodos();
            //foreach (var item in listaUtilizadores)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("\n-------------------------------------------- [Obter o user por Nome ='Nuno'] \n");
            //var utilizadorNuno = utilizadores.ObterPorNome("Nuno");
            //Console.WriteLine(utilizadorNuno);

            //Console.WriteLine("\n-------------------------------------------- [Obter o user por identificador] \n");
            //var utilizadorNuno2 = utilizadores.ObterPorIdentificador(utilizadorNuno.Identificador);
            //Console.WriteLine(utilizadorNuno2);

            //Console.WriteLine("\n-------------------------------------------- [Atualizar o estado activo do utilizador Nuno] \n");
            //var utilizadorNuno3 = new Utilizador { Tipo = TipoUtilizador.Empregado, Nome = "Nuno", Senha = "1234", Email = "Nuno@mail.pt", Ativo = false };
            //utilizadores.Atualizar(utilizadorNuno, utilizadorNuno3);
            //listaUtilizadores = utilizadores.ObterTodos();
            //foreach (var item in listaUtilizadores)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("\n-------------------------------------------- [Remover o utilizador Nuno] \n");
            //utilizadores.Apagar(utilizadorNuno);
            //listaUtilizadores = utilizadores.ObterTodos();
            //foreach (var item in listaUtilizadores)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion


            Console.WriteLine("\n\t\t\t\t * * * * * * * * [GANDALF INC] * * * * * * * * \n");

            Console.WriteLine("\n-------------------------------------------- [Lojas da Gandalf Inc]\n");

            RepositorioUtilizador utilizadores = new RepositorioUtilizador();
            RepositorioLoja lojas = new RepositorioLoja(utilizadores);
            RepositorioPontoDeVenda pontosDeVenda = new RepositorioPontoDeVenda(utilizadores, lojas);
            RepositorioCategoriaProduto categorias = new RepositorioCategoriaProduto();
            RepositorioMarcaProduto marcas = new RepositorioMarcaProduto();
            RepositorioProduto produtos = new RepositorioProduto(categorias,marcas);
            RepositorioEstoque estoques = new RepositorioEstoque(produtos);
            RepositorioCliente clientes = new RepositorioCliente();

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

            Console.WriteLine($"\nInicio da Nova Venda\n");




        }

    }
}
