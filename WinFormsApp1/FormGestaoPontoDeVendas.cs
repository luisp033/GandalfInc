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
    public partial class FormGestaoPontoDeVendas : Form
    {
        private readonly ProjetoDBContext contexto;
        public FormGestaoPontoDeVendas(ProjetoDBContext context)
        {
            InitializeComponent();
            contexto = context;
            DataGridLoad();

        }

        private void DataGridLoad()
        {

                LogicaSistema sistema = new LogicaSistema(contexto);
                this.dgvPontoVenda.DataSource = sistema.GetAllPontoDeVendas();

                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                editButtonColumn.Name = "edit_column";
                editButtonColumn.Text = "Editar";
                editButtonColumn.HeaderText = "Action";
                editButtonColumn.UseColumnTextForButtonValue = true;
                int columnIndex = 0;
                if (dgvPontoVenda.Columns["edit_column"] == null)
                {
                    dgvPontoVenda.Columns.Insert(columnIndex, editButtonColumn);
                }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (FormGestaoPontoDeVendasEdit frm = new FormGestaoPontoDeVendasEdit(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
            DataGridLoad();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvpontoDeVenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                Guid? identificador = (Guid)dgvPontoVenda.Rows[e.RowIndex].Cells[3].Value;
                using (FormGestaoPontoDeVendasEdit frm = new FormGestaoPontoDeVendasEdit(contexto, identificador))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
                DataGridLoad();

            }
        }
    }
}
