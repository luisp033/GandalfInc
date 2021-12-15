
namespace WinFormsApp1
{
    partial class FormLogin
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
            this.pnlLoginUtilizador = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSenhaLogin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmailLogin = new System.Windows.Forms.TextBox();
            this.pnlRegistoUtilizador.SuspendLayout();
            this.pnlLoginUtilizador.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRegistoUtilizador
            // 
            this.pnlRegistoUtilizador.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // pnlLoginUtilizador
            // 
            this.pnlLoginUtilizador.BackColor = System.Drawing.Color.LightCyan;
            this.pnlLoginUtilizador.Controls.Add(this.label7);
            this.pnlLoginUtilizador.Controls.Add(this.btnLogin);
            this.pnlLoginUtilizador.Controls.Add(this.label5);
            this.pnlLoginUtilizador.Controls.Add(this.txtSenhaLogin);
            this.pnlLoginUtilizador.Controls.Add(this.label6);
            this.pnlLoginUtilizador.Controls.Add(this.txtEmailLogin);
            this.pnlLoginUtilizador.Location = new System.Drawing.Point(365, 58);
            this.pnlLoginUtilizador.Name = "pnlLoginUtilizador";
            this.pnlLoginUtilizador.Size = new System.Drawing.Size(467, 269);
            this.pnlLoginUtilizador.TabIndex = 2;
            this.pnlLoginUtilizador.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(31, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 30);
            this.label7.TabIndex = 42;
            this.label7.Text = "Acesso";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogin.Location = new System.Drawing.Point(332, 209);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(117, 31);
            this.btnLogin.TabIndex = 41;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "Senha";
            // 
            // txtSenhaLogin
            // 
            this.txtSenhaLogin.Location = new System.Drawing.Point(31, 134);
            this.txtSenhaLogin.Name = "txtSenhaLogin";
            this.txtSenhaLogin.PasswordChar = '*';
            this.txtSenhaLogin.Size = new System.Drawing.Size(418, 23);
            this.txtSenhaLogin.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "Email";
            // 
            // txtEmailLogin
            // 
            this.txtEmailLogin.Location = new System.Drawing.Point(31, 87);
            this.txtEmailLogin.Name = "txtEmailLogin";
            this.txtEmailLogin.Size = new System.Drawing.Size(418, 23);
            this.txtEmailLogin.TabIndex = 33;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 600);
            this.Controls.Add(this.pnlLoginUtilizador);
            this.Controls.Add(this.pnlRegistoUtilizador);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Gandalf Inc- Ponto de Venda";
            this.pnlRegistoUtilizador.ResumeLayout(false);
            this.pnlRegistoUtilizador.PerformLayout();
            this.pnlLoginUtilizador.ResumeLayout(false);
            this.pnlLoginUtilizador.PerformLayout();
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
        private System.Windows.Forms.Panel pnlLoginUtilizador;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSenhaLogin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmailLogin;
    }
}

