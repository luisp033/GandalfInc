
namespace WinFormsApp1
{
    partial class FormGestaoPontoDeVendas
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
            this.dgvPontoVenda = new System.Windows.Forms.DataGridView();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPontoVenda)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(12, 10);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(205, 23);
            this.btnInsert.TabIndex = 10;
            this.btnInsert.Text = "Adicionar novo ponto de venda";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // dgvPontoVenda
            // 
            this.dgvPontoVenda.AllowUserToAddRows = false;
            this.dgvPontoVenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPontoVenda.Location = new System.Drawing.Point(12, 54);
            this.dgvPontoVenda.Name = "dgvPontoVenda";
            this.dgvPontoVenda.RowTemplate.Height = 25;
            this.dgvPontoVenda.RowTemplate.ReadOnly = true;
            this.dgvPontoVenda.Size = new System.Drawing.Size(776, 313);
            this.dgvPontoVenda.TabIndex = 9;
            this.dgvPontoVenda.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpontoDeVenda_CellClick);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(652, 394);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(126, 46);
            this.btnSair.TabIndex = 8;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FormGestaoPontoDeVendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.dgvPontoVenda);
            this.Controls.Add(this.btnSair);
            this.Name = "FormGestaoPontoDeVendas";
            this.Text = "Gestao ponto de vendas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPontoVenda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.DataGridView dgvPontoVenda;
        private System.Windows.Forms.Button btnSair;
    }
}