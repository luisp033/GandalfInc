using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            VerificaSistema();

        }

        private void VerificaSistema()
        {
            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);

                if (sistema.VerificaSeExisteUmUtilizadorGerente())
                {
                    // mostra o pedido de login
                    pnlRegistoUtilizador.Visible = false;
                }
                else
                {
                    // sistema novo, permite criar um utilizador gerente...
                    pnlRegistoUtilizador.Visible = true;
                }

            }
        }

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);

                var resultado = sistema.InsereUtilizador(txtNome.Text, txtEmail.Text, txtSenha.Text, TipoUtilizadorEnum.Gerente);

                MessageBox.Show(resultado.Mensagem);

                if (resultado.Sucesso)
                { 
                    VerificaSistema();
                }
            }

        }
    }
}
