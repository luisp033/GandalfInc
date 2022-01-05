using Projeto.DataAccessLayer;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestao : Form
    {


        private readonly ProjetoDBContext contexto;
        public FormGestao(ProjetoDBContext context)
        {
            InitializeComponent();
            contexto = context;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            var formLogin = (FormLogin)Tag;

            formLogin.Top = this.Top;
            formLogin.Left = this.Left;
            formLogin.Show();
            Close();
        }

        private void btnLojas_Click(object sender, EventArgs e)
        {
            using (FormGestaoLojas frm = new FormGestaoLojas(contexto)) 
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this); 
            }
        }


        private void btnUtilizadores_Click(object sender, EventArgs e)
        {
            using (FormGestaoUtilizadores frm = new FormGestaoUtilizadores(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnPontoVendas_Click(object sender, EventArgs e)
        {
            using (FormGestaoPontoDeVendas frm = new FormGestaoPontoDeVendas(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            using (FormGestaoCategorias frm = new FormGestaoCategorias(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            using (FormGestaoMarcas frm = new FormGestaoMarcas(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            using (FormGestaoProdutos frm = new FormGestaoProdutos(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            using (FormGestaoStocks frm = new FormGestaoStocks(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }
    }
}
