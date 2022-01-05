using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestaoPontoDeVendasEdit : Form
    {

        public Guid? Id { get; set; }
        private bool editMode = false;
        private readonly ProjetoDBContext contexto;

        public FormGestaoPontoDeVendasEdit(ProjetoDBContext context, Guid? id = null)
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
            var lojas = sistema.GetAllLojas();
            this.cmbLoja.DataSource = lojas;
            this.cmbLoja.DisplayMember = "Nome";
            this.cmbLoja.ValueMember = "Identificador";
        }

        private void LoadForm()
        {
            if (!editMode)
            {
                btnDelete.Visible = false;
                return;
            }

            LogicaSistema sistema = new LogicaSistema(contexto);
            var resultado = sistema.ObtemPontoDeVenda(Id.Value);

            if (!resultado.Sucesso)
            {
                MessageBox.Show(resultado.Mensagem);
                Close();
            }

            var pontoDeVenda = ((PontoDeVenda)resultado.Objeto);
            txtNome.Text = pontoDeVenda.Nome;
            cmbLoja.SelectedValue = pontoDeVenda.Loja.Identificador;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            LogicaSistema sistema = new LogicaSistema(contexto);

            if (editMode)
            {
                var resultado = sistema.AlteraPontoDeVenda(Id.Value, txtNome.Text, (Guid)cmbLoja.SelectedValue);
                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            else
            {

                var loja = sistema.ObtemLoja((Guid)cmbLoja.SelectedValue);
                if (!loja.Sucesso)
                {
                    MessageBox.Show(loja.Mensagem);
                }
                var resultado = sistema.InserePontoDeVenda(txtNome.Text, (Loja)loja.Objeto);
                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja apagar o ponto de venda seleccionado?", "Apagar Ponto de Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {

                LogicaSistema sistema = new LogicaSistema(contexto);
                var resultado = sistema.ApagaPontoDeVenda(Id.Value);

                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
                Close();
            }
        }
    }
}
