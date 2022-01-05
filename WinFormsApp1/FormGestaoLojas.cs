using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestaoLojas : Form
    {

        private readonly ProjetoDBContext contexto;

        public FormGestaoLojas(ProjetoDBContext context)
        {
            InitializeComponent();

            contexto = context;

            FormGestaoLojas_Load();
        }

        private void FormGestaoLojas_Load()
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            using (FormGestaoLojasEdit frmGestaoLojasEdit = new FormGestaoLojasEdit(contexto))
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
                using (FormGestaoLojasEdit frmGestaoLojasEdit = new FormGestaoLojasEdit(contexto,identificador))
                {
                    frmGestaoLojasEdit.StartPosition = FormStartPosition.CenterParent;
                    frmGestaoLojasEdit.ShowDialog(this);
                }
                FormGestaoLojas_Load();

            }
        }

    }
}
