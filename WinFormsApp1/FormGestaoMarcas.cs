using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestaoMarcas : Form
    {
        private readonly ProjetoDBContext contexto;

        public FormGestaoMarcas(ProjetoDBContext context)
        {
            InitializeComponent();
            contexto = context;
            DataGridLoad();
        }
        private void DataGridLoad()
        {

                LogicaSistema sistema = new LogicaSistema(contexto);
                this.dgv.DataSource = sistema.GetAllMarcas();

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
            using (FormGestaoMarcasEdit frm = new FormGestaoMarcasEdit(contexto))
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
                using (FormGestaoMarcasEdit frm = new FormGestaoMarcasEdit(contexto,identificador))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
                DataGridLoad();

            }
        }
    }
}
