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
    public partial class FormSelecionaPontoVenda : Form
    {
        private bool afterFirstLoad = false;
        public Guid LojaId { get; set; }
        public Guid PontoVendaId { get; set; }

        private readonly ProjetoDBContext contexto;

        public FormSelecionaPontoVenda(ProjetoDBContext context)
        {
            InitializeComponent();
            contexto = context;
            LoadCombo();
        }

        private void SetComboPontoVenda()
        {

            LogicaSistema sistema = new LogicaSistema(contexto);
            Guid idLoja = (Guid)this.cmbLoja.SelectedValue;
            var pontoVendas = sistema.GetAllPontoDeVendasByLoja(idLoja);
            this.cmbPontoVenda.DataSource = pontoVendas;
            this.cmbPontoVenda.DisplayMember = "Nome";
            this.cmbPontoVenda.ValueMember = "Identificador";
            this.cmbPontoVenda.SelectedIndex = 0;
        }

        private void LoadCombo()
        {

            LogicaSistema sistema = new LogicaSistema(contexto);

            var lojas = sistema.GetAllLojasComPontosDeVenda();
            this.cmbLoja.DataSource = lojas;
            this.cmbLoja.DisplayMember = "Nome";
            this.cmbLoja.ValueMember = "Identificador";
            afterFirstLoad = true;
            SetComboPontoVenda();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            LojaId = (Guid)this.cmbLoja.SelectedValue;
            PontoVendaId = (Guid)this.cmbPontoVenda.SelectedValue;
            this.DialogResult = DialogResult.OK;
        }

        private void FormSelecionaPontoVenda_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.Cancel;
            }

        }

        private void cmbLoja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (afterFirstLoad)
            {
                SetComboPontoVenda();
            }
        }
    }
}
