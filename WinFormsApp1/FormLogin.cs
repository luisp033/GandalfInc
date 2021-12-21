using Newtonsoft.Json;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormLogin : Form
    {

        private readonly ProjetoDBContext contexto;

        public FormLogin(ProjetoDBContext context)
        {
            InitializeComponent();

            contexto = context;

            VerificaSistema();

        }

        private void VerificaSistema()
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

        private void btnRegistar_Click(object sender, EventArgs e)
        {
                LogicaSistema sistema = new LogicaSistema(contexto);

                var resultado = sistema.InsereUtilizador(txtNome.Text, txtEmail.Text, txtSenha.Text, TipoUtilizadorEnum.Gerente);

                MessageBox.Show(resultado.Mensagem);

                if (resultado.Sucesso)
                { 
                    VerificaSistema();
                }
        }

        private void btnLogin_Click(object sender, EventArgs e)
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

                    var utilizador = (Utilizador)resultado.Objeto;

                    if (utilizador.Tipo.TipoId == (int)TipoUtilizadorEnum.Gerente)
                    {
                        //Form da gestão
                        var m = new FormGestao(contexto);
                        m.Tag = this;
                        m.Top = this.Top;
                        m.Left = this.Left;
                        m.Show();
                        this.Hide();
                    }
                    else if (utilizador.Tipo.TipoId == (int)TipoUtilizadorEnum.Empregado)
                    {

                        using (FormSelecionaPontoVenda frm = new FormSelecionaPontoVenda(contexto)) 
                        { 
                            DialogResult dr = frm.ShowDialog(this);

                            var x = frm.LojaId;

                            if (dr == DialogResult.Cancel)
                            {
                                VerificaSistema();
                            }
                            else if (dr == DialogResult.OK)
                            {
                                var pontoVendaResult = sistema.ObtemPontoDeVenda(frm.PontoVendaId);
                                if (!pontoVendaResult.Sucesso)
                                {
                                    MessageBox.Show(pontoVendaResult.Mensagem);
                                    VerificaSistema();
                                    return;
                                }
                                var aberturaResult = sistema.AberturaSessao(utilizador, (PontoDeVenda)pontoVendaResult.Objeto);
                                if (!aberturaResult.Sucesso)
                                {
                                    MessageBox.Show(aberturaResult.Mensagem);
                                    VerificaSistema();
                                    return;
                                }

                                //Form da venda
                                var m = new FormPontoVenda(contexto);
                                m.Tag = this;
                                m.Top = this.Top;
                                m.Left = this.Left;
                                m.Sessao = (PontoDeVendaSessao)aberturaResult.Objeto;
                                m.Utilizador = utilizador;

                                m.Show();
                                this.Hide();
                            }                       
                        }
                            //Exemplo de chamada a WebAPI
                            //HttpClient client = new HttpClient();
                            //var webResult = client.GetAsync("https://localhost:44314/api/Loja").Result;
                            //var conteudo = webResult.Content.ReadAsStringAsync();
                            //var x = JsonConvert.DeserializeObject<List<Loja>>(conteudo.Result);

                    }
                    else 
                    {
                        VerificaSistema();
                    }
                }
            
        }
    }
}
