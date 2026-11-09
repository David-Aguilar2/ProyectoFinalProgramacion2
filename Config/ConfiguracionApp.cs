using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace VentasOnline.Config
{
    public class ConfiguracionApp
    {
        // Nombre del archivo de configuración que contiene la cadena de conexión
        private const string Archivo = "appsettings.json";

        // Método que obtiene la cadena de conexión desde el archivo de configuración
        public static string ObtenerCadenaConexion(
            string nombre = "TiendaOnlineDb")// Nombre de la cadena de conexión en el archivo appsettings.json
        {
            string rutaExe = Path.Combine(AppContext.BaseDirectory, Archivo);// Ruta completa del archivo de configuración
            string rutaBase = File.Exists(rutaExe)// Si el archivo existe en la ruta de ejecución, usar esa ruta
                ? AppContext.BaseDirectory
                : Directory.GetCurrentDirectory();// Si no, usar la ruta del directorio actual

            // Construir la configuración a partir del archivo appsettings.json
            IConfigurationRoot configuracion = new ConfigurationBuilder()
                .SetBasePath(rutaBase)
                .AddJsonFile(Archivo, optional: false, reloadOnChange: false)
                .Build();

            // Obtener la cadena de conexión por nombre y lanzar una excepción si no se encuentra
            return configuracion.GetConnectionString(nombre)
                ?? throw new InvalidOperationException(
                    $"Falta la cadena '{nombre}' en {Archivo}.");

        }
    }
}
