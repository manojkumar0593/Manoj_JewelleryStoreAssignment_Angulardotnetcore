using DinkToPdf;
using DinkToPdf.Contracts;
using Jewellery.ReportHelper.IReportServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.ReportHelper.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IConverter _converter;
        public ReportService(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] GeneratePdfReport(string content)
        {

            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            globalSettings.DocumentTitle = "PDF Report Jwellery Store Summary";

            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = content;

            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            webSettings.UserStyleSheet = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "assets", "styles.css");

            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 14;
            headerSettings.FontName = "Arial";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;

            FooterSettings footerSettings = new FooterSettings();
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Arial";
            footerSettings.Center = "Copyright Protected";
            footerSettings.Line = true;

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;

            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            return _converter.Convert(htmlToPdfDocument);
        }
        public byte[] PrintToPaper(string htmlString)
        {
             throw new NotImplementedException();
        }
    }
}
