using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestaoUtilizadoresEdit : Form
    {
        private readonly ProjetoDBContext contexto;
        public Guid? Id { get; set; }
        private bool editMode = false;

        public FormGestaoUtilizadoresEdit(ProjetoDBContext context, Guid? id = null)
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
            this.cmbTipoUtilizador.DataSource = Enum.GetValues(typeof(TipoUtilizadorEnum));
        }

        private void LoadForm()
        {
            if (!editMode)
            {
                btnDelete.Visible = false;
                return;
            }


            LogicaSistema sistema = new LogicaSistema(contexto);
            var resultado = sistema.ObtemUtilizador(Id.Value);

            if (!resultado.Sucesso)
            {
                MessageBox.Show(resultado.Mensagem);
                Close();
            }

            var utilizador = ((Utilizador)resultado.Objeto);

            txtNome.Text = utilizador.Nome;
            txtEmail.Text = utilizador.Email;
            txtSenha.Text = utilizador.Senha;
            cmbTipoUtilizador.SelectedItem = Enum.Parse(typeof(TipoUtilizadorEnum), utilizador.Tipo.TipoId.ToString());

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
                var resultado = sistema.AlteraUtilizador(Id.Value, txtNome.Text, txtEmail.Text, txtSenha.Text, (TipoUtilizadorEnum)cmbTipoUtilizador.SelectedItem);
                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            else
            {
                var resultado = sistema.InsereUtilizador(txtNome.Text, txtEmail.Text, txtSenha.Text, (TipoUtilizadorEnum)cmbTipoUtilizador.SelectedItem);
                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja apagar o utilizador seleccionado?", "Apagar Utilizador", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                LogicaSistema sistema = new LogicaSistema(contexto);
                var resultado = sistema.ApagaUtilizador(Id.Value);

                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
                Close();
            }
        }
    }
}
