using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormPontoVenda : Form
    {
        public FormPontoVenda()
        {
            InitializeComponent();

            LoadCategorias();
        }

        private void LoadProdutosPorCategoria(Guid identificador)
        {
            this.flowLayoutPanelProdutos.Controls.Clear();
            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);
                foreach (var item in ((List<Produto>)sistema.ObtemProdutosPorCategoria(identificador).Objeto))                {
                    Button btnProduto = new Button();
                    btnProduto.Height = 60;
                    btnProduto.Width = 100;
                    btnProduto.Text = item.Nome;
                    btnProduto.BackColor = Color.White;
                    btnProduto.Tag = item.Identificador;
                    this.flowLayoutPanelProdutos.Controls.Add(btnProduto);
                    //btnCategoria.Click += new EventHandler(btnCategoria_Click);
                }



            }

        }

            private void LoadCategorias() {

            using (var contexto = new ProjetoDBContext())
            {
                int c = 0;
                LogicaSistema sistema = new LogicaSistema(contexto);
                foreach (var item in sistema.GetAllCategorias())
                {
                    Button btnCategoria = new Button();
                    btnCategoria.Height = 60;
                    btnCategoria.Width = 100;
                    btnCategoria.Text = item.Nome;
                    btnCategoria.BackColor = c == 0 ? Color.Yellow : Color.White;
                    btnCategoria.Tag = item.Identificador;
                    this.flowLayoutCategorias.Controls.Add(btnCategoria);
                    btnCategoria.Click += new EventHandler(btnCategoria_Click);
                    c++;
                } 
            }
        }

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

        private void FormPontoVenda_FormClosed(object sender, FormClosedEventArgs e)
        {
            var formLogin = (FormLogin)Tag;
            formLogin.Top = this.Top;
            formLogin.Left = this.Left;
            formLogin.Show();
        }
    }
}
