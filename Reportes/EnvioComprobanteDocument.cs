using System;
using VentasOnline.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

// Alias para resolver la ambigüedad entre System.ComponentModel y QuestPDF.Infrastructure
// Le indicamos explícitamente a C# que IContainer dentro de este archivo se refiere al IContainer de QuestPDF.
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace VentasOnline.Reportes
{
    public class EnvioComprobanteDocument : IDocument
    {
        private readonly EnvioDetalleReporteDto _modelo;
        private readonly string _usuarioGenerador;

        public EnvioComprobanteDocument(EnvioDetalleReporteDto modelo, string usuarioGenerador)
        {
            _modelo = modelo;
            _usuarioGenerador = usuarioGenerador;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        // Configuración general de la página del PDF (Tamaño carta, márgenes y tipografía base)
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.Letter);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Calibri));

                page.Header().Element(ComponerEncabezado);
                page.Content().Element(ComponerContenido);
                page.Footer().Element(ComponerPie);
            });
        }

        // Encabezado del comprobante: Muestra el nombre del sistema, título, IDs de envío/pedido y datos de generación
        private void ComponerEncabezado(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("SivarPOS & C807 Express").FontSize(18).Bold().FontColor("#1B3A5C");
                        c.Item().Text("Comprobante de Envío y Detalle de Pedido").FontSize(12).SemiBold();
                        c.Item().Text($"Envío N.°: {_modelo.IdEnvio} | Pedido N.°: {_modelo.IdPedido}");
                    });

                    row.ConstantItem(180).AlignRight().Column(c =>
                    {
                        c.Item().AlignRight().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}");
                        c.Item().AlignRight().Text($"Atendido por: {_usuarioGenerador}");
                    });
                });
                col.Item().PaddingTop(5).LineHorizontal(1.5f).LineColor("#2E6DA4");
            });
        }

        // Contenido principal del documento: Se divide en datos generales, tabla de productos y totales
        private void ComponerContenido(IContainer container)
        {
            container.PaddingVertical(10).Column(col =>
            {
                // SECCIÓN 1: DATOS GENERALES (Información del Cliente y Logística C807 Express)
                col.Item().Row(row =>
                {
                    // Bloque izquierdo: Datos del Cliente
                    row.RelativeItem().Border(0.5f).BorderColor("#D0D0D0").Padding(8).Column(c =>
                    {
                        c.Item().Text("Información del Cliente").Bold().FontColor("#1B3A5C");
                        c.Item().Text($"Nombre: {_modelo.ClienteNombre}");
                        c.Item().Text($"Teléfono: {_modelo.ClienteTelefono}");
                        c.Item().Text($"Correo: {_modelo.ClienteCorreo}");
                    });

                    row.ConstantItem(10); // Espacio entre columnas

                    // Bloque derecho: Datos del Envío y Dirección
                    row.RelativeItem().Border(0.5f).BorderColor("#D0D0D0").Padding(8).Column(c =>
                    {
                        c.Item().Text("Logística C807 Express").Bold().FontColor("#1B3A5C");
                        c.Item().Text($"Transporte: {_modelo.EmpresaTransporte}");
                        c.Item().Text($"Guía/Tracking: {_modelo.NumeroSeguimiento}");
                        c.Item().Text($"Destino: {_modelo.Departamento}\n{_modelo.UbicacionExacta}");
                    });
                });

                col.Item().PaddingTop(10);

                // SECCIÓN 2: TABLA DE PRODUCTOS ASOCIADOS AL PEDIDO
                col.Item().Table(table =>
                {
                    // Definición de anchos para las 4 columnas de la tabla
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3); // Nombre del Producto
                        columns.ConstantColumn(60); // Cantidad
                        columns.ConstantColumn(80); // Precio Unitario
                        columns.ConstantColumn(80); // SubTotal
                    });

                    // Cabecera de la tabla (Se repite automáticamente si el documento ocupa múltiples páginas)
                    table.Header(header =>
                    {
                        header.Cell().Element(CeldaTitulo).Text("Producto");
                        header.Cell().Element(CeldaTitulo).AlignRight().Text("Cant.");
                        header.Cell().Element(CeldaTitulo).AlignRight().Text("P. Unit ($)");
                        header.Cell().Element(CeldaTitulo).AlignRight().Text("SubTotal ($)");
                    });

                    // Recorrido de los productos del pedido para generar las filas de la tabla con colores alternados
                    for (int i = 0; i < _modelo.Productos.Count; i++)
                    {
                        var prod = _modelo.Productos[i];
                        string fondo = i % 2 == 0 ? "#FFFFFF" : "#EEF5FC";
                        IContainer Celda() => table.Cell().Element(c => CeldaDato(c, fondo));

                        Celda().Text(prod.NombreProducto);
                        Celda().AlignRight().Text(prod.Cantidad.ToString());
                        Celda().AlignRight().Text(prod.PrecioUnitario.ToString("N2"));
                        Celda().AlignRight().Text(prod.SubTotal.ToString("N2"));
                    }
                });

                // SECCIÓN 3: TOTALES FINANCIEROS (Subtotal de mercancía, costo de envío y total final contra entrega)
                col.Item().PaddingTop(10).AlignRight().Column(c =>
                {
                    c.Item().Text($"Subtotal Mercancía: ${_modelo.TotalPedido:N2}");
                    c.Item().Text($"Costo Envío (C807 Express): ${_modelo.CostoEnvio:N2}");
                    c.Item().PaddingTop(4).Text($"TOTAL A COBRAR (Contra Entrega): ${(_modelo.TotalPedido + _modelo.CostoEnvio):N2}")
                        .FontSize(12).Bold().FontColor("#1B3A5C");
                });
            });
        }

        // Pie de página institucional con numeración dinámica "Página X de Y"
        private void ComponerPie(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().LineHorizontal(0.5f).LineColor("#2E6DA4");
                col.Item().PaddingTop(3).Row(row =>
                {
                    row.RelativeItem().DefaultTextStyle(x => x.FontSize(8))
                        .Text("SivarPOS - Sistema Integrado Contra Entrega / C807 Express");
                    row.RelativeItem().AlignRight().DefaultTextStyle(x => x.FontSize(8))
                        .Text(t =>
                        {
                            t.Span("Página ");
                            t.CurrentPageNumber();
                            t.Span(" de ");
                            t.TotalPages();
                        });
                });
            });
        }

        // Estilos reutilizables para las celdas de la tabla (títulos y datos)
        private static IContainer CeldaTitulo(IContainer c) =>
            c.Background("#1B3A5C").Padding(5).DefaultTextStyle(x => x.FontColor("#FFFFFF").Bold());

        private static IContainer CeldaDato(IContainer c, string fondo) =>
            c.Background(fondo).BorderBottom(0.5f).BorderColor("#D0D0D0").Padding(4);
    }
}