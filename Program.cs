using System;
using VentasOnline.Forms;
using VentasOnline.Models;

namespace VentasOnline
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Usuario usuarioAutenticado = null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (FrmLogin formLogin = new FrmLogin())
            {
                if (formLogin.ShowDialog() == DialogResult.OK)
                {
                    usuarioAutenticado = formLogin.UsuarioAutenticado;
                }
            }

            if (usuarioAutenticado != null)
            {
                // Forzamos la ejecución del Menú
                Application.Run(new Form1(usuarioAutenticado));
            }
        }
    }
}