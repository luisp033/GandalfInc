using Projeto.DataAccessLayer;
using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    static class Program
    {

        static readonly ProjetoDBContext Contexto = new ProjetoDBContext();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin(Contexto));
        }
    }
}
