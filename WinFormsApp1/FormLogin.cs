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
    public partial class FormLogin : Form
    {
        public FormLogin()
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
                    pnlLoginUtilizador.Visible = true;
                    pnlLoginUtilizador.Location = new Point(266,163);
                }
                else
                {
                    // sistema novo, permite criar um utilizador gerente...
                    pnlRegistoUtilizador.Visible = true;
                    pnlLoginUtilizador.Visible = false;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);

                var resultado = sistema.Login(txtEmailLogin.Text, txtSenhaLogin.Text);

                if (!resultado.Sucesso)
                {
                    MessageBox.Show(this,resultado.Mensagem,"");

                    VerificaSistema();
                }
                else
                {
                    txtEmailLogin.Text = "";
                    txtSenhaLogin.Text = "";

                    if (((Utilizador)resultado.Objeto).Tipo.TipoId == (int)TipoUtilizadorEnum.Gerente)
                    {
                        //Form da gestão
                        var m = new FormGestao
                        {
                            Tag = this
                        };
                        m.Show();
                        this.Hide();
                    }
                    else if (((Utilizador)resultado.Objeto).Tipo.TipoId == (int)TipoUtilizadorEnum.Empregado)
                    {
                        //Form da venda
                        MessageBox.Show("Form da venda");
                    }
                    else 
                    {
                        VerificaSistema();
                    }

                }

            }
        }
    }
}
