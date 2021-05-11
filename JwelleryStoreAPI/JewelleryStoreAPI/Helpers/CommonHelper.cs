using JewelleryStoreAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStoreAPI.Helpers
{
    public static class CommonHelper
    {
        public static string GetHTMLString(PdfContentViewModel content)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Jewellery Store Summary</h1></div>
                                <table class='storeTable table table-responsive'>
                                    <tbody>
                                      <tr>
                                        <td class='ItemName'>
                                          <div>Gold Price(per gram)</div>
                                        </td>
                                        <td class='ItemValue'>");
                                            sb.AppendFormat(@"
                                            <div>{0}</div>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td class= 'ItemName'>
                                           <div>Weight(grams)</div>
                                         </td>
 
                                         <td class= 'ItemValue'>
                                          <div>{1}</div>
                                        </td>
                                      </tr>", content.Price.Value.ToString("C0", new CultureInfo("en-IN")), content.Weight);

                                    if (content.IsPriviledged == true)
                                    {
                                        sb.AppendFormat(@"
                                          <tr>
                                            <td class= 'ItemName'>
                                               <div>Discount</div>
                                             </td>
                                             <td class='ItemValue'>
                                              <div>{0} %</div>
                                            </td>
                                          </tr>", content.discount);

                                    }
                                    sb.AppendFormat(@"
                                          <tr>
                                            <td class= 'ItemName'>
                                               <div>Total Price</div>
                                             </td>
                                            <td class= 'ItemValue'>
                                             <div>{0}</div>
                                            </td>
                                          </tr>
                                 </tbody>
                              </table>
                           </body>
                        </html>"
                            , content.TotalPrice.Value.ToString("C0", new CultureInfo("en-IN")));

            return sb.ToString();
        }
    }
}
