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
    public partial class FormGestaoStocksEdit : Form
    {
        public Guid? Id { get; set; }
        private bool editMode = false;

        public FormGestaoStocksEdit(Guid? id = null)
        {
            InitializeComponent();
            Id = id;
            editMode = (Id != null);

            LoadCombo();
            LoadForm();

        }

        private void LoadForm()
        {
            if (!editMode)
            {
                btnDelete.Visible = false;
                lblSerie.Visible = false;
                txtSerie.Visible = false;
                return;
            }

            lblQtd.Visible = false;
            numQtd.Visible = false;
            cmbProduto.Enabled = false;

            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);
                var resultado = sistema.ObtemEstoque(Id.Value);

                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                    Close();
                }

                var estoque = ((Estoque)resultado.Objeto);
                txtSerie.Text = estoque.NumeroSerie;
                cmbProduto.SelectedValue = estoque.Produto.Identificador;

            }
        }

        private void LoadCombo()
        {
            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);
                var produtos = sistema.GetAllProdutos();
                this.cmbProduto.DataSource = produtos;
                this.cmbProduto.DisplayMember = "Nome";
                this.cmbProduto.ValueMember = "Identificador";
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja apagar o estoque seleccionado?", "Apagar Estoque", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                using (var contexto = new ProjetoDBContext())
                {
                    LogicaSistema sistema = new LogicaSistema(contexto);
                    var resultado = sistema.ApagaEstoque(Id.Value);

                    if (!resultado.Sucesso)
                    {
                        MessageBox.Show(resultado.Mensagem);
                    }
                }
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);

                if (editMode)
                {
                    var resultado = sistema.AlteraEstoque(Id.Value, txtSerie.Text);
                    if (!resultado.Sucesso)
                    {
                        MessageBox.Show(resultado.Mensagem);
                    }
                }
                else
                {
                    var resultado = sistema.InsereEstoque((Guid)cmbProduto.SelectedValue, (int)numQtd.Value);
                    if (!resultado.Sucesso)
                    {
                        MessageBox.Show(resultado.Mensagem);
                    }
                }

            }
            Close();
        }
    }
}
