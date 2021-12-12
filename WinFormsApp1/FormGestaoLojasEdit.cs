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
    public partial class FormGestaoLojasEdit : Form
    {

        public Guid? Id { get; set; }
        private bool editMode = false;

        public FormGestaoLojasEdit(Guid? id = null)
        {
            InitializeComponent();
            Id = id;
            editMode = (Id != null);
            LoadForm();
        }

        private void LoadForm() 
        {
            if (!editMode) 
            {
                return;
            }

            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);
                var resultado = sistema.ObtemLoja(Id.Value);

                if (!resultado.Sucesso)
                {
                    MessageBox.Show(resultado.Mensagem);
                    Close();
                }

                txtNome.Text = ((Loja)resultado.Objeto).Nome;
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
                    var resultado = sistema.AlteraLoja(Id.Value, txtNome.Text, "123456789", "emailTeste", "999999999", null);
                    if (!resultado.Sucesso)
                    {
                        MessageBox.Show(resultado.Mensagem);
                    }
                }
                else
                {
                    var resultado = sistema.InsereLoja(txtNome.Text, "123456789", "emailTeste", "999999999", null);
                    if (!resultado.Sucesso)
                    {
                        MessageBox.Show(resultado.Mensagem);
                    }
                }

            }
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja apagar a loja seleccionada?", "Apagar Loja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                using (var contexto = new ProjetoDBContext())
                {
                    LogicaSistema sistema = new LogicaSistema(contexto);
                    var resultado = sistema.ApagaLoja(Id.Value);

                    if (!resultado.Sucesso)
                    {
                        MessageBox.Show(resultado.Mensagem);
                    }
                }
                Close();
            }

        }
    }
}
