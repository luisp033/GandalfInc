using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestaoProdutosEdit : Form
    {

        public Guid? Id { get; set; }
        private bool editMode = false;
        private readonly ProjetoDBContext contexto;

        public FormGestaoProdutosEdit(ProjetoDBContext context, Guid? id = null)
        {
            InitializeComponent();
            contexto = context;
            Id = id;
            editMode = (Id != null);

            LoadCombo();
            LoadForm();

        }

        private void LoadCombo()
        {

            LogicaSistema sistema = new LogicaSistema(contexto);

            var categorias = sistema.GetAllCategorias();
            this.cmbCategoria.DataSource = categorias;
            this.cmbCategoria.DisplayMember = "Nome";
            this.cmbCategoria.ValueMember = "Identificador";

            var marcas = sistema.GetAllMarcas();
            this.cmbMarca.DataSource = marcas;
            this.cmbMarca.DisplayMember = "Nome";
            this.cmbMarca.ValueMember = "Identificador";
        }


        private void LoadForm()
        {
            if (!editMode)
            {
                btnDelete.Visible = false;
                return;
            }


            LogicaSistema sistema = new LogicaSistema(contexto);
            var resultado = sistema.ObtemProduto(Id.Value);

            if (!resultado.Sucesso)
            {
                MessageBox.Show(resultado.Mensagem);
                Close();
            }

            var produto = ((Produto)resultado.Objeto);
            txtNome.Text = produto.Nome;
            txtEan.Text = produto.Ean;
            numPreco.Value = produto.PrecoUnitario;
            cmbCategoria.SelectedValue = produto.Categoria.Identificador;
            cmbMarca.SelectedValue = produto.Marca.Identificador;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja apagar o produto seleccionado?", "Apagar Produto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {

                LogicaSistema sistema = new LogicaSistema(contexto);
                var resultado = sistema.ApagaProduto(Id.Value);

                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
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

            LogicaSistema sistema = new LogicaSistema(contexto);

            if (editMode)
            {
                var resultado = sistema.AlteraProduto(Id.Value, txtNome.Text, (Guid)cmbCategoria.SelectedValue, (Guid)cmbMarca.SelectedValue, txtEan.Text, numPreco.Value);
                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            else
            {
                var resultado = sistema.InsereProduto(txtNome.Text, (Guid)cmbCategoria.SelectedValue, (Guid)cmbMarca.SelectedValue, txtEan.Text, numPreco.Value);
                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            Close();
        }
    }
}
