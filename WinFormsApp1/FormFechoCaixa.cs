using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Auxiliar;
using Projeto.DataAccessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormFechoCaixa : Form
    {

        private readonly ProjetoDBContext contexto;
        public Guid PontoDeVendaSessaoId { get; set; }


        public FormFechoCaixa(ProjetoDBContext context, Guid pontoDeVendaSessaoId)
        {
            InitializeComponent();

            contexto = context;

            PontoDeVendaSessaoId = pontoDeVendaSessaoId;

            DataGridLoad();
        }

        private void btnFecho_Click(object sender, EventArgs e)
        {
            FechoCaixa();
        }

        private void DataGridLoad()
        {

            LogicaSistema sistema = new LogicaSistema(contexto);
            this.dgv.DataSource = sistema.GetTotaisSessao(PontoDeVendaSessaoId);
            AtualizaTotais();
        }


        private void AtualizaTotais()
        {
            //https://stackoverflow.com/questions/13056678/datagridview-column-footer-c-net-winforms        
            DataTable dt = DataTableUtils.ToDataTable((List<TotalSessao>)dgv.DataSource);
            if (dgv.Rows.Count == 0)
            {
                return;
            }

            int lastRow = (dgv.Rows.Count - 1);
            if (dt.Rows[lastRow][0].ToString() == "Total")
            {
                dt.Rows.RemoveAt(lastRow);
            }
            decimal totalSum = 0.0m;
            foreach (DataRow row in dt.Rows)
            {
                totalSum += Convert.ToDecimal(row.ItemArray[1]);
            }

            DataRow newRow = dt.NewRow();
            newRow[0] = "Total";
            newRow[1] = totalSum;
            dt.Rows.Add(newRow);
            dgv.DataSource = dt;
        }

        private void FechoCaixa()
        {
            this.DialogResult = DialogResult.OK;

            LogicaSistema sistema = new LogicaSistema(contexto);

            var resultado = sistema.FechaSessaoVenda(PontoDeVendaSessaoId);

            if (!resultado.Sucesso)
            {
                MessageBox.Show(resultado.Mensagem);
                this.DialogResult = DialogResult.No;
            }

            Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }

}
