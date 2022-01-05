using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestaoMarcasEdit : Form
    {
        public Guid? Id { get; set; }
        private bool editMode = false;
        private readonly ProjetoDBContext contexto;

        public FormGestaoMarcasEdit(ProjetoDBContext context, Guid? id = null)
        {
            InitializeComponent();
            contexto = context;
            Id = id;
            editMode = (Id != null);

            LoadForm();
        }

        private void LoadForm()
        {
            if (!editMode)
            {
                btnDelete.Visible = false;
                return;
            }

            LogicaSistema sistema = new LogicaSistema(contexto);
            var resultado = sistema.ObtemMarca(Id.Value);

            if (!resultado.Sucesso)
            {
                MessageBox.Show(resultado.Mensagem);
                Close();
            }

            var marca = ((MarcaProduto)resultado.Objeto);
            txtNome.Text = marca.Nome;
            txtOrigem.Text = marca.Origem;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {

            LogicaSistema sistema = new LogicaSistema(contexto);

            if (editMode)
            {
                var resultado = sistema.AlteraMarca(Id.Value, txtNome.Text, txtOrigem.Text);
                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            else
            {

                var resultado = sistema.InsereMarca(txtNome.Text, txtOrigem.Text);
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
            DialogResult dr = MessageBox.Show("Deseja apagar a marca seleccionada?", "Apagar Marca", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                LogicaSistema sistema = new LogicaSistema(contexto);
                var resultado = sistema.ApagaMarca(Id.Value);

                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                }
            }
            Close();
        }
    }
}
