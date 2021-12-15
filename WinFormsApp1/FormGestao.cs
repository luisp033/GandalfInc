﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormGestao : Form
    {

        public FormGestao()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            var formLogin = (FormLogin)Tag;

            formLogin.Top = this.Top;
            formLogin.Left = this.Left;
            formLogin.Show();
            Close();
        }

        private void btnLojas_Click(object sender, EventArgs e)
        {
            using (FormGestaoLojas frm = new FormGestaoLojas()) 
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this); 
            }
        }


        private void btnUtilizadores_Click(object sender, EventArgs e)
        {
            using (FormGestaoUtilizadores frm = new FormGestaoUtilizadores())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnPontoVendas_Click(object sender, EventArgs e)
        {
            using (FormGestaoPontoDeVendas frm = new FormGestaoPontoDeVendas())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            using (FormGestaoCategorias frm = new FormGestaoCategorias())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            using (FormGestaoMarcas frm = new FormGestaoMarcas())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            using (FormGestaoProdutos frm = new FormGestaoProdutos())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            using (FormGestaoStocks frm = new FormGestaoStocks())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }
    }
}