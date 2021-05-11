﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.Domain.Models
{
    public class Jewel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? PricePerGram { get; set; }
    }
}
