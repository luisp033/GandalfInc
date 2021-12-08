
namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlRegistoUtilizador = new System.Windows.Forms.Panel();
            this.btnRegistar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRegistoUtilizador.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRegistoUtilizador
            // 
            this.pnlRegistoUtilizador.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlRegistoUtilizador.Controls.Add(this.btnRegistar);
            this.pnlRegistoUtilizador.Controls.Add(this.label4);
            this.pnlRegistoUtilizador.Controls.Add(this.txtSenha);
            this.pnlRegistoUtilizador.Controls.Add(this.label3);
            this.pnlRegistoUtilizador.Controls.Add(this.txtEmail);
            this.pnlRegistoUtilizador.Controls.Add(this.label2);
            this.pnlRegistoUtilizador.Controls.Add(this.txtNome);
            this.pnlRegistoUtilizador.Controls.Add(this.label1);
            this.pnlRegistoUtilizador.Location = new System.Drawing.Point(266, 163);
            this.pnlRegistoUtilizador.Name = "pnlRegistoUtilizador";
            this.pnlRegistoUtilizador.Size = new System.Drawing.Size(467, 269);
            this.pnlRegistoUtilizador.TabIndex = 1;
            this.pnlRegistoUtilizador.Visible = false;
            // 
            // btnRegistar
            // 
            this.btnRegistar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRegistar.Location = new System.Drawing.Point(323, 216);
            this.btnRegistar.Name = "btnRegistar";
            this.btnRegistar.Size = new System.Drawing.Size(117, 31);
            this.btnRegistar.TabIndex = 40;
            this.btnRegistar.Text = "Registar";
            this.btnRegistar.UseVisualStyleBackColor = false;
            this.btnRegistar.Click += new System.EventHandler(this.btnRegistar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(22, 170);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(418, 23);
            this.txtSenha.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(22, 123);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(418, 23);
            this.txtEmail.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(22, 78);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(418, 23);
            this.txtNome.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "O sistema não tem nenhum utilizador Gerente, é necessário registar um utilizador";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 600);
            this.Controls.Add(this.pnlRegistoUtilizador);
            this.Name = "Form1";
            this.Text = "Gandalf Inc- Ponto de Venda";
            this.pnlRegistoUtilizador.ResumeLayout(false);
            this.pnlRegistoUtilizador.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegistoUtilizador;
        private System.Windows.Forms.Button btnRegistar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
    }
}

