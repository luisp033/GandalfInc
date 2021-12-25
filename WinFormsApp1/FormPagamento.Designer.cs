namespace WinFormsApp1
{
    partial class FormPagamento
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroContribuinte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.btnMbWay = new System.Windows.Forms.Button();
            this.btnMultibanco = new System.Windows.Forms.Button();
            this.btnDinheiro = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTelefone);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNumeroContribuinte);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(33, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados Cliente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de Contribuinte";
            // 
            // txtNumeroContribuinte
            // 
            this.txtNumeroContribuinte.Location = new System.Drawing.Point(18, 45);
            this.txtNumeroContribuinte.Name = "txtNumeroContribuinte";
            this.txtNumeroContribuinte.Size = new System.Drawing.Size(166, 23);
            this.txtNumeroContribuinte.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nome do Cliente";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(18, 112);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(367, 23);
            this.txtNome.TabIndex = 3;
            // 
            // btnMbWay
            // 
            this.btnMbWay.Location = new System.Drawing.Point(33, 315);
            this.btnMbWay.Name = "btnMbWay";
            this.btnMbWay.Size = new System.Drawing.Size(146, 70);
            this.btnMbWay.TabIndex = 1;
            this.btnMbWay.Text = "MB Way";
            this.btnMbWay.UseVisualStyleBackColor = true;
            this.btnMbWay.Click += new System.EventHandler(this.btnMbWay_Click);
            // 
            // btnMultibanco
            // 
            this.btnMultibanco.Location = new System.Drawing.Point(196, 315);
            this.btnMultibanco.Name = "btnMultibanco";
            this.btnMultibanco.Size = new System.Drawing.Size(146, 70);
            this.btnMultibanco.TabIndex = 2;
            this.btnMultibanco.Text = "Multibanco";
            this.btnMultibanco.UseVisualStyleBackColor = true;
            this.btnMultibanco.Click += new System.EventHandler(this.btnMultibanco_Click);
            // 
            // btnDinheiro
            // 
            this.btnDinheiro.Location = new System.Drawing.Point(359, 315);
            this.btnDinheiro.Name = "btnDinheiro";
            this.btnDinheiro.Size = new System.Drawing.Size(146, 70);
            this.btnDinheiro.TabIndex = 3;
            this.btnDinheiro.Text = "Dinheiro";
            this.btnDinheiro.UseVisualStyleBackColor = true;
            this.btnDinheiro.Click += new System.EventHandler(this.btnDinheiro_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(396, 241);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 39);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(18, 179);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(166, 23);
            this.txtTelefone.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Telefone";
            // 
            // FormPagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 409);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnDinheiro);
            this.Controls.Add(this.btnMultibanco);
            this.Controls.Add(this.btnMbWay);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormPagamento";
            this.Text = "Pagamento";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumeroContribuinte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMbWay;
        private System.Windows.Forms.Button btnMultibanco;
        private System.Windows.Forms.Button btnDinheiro;
        private System.Windows.Forms.Button btnCancelar;
    }
}