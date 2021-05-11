using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryStoreAPI.ViewModel
{
    public class PdfContentViewModel
    {
        public decimal? Price { get; set; }
        public float? Weight { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsPriviledged { get; set; }
        public float? discount { get; set; }
    }
}
