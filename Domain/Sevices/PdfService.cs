using DesafioCCAA.Domain.Entity;
using DesafioCCAA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DesafioCCAA.Domain.Sevices
{
    public class PdfService : IPdfService
    {
        public PdfService() { }

        public async Task<byte[]> GerarRelatorioLivros(IEnumerable<Livro> livros)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Relatório de Livros Cadastrados").FontSize(18).Bold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Título
                            columns.RelativeColumn(1); // ISBN
                            columns.RelativeColumn(2); // Autor
                            columns.RelativeColumn(2); // Editora
                        });

                        // Cabeçalho
                        table.Header(header =>
                        {
                            header.Cell().Text("Título").Bold();
                            header.Cell().Text("ISBN").Bold();
                            header.Cell().Text("Autor").Bold();
                            header.Cell().Text("Editora").Bold();
                        });

                        // Conteúdo
                        foreach (var livro in livros)
                        {
                            table.Cell().Text(livro.Titulo);
                            table.Cell().Text(livro.ISBN);
                            table.Cell().Text(livro.Autor);
                            table.Cell().Text(livro.Editora?.Nome ?? "N/A");
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Gerado em: ");
                        x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold();
                    });
                });
            });

            return documento.GeneratePdf();
        }
    }
}
