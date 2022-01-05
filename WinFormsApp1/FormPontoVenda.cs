using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormPontoVenda : Form
    {

        #region Variables and Constructors
        public PontoDeVendaSessao Sessao { get; set; }
        public Utilizador Utilizador { get; set; }

        private readonly ProjetoDBContext contexto;

        public FormPontoVenda(ProjetoDBContext context)
        {
            InitializeComponent();

            contexto = context;

            LoadCategorias();
        }

        #endregion

        #region Events
        private void btnCategoria_Click(object sender, EventArgs e)
        {
            Button btnCategoria = (Button)sender;

            foreach (var item in this.flowLayoutCategorias.Controls)
            {
                var itemButton = (Button)item;
                if (itemButton == btnCategoria)
                {
                    itemButton.BackColor = Color.Yellow;
                    LoadProdutosPorCategoria((Guid)itemButton.Tag);
                }
                else
                {
                    itemButton.BackColor = Color.White;
                }
            }
        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            Button btnProduto = (Button)sender;
            var produtoId = (Guid)btnProduto.Tag;

            LogicaSistema sistema = new LogicaSistema(contexto);

            var vendaResult = sistema.GetVendaEmCurso(Sessao.Identificador);
            if (!vendaResult.Sucesso)
            {
                EscreveMensagem(vendaResult.Mensagem, vendaResult.Sucesso);
                return;
            }
            var produtoAdicionado = sistema.AddProdutoVenda(((Venda)vendaResult.Objeto).Identificador, produtoId);
            EscreveMensagem(produtoAdicionado.Mensagem, produtoAdicionado.Sucesso);

            AtualizaCarrinhoTotais();
        }

        private void btnItemCarrinho_Click(object sender, EventArgs e)
        {
            Button btnItemCarrinho = (Button)sender;
            var detalheVendaId = (Guid)btnItemCarrinho.Tag;
            LogicaSistema sistema = new LogicaSistema(contexto);

            var produtoRemovido = sistema.DeleteDetalheVenda(detalheVendaId);

            EscreveMensagem(produtoRemovido.Mensagem, produtoRemovido.Sucesso);

            AtualizaCarrinhoTotais();
        }

        private void FormPontoVenda_FormClosed(object sender, FormClosedEventArgs e)
        {
            var formLogin = (FormLogin)Tag;
            formLogin.Top = this.Top;
            formLogin.Left = this.Left;
            formLogin.Show();
        }

        private void FormPontoVenda_Load(object sender, EventArgs e)
        {
            AtualizaCarrinhoTotais();

            lblInfo.Text = $"Loja : {Sessao.PontoDeVenda.Loja.Nome} Ponto de venda: {Sessao.PontoDeVenda.Nome}  Utilizador : {Sessao.Utilizador.Nome}  Sessão aberta : {Sessao.DataLogin}";

            LimparMensagem();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelarCompra();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            LogicaSistema sistema = new LogicaSistema(contexto);
            var vendaResult = sistema.GetVendaEmCurso(Sessao.Identificador);
            if (!vendaResult.Sucesso)
            {
                EscreveMensagem(vendaResult.Mensagem, vendaResult.Sucesso);
                return;
            }

            var vendaAtual = (Venda)vendaResult.Objeto;
            if (vendaAtual.DetalheVendas.Count == 0)
            {
                EscreveMensagem("Não é possível terminar uma venda sem produtos.", false);
                return;
            }

            using (FormPagamento frm = new FormPagamento(contexto, vendaAtual.Identificador))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = frm.ShowDialog(this);
                
                if (dr == DialogResult.OK)
                {
                    AtualizaCarrinhoTotais();
                }
    
            }
        }

        private void btnCleanMessage_Click(object sender, EventArgs e)
        {
            LimparMensagem();
        }
        #endregion

        #region Methods

        private void CancelarCompra()
        {
            DialogResult dr = MessageBox.Show("Deseja cancelar toda a compra?", "Cancelar Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No)
            {
                return;
            }

            LogicaSistema sistema = new LogicaSistema(contexto);

            var vendaResult = sistema.GetVendaEmCurso(Sessao.Identificador);
            if (!vendaResult.Sucesso)
            {
                EscreveMensagem(vendaResult.Mensagem, vendaResult.Sucesso);
                return;
            }

            var detalheVendaResult = sistema.DeleteAllDetalheVendasPorCompra(((Venda)vendaResult.Objeto).Identificador);
            if (!detalheVendaResult.Sucesso)
            {
                EscreveMensagem(detalheVendaResult.Mensagem, detalheVendaResult.Sucesso);
                return;
            }
            AtualizaCarrinhoTotais();
        }

        private void LoadProdutosPorCategoria(Guid identificador)
        {
            this.flowLayoutPanelProdutos.Controls.Clear();

            LogicaSistema sistema = new LogicaSistema(contexto);
            foreach (var item in ((List<Produto>)sistema.ObtemProdutosPorCategoria(identificador).Objeto))
            {
                Button btnProduto = new Button();
                btnProduto.Height = 60;
                btnProduto.Width = 100;
                btnProduto.Text = item.Nome;
                btnProduto.BackColor = Color.White;
                btnProduto.Tag = item.Identificador;
                this.flowLayoutPanelProdutos.Controls.Add(btnProduto);
                btnProduto.Click += new EventHandler(btnProduto_Click);
            }
        }

        private void LoadCategorias()
        {
            Guid? categoriaSelecionada = null;

            int c = 0;
            LogicaSistema sistema = new LogicaSistema(contexto);
            foreach (var item in sistema.GetAllCategorias())
            {
                Button btnCategoria = new Button();
                btnCategoria.Height = 60;
                btnCategoria.Width = 100;
                btnCategoria.Text = item.Nome;
                btnCategoria.BackColor = Color.White;
                if (c == 0)
                {
                    categoriaSelecionada = item.Identificador;
                    btnCategoria.BackColor = Color.Yellow;
                }
                btnCategoria.Tag = item.Identificador;
                this.flowLayoutCategorias.Controls.Add(btnCategoria);

                btnCategoria.Click += new EventHandler(btnCategoria_Click);
                c++;
            }

            if (categoriaSelecionada != null)
            {
                LoadProdutosPorCategoria(categoriaSelecionada.Value);
            }
        }

        private void AtualizaCarrinhoTotais()
        {
            this.flowLayoutDetalheVenda.Controls.Clear();

            LogicaSistema sistema = new LogicaSistema(contexto);

            var vendaResult = sistema.GetVendaEmCurso(Sessao.Identificador);
            if (!vendaResult.Sucesso)
            {
                EscreveMensagem(vendaResult.Mensagem, vendaResult.Sucesso);
                return;
            }

            //Todas os detalhes de venda da compra em cusro
            var detalheVendaResult = sistema.GetDetalheVendasPorCompra(((Venda)vendaResult.Objeto).Identificador);
            if (!detalheVendaResult.Sucesso)
            {
                EscreveMensagem(detalheVendaResult.Mensagem, detalheVendaResult.Sucesso);
                return;
            }

            var items = (List<DetalheVenda>)detalheVendaResult.Objeto;
            decimal totalValue = 0.0m;

            foreach (var item in items)
            {
                Button btnItemCarrinho = new Button();
                btnItemCarrinho.Height = 30;
                btnItemCarrinho.Width = 150;
                btnItemCarrinho.Text = $"{item.PrecoFinal} - {item.Estoque.Produto.Nome}";
                btnItemCarrinho.Tag = item.Identificador;
                this.flowLayoutDetalheVenda.Controls.Add(btnItemCarrinho);
                totalValue += item.PrecoFinal;
                btnItemCarrinho.Click += new EventHandler(btnItemCarrinho_Click);
            }

            lblTotalEuros.Text = totalValue.ToString();
            lblTotalItems.Text = items.Count().ToString();

        }

        private void LimparMensagem()
        {
            lblMessage.Text = String.Empty;
        }

        private void EscreveMensagem(string message, bool success)
        {
            lblMessage.BackColor = success ? Color.Transparent : Color.Red;
            lblMessage.ForeColor = success ? Color.Green : Color.White;
            lblMessage.Text = message;
        }
        #endregion

        private void btnTotal_Click(object sender, EventArgs e)
        {

            using (FormFechoCaixa frm = new FormFechoCaixa(contexto, Sessao.Identificador))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = frm.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    var formLogin = (FormLogin)Tag;

                    formLogin.Top = this.Top;
                    formLogin.Left = this.Left;
                    formLogin.Show();
                    Close();
                }
            }
        }
    }
}
