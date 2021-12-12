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
    public partial class FormGestaoLojas : Form
    {
        public FormGestaoLojas()
        {
            InitializeComponent();

            FormGestaoLojas_Load();
        }

        private void FormGestaoLojas_Load()
        {
            using (var contexto = new ProjetoDBContext())
            {
                LogicaSistema sistema = new LogicaSistema(contexto);
                this.dgvLojas.DataSource = sistema.GetAllLojas();

                DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                editButtonColumn.Name = "edit_column";
                editButtonColumn.Text = "Editar";
                editButtonColumn.HeaderText = "Action";
                editButtonColumn.UseColumnTextForButtonValue = true;
                int columnIndex = 0;
                if (dgvLojas.Columns["edit_column"] == null)
                {
                    dgvLojas.Columns.Insert(columnIndex, editButtonColumn);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            using (FormGestaoLojasEdit frmGestaoLojasEdit = new FormGestaoLojasEdit())
            {
                frmGestaoLojasEdit.StartPosition = FormStartPosition.CenterParent;
                frmGestaoLojasEdit.ShowDialog(this);
            }
            FormGestaoLojas_Load();

        }

        private void dgvLojas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                Guid? identificador = (Guid)dgvLojas.Rows[e.RowIndex].Cells[7].Value;
                using (FormGestaoLojasEdit frmGestaoLojasEdit = new FormGestaoLojasEdit(identificador))
                {
                    frmGestaoLojasEdit.StartPosition = FormStartPosition.CenterParent;
                    frmGestaoLojasEdit.ShowDialog(this);
                }
                FormGestaoLojas_Load();

            }
        }

    }
}
