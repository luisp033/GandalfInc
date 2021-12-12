
namespace WinFormsApp1
{
    partial class FormGestaoLojas
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
            this.button1 = new System.Windows.Forms.Button();
            this.dgvLojas = new System.Windows.Forms.DataGridView();
            this.btnInsert = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLojas)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(652, 396);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sair";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvLojas
            // 
            this.dgvLojas.AllowUserToAddRows = false;
            this.dgvLojas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLojas.Location = new System.Drawing.Point(12, 56);
            this.dgvLojas.Name = "dgvLojas";
            this.dgvLojas.RowTemplate.Height = 25;
            this.dgvLojas.RowTemplate.ReadOnly = true;
            this.dgvLojas.Size = new System.Drawing.Size(776, 313);
            this.dgvLojas.TabIndex = 1;
            this.dgvLojas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLojas_CellClick);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(12, 12);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(205, 23);
            this.btnInsert.TabIndex = 4;
            this.btnInsert.Text = "Adicionar nova loja";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // FormGestaoLojas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.dgvLojas);
            this.Controls.Add(this.button1);
            this.Name = "FormGestaoLojas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Gestao Lojas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLojas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvLojas;
        private System.Windows.Forms.Button btnInsert;
    }
}