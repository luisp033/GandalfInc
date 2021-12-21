
namespace WinFormsApp1
{
    partial class FormSelecionaPontoVenda
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
            this.btnEntrar = new System.Windows.Forms.Button();
            this.cmbLoja = new System.Windows.Forms.ComboBox();
            this.cmbPontoVenda = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEntrar
            // 
            this.btnEntrar.Location = new System.Drawing.Point(150, 225);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(160, 38);
            this.btnEntrar.TabIndex = 0;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = true;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // cmbLoja
            // 
            this.cmbLoja.FormattingEnabled = true;
            this.cmbLoja.Location = new System.Drawing.Point(76, 52);
            this.cmbLoja.Name = "cmbLoja";
            this.cmbLoja.Size = new System.Drawing.Size(323, 23);
            this.cmbLoja.TabIndex = 1;
            this.cmbLoja.SelectedIndexChanged += new System.EventHandler(this.cmbLoja_SelectedIndexChanged);
            // 
            // cmbPontoVenda
            // 
            this.cmbPontoVenda.FormattingEnabled = true;
            this.cmbPontoVenda.Location = new System.Drawing.Point(76, 141);
            this.cmbPontoVenda.Name = "cmbPontoVenda";
            this.cmbPontoVenda.Size = new System.Drawing.Size(323, 23);
            this.cmbPontoVenda.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Loja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ponto de Venda";
            // 
            // FormSelecionaPontoVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 293);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPontoVenda);
            this.Controls.Add(this.cmbLoja);
            this.Controls.Add(this.btnEntrar);
            this.Name = "FormSelecionaPontoVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleção Ponto Venda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSelecionaPontoVenda_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.ComboBox cmbLoja;
        private System.Windows.Forms.ComboBox cmbPontoVenda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}