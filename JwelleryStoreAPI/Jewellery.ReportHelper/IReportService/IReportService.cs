using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.ReportHelper.IReportServices
{
    public interface IReportService
    {
        byte[] GeneratePdfReport(string content);

        byte[] PrintToPaper(string htmlString);
    }
}
