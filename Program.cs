using System;
using QuestPDF.Infrastructure;
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
            //Establecer la variable de usuario autenticado para que pueda ser utilizada en toda la aplicación
            Usuario usuarioAutenticado = null;

            ApplicationConfiguration.Initialize();//configura la aplicacion para que se vea bien en pantallas de alta resolucion
            QuestPDF.Settings.License = LicenseType.Community;//Licencia gratuita para usar QuestPDF

            // Habilita el uso de fuentes instaladas en el sistema operativo (Windows)
            // Esto permite usar tipografías como 'Calibri', 'Arial', etc., sin lanzar excepciones.
            QuestPDF.Settings.UseSystemFonts = true;
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
            //Se muestra el formulario de login y se valida la sesión
            using (var login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return; //si la persona agota los intentos
            }
            Application.Run(new FrmMenu(usuarioAutenticado)); //accedera solo cuando la sesion sea valida.
        }
    }
}