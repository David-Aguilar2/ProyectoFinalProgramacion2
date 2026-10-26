using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace VentasOnline.Config
{
    public class ConfiguracionApp
    {
        private const string Archivo = "appsettings.json";

        public static string ObtenerCadenaConexion(
            string nombre = "TiendaOnlineDb")
        {
            string rutaExe = Path.Combine(AppContext.BaseDirectory, Archivo);
            string rutaBase = File.Exists(rutaExe)
                ? AppContext.BaseDirectory
                : Directory.GetCurrentDirectory();

            IConfigurationRoot configuracion = new ConfigurationBuilder()
                .SetBasePath(rutaBase)
                .AddJsonFile(Archivo, optional: false, reloadOnChange: false)
                .Build();

            return configuracion.GetConnectionString(nombre)
                ?? throw new InvalidOperationException(
                    $"Falta la cadena '{nombre}' en {Archivo}.");

        }
    }
}
