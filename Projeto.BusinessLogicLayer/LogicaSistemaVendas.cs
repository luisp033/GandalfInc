using Projeto.DataAccessLayer.Auxiliar;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Enumerados;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {

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
        /// Devolve a venda em curso para o utilizador logado, , se esta não existir cria uma venda nova
        /// </summary>
        public Resultado GetVendaEmCursoForUser(string email)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {

                var utilizador = unitOfWork.Utilizadores.Find(x=>x.Email == email).FirstOrDefault();

                var sessaoAberta = unitOfWork.PontoDeVendaSessoes.Find(x => x.Utilizador.Identificador== utilizador.Identificador && x.DataLogout== null).FirstOrDefault();

                var venda = unitOfWork.Vendas.GetVendaEmCurso(sessaoAberta.Identificador);

                if (venda == null)
                {

                    Venda novaVenda = new Venda()
                    {
                        PontoDeVendaSessao = sessaoAberta
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


        /// <summary>
        /// Devolve a venda em curso para a sessão, se esta não existir cria uma venda nova
        /// </summary>
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
    }
}
