using System;
using VentasOnline.Data;
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
            Usuario usuarioAutenticado = null;

            ApplicationConfiguration.Initialize();
            try
            {
                Seguridad.Ejecutar(); //Completa contraseñas pendientes
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la base de datos.\n\n" +
                    ex.Message, "SivarPOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return; //si la persona agota los intentos
            }
            Application.Run(new FrmMenu(usuarioAutenticado)); //accedera solo cuando la sesion sea valida.
        }
    }
}