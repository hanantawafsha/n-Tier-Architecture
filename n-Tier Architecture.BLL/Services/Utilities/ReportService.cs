using NTierArchitecture.BLL.Services.Interfaces;
using NTierArchitecture.DAL.Repositories.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NTierArchitecture.BLL.Services.Utilities
{
    public class ReportService
    {
        private readonly IProductRepository _productRepository;

        public ReportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            QuestPDF.Settings.License = LicenseType.Community;

        }


        public async Task<IDocument> CreateDocumentAsync()
        {
            var products = await _productRepository.GelAllProductsWithImageAsync();



            return Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header()
                            .Text("Hello PDF!")
                            .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);

                                foreach (var item in products)
                                {
                                    x.Item().Text($"Product ID: {item.Id} - Product Name: {item.Name}");

                                }
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("Page ");
                                x.CurrentPageNumber();
                            });
                    });
                });

        }
    }
}