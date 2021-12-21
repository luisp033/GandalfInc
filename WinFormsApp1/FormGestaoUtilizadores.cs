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
    public partial class FormGestaoUtilizadores : Form
    {
        private readonly ProjetoDBContext contexto;
        public FormGestaoUtilizadores(ProjetoDBContext context)
        {
            InitializeComponent();
            contexto = context;
            DataGridLoad();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataGridLoad()
        {

                LogicaSistema sistema = new LogicaSistema(contexto);
                this.dgvUtilizadores.DataSource = sistema.GetAllUtilizadores();

                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                editButtonColumn.Name = "edit_column";
                editButtonColumn.Text = "Editar";
                editButtonColumn.HeaderText = "Action";
                editButtonColumn.UseColumnTextForButtonValue = true;
                int columnIndex = 0;
                if (dgvUtilizadores.Columns["edit_column"] == null)
                {
                    dgvUtilizadores.Columns.Insert(columnIndex, editButtonColumn);
                }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (FormGestaoUtilizadoresEdit frm = new FormGestaoUtilizadoresEdit(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
            DataGridLoad();
        }


        private void dgvUtilizadores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                Guid? identificador = (Guid)dgvUtilizadores.Rows[e.RowIndex].Cells[5].Value;
                using (FormGestaoUtilizadoresEdit frm = new FormGestaoUtilizadoresEdit(contexto, identificador))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
                DataGridLoad();

            }
        }

    }
}
