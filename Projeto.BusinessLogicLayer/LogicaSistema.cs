using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Auxiliar;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Enumerados;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public class LogicaSistema
    {

        private readonly ProjetoDBContext _context;
        public LogicaSistema(ProjetoDBContext context)
        {
            _context = context;
        }

        #region Acessos
        public Resultado AberturaSessao(Utilizador utilizador, PontoDeVenda pontoDeVenda)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                #region Validacoes
                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador obrigatório.");
                }
                if (pontoDeVenda == null)
                {
                    return new Resultado(false, "Ponto de venda obrigatório.");
                }
                #endregion

                var UtilizadorComSessaoAberta = unitOfWork.PontoDeVendaSessoes.Find(x => x.Utilizador.Identificador == utilizador.Identificador && x.DataLogout == null).FirstOrDefault();
                if (UtilizadorComSessaoAberta != null)
                {
                    if (UtilizadorComSessaoAberta.PontoDeVenda.Identificador == pontoDeVenda.Identificador)
                    {
                        return new Resultado(true, "Continuação da sessão que já estava aberta neste ponto de venda para este utilizador.", UtilizadorComSessaoAberta);
                    }

                    return new Resultado(false, $"O utilizador tem uma sessão aberta no Ponto de venda {UtilizadorComSessaoAberta.PontoDeVenda.Nome}");
                }

                var PontoDeVendaComSessaoAberta = unitOfWork.PontoDeVendaSessoes.Find(x => x.PontoDeVenda.Identificador == pontoDeVenda.Identificador && x.DataLogout == null).FirstOrDefault();
                if (PontoDeVendaComSessaoAberta != null)
                {
                    return new Resultado(false, $"Este ponto de venda tem uma sessão aberta para o utilizador {PontoDeVendaComSessaoAberta.Utilizador.Nome}");
                }

                var pontoDeVendaSessao = new PontoDeVendaSessao
                {
                    Utilizador = utilizador,
                    PontoDeVenda = pontoDeVenda,
                    DataLogin = DateTime.Now,
                };
                unitOfWork.PontoDeVendaSessoes.Add(pontoDeVendaSessao);
                unitOfWork.Complete();

                return new Resultado(true, $"Ponto de Venda: {pontoDeVenda.Nome} logado com sucesso pelo utilizador: {utilizador.Nome}.", pontoDeVendaSessao);
            }
        }

        public Resultado Login(string email, string senha)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                //unitOfWork.TipoUtilizadores.GetAll();
                var utilizador = unitOfWork.Utilizadores
                                            .Find(x => x.Email == email && x.Senha == senha && x.Ativo)
                                            .FirstOrDefault();

                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador ou senha inválidos");
                }

                if (utilizador.Tipo.TipoId == (int)TipoUtilizadorEnum.Gerente)
                {
                    return new Resultado(true, "Utilizador valido para abertura", utilizador);
                }
                else if (utilizador.Tipo.TipoId == (int)TipoUtilizadorEnum.Empregado)
                {

                    //var sessaoAbertaParaUtilizador = unitOfWork.PontoDeVendaSessoes
                    //                                        .Find(x => x.Utilizador.Identificador == utilizador.Identificador && x.DataLogout == null)
                    //                                        .FirstOrDefault();
                    //if (sessaoAbertaParaUtilizador != null)
                    //{
                    //    return new Resultado(true, $"Utilizador logado com sucesso no POS {sessaoAbertaParaUtilizador.PontoDeVenda.Nome} da Loja {sessaoAbertaParaUtilizador.PontoDeVenda.Loja.Nome}", sessaoAbertaParaUtilizador);
                    //}

                    return new Resultado(true, $"Utilizador valido para abertura", utilizador);
                }
                else
                {
                    return new Resultado(false, "Tipo de utilizador não implementado.");
                }
            }
        }


        public bool VerificaSeExisteUmUtilizadorGerente()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                return unitOfWork.Utilizadores.Find(x => x.Tipo.TipoId == (int)TipoUtilizadorEnum.Gerente && x.Ativo).Any();
            }
        }

        public Resultado Logout(PontoDeVendaSessao pontoDeVendaSessao)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var sessaoAberta = unitOfWork.PontoDeVendaSessoes.Get(pontoDeVendaSessao.Identificador);

                if (sessaoAberta == null || sessaoAberta.DataLogout != null)
                {
                    return new Resultado(true, "Sessão inválida ou inexistente");
                }

                sessaoAberta.DataLogout = DateTime.Now;
                unitOfWork.PontoDeVendaSessoes.Update(sessaoAberta);
                unitOfWork.Complete();
            }

            return new Resultado(true, "Sessão fechada com sucesso");
        }

        #endregion

        #region GestaoPontoDeVenda

        public Resultado InserePontoDeVenda(string nome, Loja loja)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }

            if (loja == null)
            {
                return new Resultado(false, "Loja obrigatória");
            }

            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                PontoDeVenda pontoDeVenda = new PontoDeVenda
                {
                    Nome = nome,
                    Loja = loja
                };

                unitOfWork.PontoDeVendas.Add(pontoDeVenda);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Ponto de venda não inserido");
                }

                return new Resultado(true, "Ponto de venda inserido com sucesso", pontoDeVenda);
            }
        }

        public List<PontoDeVenda> GetAllPontoDeVendasByLoja(Guid idLoja)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.PontoDeVendas.Find(x => x.Loja.Identificador == idLoja).ToList();
                return result;
            }
        }

        public List<PontoDeVenda> GetAllPontoDeVendas()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.PontoDeVendas.GetAll().ToList();
                return result;
            }
        }

        public Resultado ObtemPontoDeVenda(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var pontoDeVenda = unitOfWork.PontoDeVendas.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Ponto de venda lido com sucesso", pontoDeVenda);
            }
        }

        public Resultado AlteraPontoDeVenda(Guid identificador, string nome, Guid lojaId)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var loja = unitOfWork.Lojas.Get(lojaId);
                if (loja == null)
                {
                    return new Resultado(false, "Loja inexistente");
                }

                var pontoDeVenda = unitOfWork.PontoDeVendas.Get(identificador);
                if (pontoDeVenda == null)
                {
                    return new Resultado(false, "Ponto de venda inexistente");
                }
                pontoDeVenda.Nome = nome;
                pontoDeVenda.Loja = loja;

                unitOfWork.PontoDeVendas.Update(pontoDeVenda);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Ponto de venda não alterado");
                }

                return new Resultado(true, "Ponto de venda alterado com sucesso", pontoDeVenda);
            }
        }

        public Resultado ApagaPontoDeVenda(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var pontoDeVenda = unitOfWork.PontoDeVendas.Get(identificador);
                if (pontoDeVenda == null)
                {
                    return new Resultado(false, "Ponto de venda inexistente");
                }

                unitOfWork.PontoDeVendas.Remove(pontoDeVenda);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Ponto de venda não removido");
                }

                return new Resultado(true, "Ponto de venda removido com sucesso");
            }
        }

        #endregion

        #region GestaoUtilizador
        public Resultado InsereUtilizador(string nome, string email, string senha, TipoUtilizadorEnum tipo)
        {
            #region Validacoes

            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            if (String.IsNullOrEmpty(email))
            {
                return new Resultado(false, "Email obrigatório");
            }
            if (String.IsNullOrEmpty(senha))
            {
                return new Resultado(false, "Senha obrigatória");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var tipoUtilizador = unitOfWork.TipoUtilizadores.GetTipoUtilizadorByEnum(tipo);

                Utilizador user = new Utilizador
                {
                    Nome = nome,
                    Email = email,
                    Senha = senha,
                    Tipo = tipoUtilizador
                };

                unitOfWork.Utilizadores.Add(user);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Utilizador não registado");
                }

                return new Resultado(true, "Utilizador registado com sucesso", user);
            }

        }

        public List<Utilizador> GetAllUtilizadores()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Utilizadores.GetAll().ToList();
                return result;
            }
        }

        public Resultado ObtemUtilizador(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var utilizador = unitOfWork.Utilizadores.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Utilizador lido com sucesso", utilizador);
            }
        }

        public Resultado AlteraUtilizador(Guid identificador, string nome, string email, string senha, TipoUtilizadorEnum tipo)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            if (String.IsNullOrEmpty(email))
            {
                return new Resultado(false, "Email obrigatório");
            }
            if (String.IsNullOrEmpty(senha))
            {
                return new Resultado(false, "Senha obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var utilizador = unitOfWork.Utilizadores.Get(identificador);
                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador inexistente");
                }

                var tipoUtilizador = unitOfWork.TipoUtilizadores.GetTipoUtilizadorByEnum(tipo);

                utilizador.Nome = nome;
                utilizador.Email = email;
                utilizador.Senha = senha;
                utilizador.Tipo = tipoUtilizador;

                unitOfWork.Utilizadores.Update(utilizador);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Utilizador não alterado");
                }

                return new Resultado(true, "Utilizador alterado com sucesso", utilizador);
            }
        }

        public Resultado ApagaUtilizador(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var utilizador = unitOfWork.Utilizadores.Get(identificador);
                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador inexistente");
                }

                unitOfWork.Utilizadores.Remove(utilizador);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Utilizador não removido");
                }

                return new Resultado(true, "Utilizador removido com sucesso");
            }
        }

        #endregion

        #region GestaoLoja

        public Resultado InsereLoja(string nome, string numeroFiscal, string email, string telefone, Morada morada)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                Loja loja = new Loja
                {
                    Nome = nome,
                    NumeroFiscal = numeroFiscal,
                    Email = email,
                    Telefone = telefone,
                    Morada = morada
                };

                unitOfWork.Lojas.Add(loja);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Loja não inserida");
                }

                return new Resultado(true, "Loja inserida com sucesso", loja);
            }
        }

        public Resultado AlteraLoja(Guid identificador, string nome, string numeroFiscal, string email, string telefone, Morada morada)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var loja = unitOfWork.Lojas.Get(identificador);
                if (loja == null)
                {
                    return new Resultado(false, "Loja inexistente");
                }

                loja.Nome = nome;

                unitOfWork.Lojas.Update(loja);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Loja não alterada");
                }

                return new Resultado(true, "Loja alterada com sucesso", loja);
            }
        }

        public Resultado ObtemLoja(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var loja = unitOfWork.Lojas.Get(identificador);

                return new Resultado(true, "Loja lida com sucesso", loja);
            }
        }

        public Resultado ApagaLoja(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var loja = unitOfWork.Lojas.Get(identificador);
                if (loja == null)
                {
                    return new Resultado(false, "Loja inexistente");
                }

                unitOfWork.Lojas.Remove(loja);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Loja não removida");
                }

                return new Resultado(true, "Loja removida com sucesso");
            }
        }

        public List<Loja> GetAllLojasComPontosDeVenda()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Lojas.GetAllLojasComPontosDeVenda();
                return result;
            }
        }

        public List<Loja> GetAllLojas()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Lojas.GetAll().ToList();
                return result;
            }
        }

        #endregion

        #region Marcas

        public Resultado InsereMarca(string nome, string origem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                MarcaProduto marca = new MarcaProduto
                {
                    Nome = nome,
                    Origem = origem
                };

                unitOfWork.MarcaProdutos.Add(marca);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Marca não inserida");
                }

                return new Resultado(true, "Marca inserida com sucesso", marca);
            }
        }

        public List<MarcaProduto> GetAllMarcas()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.MarcaProdutos.GetAll().ToList();
                return result;
            }
        }


        public Resultado ObtemMarca(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var marca = unitOfWork.MarcaProdutos.Get(identificador);

                return new Resultado(true, "Marca lida com sucesso", marca);
            }
        }

        public Resultado AlteraMarca(Guid identificador, string nome, string origem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var marca = unitOfWork.MarcaProdutos.Get(identificador);
                if (marca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                marca.Nome = nome;
                marca.Origem = origem;

                unitOfWork.MarcaProdutos.Update(marca);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Marca não alterada");
                }

                return new Resultado(true, "Marca alterada com sucesso", marca);
            }
        }

        public Resultado ApagaMarca(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var marca = unitOfWork.MarcaProdutos.Get(identificador);
                if (marca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                unitOfWork.MarcaProdutos.Remove(marca);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Marca não removida");
                }

                return new Resultado(true, "Marca removida com sucesso");
            }
        }

        #endregion

        #region Categorias

        public Resultado InsereCategoria(string nome, int ordem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                CategoriaProduto categoria = new CategoriaProduto
                {
                    Nome = nome,
                    OrdemApresentacao = ordem
                };

                unitOfWork.CategoriaProdutos.Add(categoria);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Categoria não inserida");
                }

                return new Resultado(true, "Categoria inserida com sucesso", categoria);
            }
        }

        public List<CategoriaProduto> GetAllCategorias()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.CategoriaProdutos.GetAll().ToList();
                return result;
            }
        }

        public Resultado ObtemCategoria(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var categoria = unitOfWork.CategoriaProdutos.Get(identificador);

                return new Resultado(true, "Categoria lida com sucesso", categoria);
            }
        }

        public Resultado AlteraCategoria(Guid identificador, string nome, int ordem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var categoria = unitOfWork.CategoriaProdutos.Get(identificador);
                if (categoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }

                categoria.Nome = nome;
                categoria.OrdemApresentacao = ordem;

                unitOfWork.CategoriaProdutos.Update(categoria);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Categoria não alterada");
                }

                return new Resultado(true, "Categoria alterada com sucesso", categoria);
            }
        }

        public Resultado ApagaCategoria(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var categoria = unitOfWork.CategoriaProdutos.Get(identificador);
                if (categoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }

                unitOfWork.CategoriaProdutos.Remove(categoria);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Categoria não removida");
                }

                return new Resultado(true, "Categoria removida com sucesso");
            }
        }


        #endregion

        #region Stocks
        public List<Estoque> GetAllEstoque()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Estoques.GetAll().ToList();
                return result;
            }
        }

        public Resultado InsereEstoque(Guid produto, int qtd)
        {

            #region Validacoes
            if (qtd < 1)
            {
                return new Resultado(false, "Quantidade deve ser superior a zero");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var insProduto = unitOfWork.Produtos.Get(produto);
                if (insProduto == null)
                {
                    return new Resultado(false, "Produto inexistente");
                }

                List<Estoque> listaEstoques = new List<Estoque>();
                for (int i = 0; i < qtd; i++)
                {
                    Estoque estoque = new Estoque
                    {
                        Produto = insProduto,
                        DataEntrada = DateTime.Today,
                        NumeroSerie = "Não definido"
                    };
                    listaEstoques.Add(estoque);
                }

                unitOfWork.Estoques.AddRange(listaEstoques);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Estoque não inserido");
                }

                return new Resultado(true, "Estoque inserido com sucesso", listaEstoques);
            }
        }

        public Resultado ObtemEstoque(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var estoque = unitOfWork.Estoques.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Estoque lido com sucesso", estoque);
            }
        }

        public Resultado AlteraEstoque(Guid identificador, string serie)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var estoque = unitOfWork.Estoques.Get(identificador);
                if (estoque == null)
                {
                    return new Resultado(false, "Estoque inexistente");
                }

                estoque.NumeroSerie = serie;

                unitOfWork.Estoques.Update(estoque);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Estoque não alterado");
                }
                return new Resultado(true, "Estoque alterado com sucesso", estoque);
            }
        }

        public Resultado ApagaEstoque(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var estoque = unitOfWork.Estoques.Get(identificador);
                if (estoque == null)
                {
                    return new Resultado(false, "Estoque inexistente");
                }

                unitOfWork.Estoques.Remove(estoque);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Estoque não removido");
                }

                return new Resultado(true, "Estoque removido com sucesso");
            }
        }


        #endregion

        #region Produtos

        public List<Produto> GetAllProdutos()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Produtos.GetAll().ToList();
                return result;
            }
        }

        public Resultado InsereProduto(string nome, Guid categoria, Guid marca, string ean, decimal preco)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var insCategoria = unitOfWork.CategoriaProdutos.Get(categoria);
                if (insCategoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }
                var insMarca = unitOfWork.MarcaProdutos.Get(marca);
                if (insMarca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                Produto produto = new Produto
                {
                    Nome = nome,
                    Categoria = insCategoria,
                    Marca = insMarca,
                    Ean = ean,
                    PrecoUnitario = preco

                };

                unitOfWork.Produtos.Add(produto);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Produto não inserido");
                }

                return new Resultado(true, "Produto inserido com sucesso", produto);
            }
        }

        public Resultado AlteraProduto(Guid identificador, string nome, Guid categoria, Guid marca, string ean, decimal preco)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var insCategoria = unitOfWork.CategoriaProdutos.Get(categoria);
                if (insCategoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }
                var insMarca = unitOfWork.MarcaProdutos.Get(marca);
                if (insMarca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }


                var produto = unitOfWork.Produtos.Get(identificador);
                if (produto == null)
                {
                    return new Resultado(false, "Produto inexistente");
                }

                produto.Nome = nome;
                produto.Categoria = insCategoria;
                produto.Marca = insMarca;
                produto.Ean = ean;
                produto.PrecoUnitario = preco;

                unitOfWork.Produtos.Update(produto);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Produto não alterado");
                }
                return new Resultado(true, "Produto alterado com sucesso", produto);
            }
        }

        public Resultado ObtemProduto(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var produto = unitOfWork.Produtos.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Produto lido com sucesso", produto);
            }
        }

        public Resultado ObtemProdutosPorCategoria(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var produtos = unitOfWork.Produtos.Find(x => x.Categoria.Identificador == identificador).ToList();

                return new Resultado(true, "Produtos lidos com sucesso", produtos);
            }
        }


        public Resultado ApagaProduto(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var produto = unitOfWork.Produtos.Get(identificador);
                if (produto == null)
                {
                    return new Resultado(false, "Produto inexistente");
                }

                unitOfWork.Produtos.Remove(produto);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Produto não removido");
                }

                return new Resultado(true, "Produto removido com sucesso");
            }
        }

        #endregion

        #region Vendas


        public Resultado Pagamento(Guid vendaId, string nome, string nif, string telefone, TipoPagamentoEnum tipo)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {

                // Se nif preenchido vai verificar se existe cliente, senão cria
                Cliente cliente = null;

                if (!String.IsNullOrEmpty(nif))
                {
                    cliente = unitOfWork.Clientes.Find(x => x.NumeroFiscal == nif).FirstOrDefault();

                    if (cliente == null)
                    {
                        cliente = new Cliente()
                        {
                            Nome = nome,
                            NumeroFiscal = nif,
                            Telefone = telefone
                        };

                        unitOfWork.Clientes.Add(cliente);
                    }
                }

                var vendaActual = unitOfWork.Vendas.Get(vendaId);
                if (vendaActual == null)
                {
                    return new Resultado(false, "Venda inexistente");
                }

                vendaActual.Cliente = cliente;
                vendaActual.TipoPagamento = unitOfWork.TipoPagamentos.GetTipoPagamentoByEnum(tipo);
                vendaActual.DataHoraVenda = DateTime.Now;

                var total = vendaActual.DetalheVendas.Sum(x => x.PrecoFinal);
                vendaActual.ValorPagamento = total;

                unitOfWork.Vendas.Update(vendaActual);

                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Venda não terminada");
                }
                return new Resultado(true, "Venda terminada com sucesso");
            }
        }


        /// <summary>
        /// Devolve a venda em curso para a sessão, se esta não existir cria uma venda nova
        /// </summary>
        /// <param name="pontoVendaSessaoId"></param>
        /// <returns></returns>
        public Resultado GetVendaEmCurso(Guid pontoVendaSessaoId)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var venda = unitOfWork.Vendas.GetVendaEmCurso(pontoVendaSessaoId);
                if (venda == null)
                {
                    var pontoVendaSessao = unitOfWork.PontoDeVendaSessoes.Get(pontoVendaSessaoId);

                    Venda novaVenda = new Venda()
                    {
                        PontoDeVendaSessao = pontoVendaSessao
                    };
                    unitOfWork.Vendas.Add(novaVenda);

                    var affected = unitOfWork.Complete();
                    if (affected == 0)
                    {
                        return new Resultado(false, "Venda não Criada");
                    }
                    return new Resultado(true, "Venda criada", novaVenda);
                }
                return new Resultado(true, "Venda aberta", venda);
            }
        }

        public List<TotalSessao> GetTotaisSessao(Guid pontoVendaSessaoId)
        {
            List<TotalSessao> result = null;
            using (var unitOfWork = new UnitOfWork(_context))
            {
                result = unitOfWork.PontoDeVendaSessoes.GetTotalSessao(pontoVendaSessaoId);

            }
            return result;
        }

        public Resultado FechaSessaoVenda(Guid pontoVendaSessaoId)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var pontoVendaSessao = unitOfWork.PontoDeVendaSessoes.Get(pontoVendaSessaoId);
                if (pontoVendaSessao == null)
                {
                    return new Resultado(false, "Sessão inexistente");
                }

                var venda = unitOfWork.Vendas.GetVendaEmCurso(pontoVendaSessaoId);
                if (venda.DetalheVendas?.Count > 0)
                {
                    return new Resultado(false, "Não pode fechar a sessão com uma venda em curso!");
                }

                pontoVendaSessao.DataLogout = DateTime.Now;
                unitOfWork.PontoDeVendaSessoes.Update(pontoVendaSessao);

                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Erro no fecho de Sessao.");
                }


            }
            return new Resultado(true, "Sessao fechada");
        }

        public Resultado AddProdutoVenda(Guid vendaId, Guid produtoId)
        {
            #region Validacoes
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                Guid newDetalheVendaId = Guid.NewGuid();

                //Procurar se o produto existe em Estoque
                var estoqueProduto = unitOfWork.Estoques.Find(e => !e.DetalheVendaId.HasValue && e.Produto.Identificador == produtoId).FirstOrDefault();

                if (estoqueProduto == null)
                {
                    return new Resultado(false, "Não existe stock do produto");
                }
                estoqueProduto.DetalheVendaId = newDetalheVendaId;

                unitOfWork.Estoques.Update(estoqueProduto);

                DetalheVenda detalheVenda = new DetalheVenda()
                {
                    VendaId = vendaId,
                    EstoqueId = estoqueProduto.Identificador,
                    PrecoFinal = estoqueProduto.Produto.PrecoUnitario,
                };
                unitOfWork.DetalheVendas.Add(detalheVenda);

                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Produto não adicionado");
                }

            }

            return new Resultado(true, "Produto adicionado com sucesso");
        }

        public Resultado GetDetalheVendasPorCompra(Guid vendaId)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var detalheVendasList = unitOfWork.DetalheVendas.Find(e => e.VendaId == vendaId).ToList();

                return new Resultado(true, "Consulta com sucesso", detalheVendasList);
            }
        }

        public Resultado DeleteAllDetalheVendasPorCompra(Guid vendaId)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var detalheVendasList = unitOfWork.DetalheVendas.Find(e => e.VendaId == vendaId).ToList();
                var estoqueList = unitOfWork.Estoques.Find(e => e.DetalheVenda.VendaId == vendaId).ToList();

                if (detalheVendasList.Count != estoqueList.Count)
                {
                    return new Resultado(false, "Erro na quantidade de items do carrinho e estoque");
                }

                foreach (var item in estoqueList)
                {
                    item.DetalheVenda = null;
                    item.DetalheVendaId = null;
                }
                unitOfWork.DetalheVendas.RemoveRange(detalheVendasList);
                unitOfWork.Estoques.UpdateRange(estoqueList);

                unitOfWork.Complete();

                return new Resultado(true, "Items removidos com sucesso");
            }
        }

        public Resultado DeleteDetalheVenda(Guid detalheVendaId)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var detalheVenda = unitOfWork.DetalheVendas.Get(detalheVendaId);
                var estoque = unitOfWork.Estoques.Find(e => e.DetalheVenda.Identificador == detalheVendaId).FirstOrDefault();

                if (detalheVenda == null || estoque == null)
                {
                    return new Resultado(false, "Erro na no item do carrinho e estoque");
                }

                estoque.DetalheVenda = null;
                estoque.DetalheVendaId = null;

                unitOfWork.DetalheVendas.Remove(detalheVenda);
                unitOfWork.Estoques.Update(estoque);

                unitOfWork.Complete();

                return new Resultado(true, "Item removido com sucesso");
            }
        }


        #endregion


    }
}
