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
    public partial class FormGestaoCategorias : Form
    {
        private readonly ProjetoDBContext contexto;
        public FormGestaoCategorias(ProjetoDBContext context)
        {
            InitializeComponent();
            contexto = context;
            DataGridLoad();
        }

        private void DataGridLoad()
        {

                LogicaSistema sistema = new LogicaSistema(contexto);
                this.dgv.DataSource = sistema.GetAllCategorias();

                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                editButtonColumn.Name = "edit_column";
                editButtonColumn.Text = "Editar";
                editButtonColumn.HeaderText = "Action";
                editButtonColumn.UseColumnTextForButtonValue = true;
                int columnIndex = 0;
                if (dgv.Columns["edit_column"] == null)
                {
                    dgv.Columns.Insert(columnIndex, editButtonColumn);
                }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (FormGestaoCategoriasEdit frm = new FormGestaoCategoriasEdit(contexto))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
            DataGridLoad();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                Guid? identificador = (Guid)dgv.Rows[e.RowIndex].Cells[3].Value;
                using (FormGestaoCategoriasEdit frm = new FormGestaoCategoriasEdit(contexto,identificador))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
                DataGridLoad();

            }
        }

    }
}
