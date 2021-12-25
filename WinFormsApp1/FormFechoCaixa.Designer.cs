namespace WinFormsApp1
{
    partial class FormFechoCaixa
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
            this.btnFecho = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFecho
            // 
            this.btnFecho.BackColor = System.Drawing.Color.Red;
            this.btnFecho.ForeColor = System.Drawing.Color.White;
            this.btnFecho.Location = new System.Drawing.Point(253, 388);
            this.btnFecho.Name = "btnFecho";
            this.btnFecho.Size = new System.Drawing.Size(152, 50);
            this.btnFecho.TabIndex = 0;
            this.btnFecho.Text = "Fecho";
            this.btnFecho.UseVisualStyleBackColor = false;
            this.btnFecho.Click += new System.EventHandler(this.btnFecho_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(23, 18);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 25;
            this.dgv.Size = new System.Drawing.Size(554, 274);
            this.dgv.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(425, 388);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(152, 50);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormFechoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 450);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnFecho);
            this.Name = "FormFechoCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormFechoCaixa";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFecho;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnCancelar;
    }
}