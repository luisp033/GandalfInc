using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Enumerados;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormPagamento : Form
    {

        private readonly ProjetoDBContext contexto;

        private readonly Guid vendaId;

        public FormPagamento(ProjetoDBContext context, Guid vendaId)
        {
            InitializeComponent();
            contexto = context;
            this.vendaId = vendaId;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnMbWay_Click(object sender, EventArgs e)
        {
            Pagamento(TipoPagamentoEnum.MbWay);
        }

        private void btnMultibanco_Click(object sender, EventArgs e)
        {
            Pagamento(TipoPagamentoEnum.Multibanco);
        }

        private void btnDinheiro_Click(object sender, EventArgs e)
        {
            Pagamento(TipoPagamentoEnum.Dinheiro);
        }

        private void Pagamento(TipoPagamentoEnum tipo) 
        {
            LogicaSistema sistema = new LogicaSistema(contexto);

            var resultado = sistema.Pagamento(vendaId, txtNome.Text, txtNumeroContribuinte.Text, txtTelefone.Text, tipo);

            if (!resultado.Sucesso) 
            {
                MessageBox.Show(resultado.Mensagem);
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

    }
}
