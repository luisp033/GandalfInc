
namespace WinFormsApp1
{
    partial class FormGestaoUtilizadores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInsert = new System.Windows.Forms.Button();
            this.dgvUtilizadores = new System.Windows.Forms.DataGridView();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilizadores)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(12, 10);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(205, 23);
            this.btnInsert.TabIndex = 7;
            this.btnInsert.Text = "Adicionar nova utilizador";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // dgvUtilizadores
            // 
            this.dgvUtilizadores.AllowUserToAddRows = false;
            this.dgvUtilizadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUtilizadores.Location = new System.Drawing.Point(12, 54);
            this.dgvUtilizadores.Name = "dgvUtilizadores";
            this.dgvUtilizadores.RowTemplate.Height = 25;
            this.dgvUtilizadores.RowTemplate.ReadOnly = true;
            this.dgvUtilizadores.Size = new System.Drawing.Size(776, 313);
            this.dgvUtilizadores.TabIndex = 6;
            this.dgvUtilizadores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUtilizadores_CellClick);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(652, 394);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(126, 46);
            this.btnSair.TabIndex = 5;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FormGestaoUtilizadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.dgvUtilizadores);
            this.Controls.Add(this.btnSair);
            this.Name = "FormGestaoUtilizadores";
            this.Text = "Gestao Utilizadores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilizadores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.DataGridView dgvUtilizadores;
        private System.Windows.Forms.Button btnSair;
    }
}