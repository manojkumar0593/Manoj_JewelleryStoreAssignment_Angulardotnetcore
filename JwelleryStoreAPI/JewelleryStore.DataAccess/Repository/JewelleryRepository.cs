using JewelleryStore.DataAccess.Domain.Context;
using JewelleryStore.DataAccess.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.Repository
{
    public class JewelleryRepository : Repository<Jewel>, IJewelleryRepository
    {
        public JewelleryRepository(JewelleryStoreDBContext context) : base(context)
        {
        }

        public decimal? GetPrice(string jewelName)
        {
            var jewel = AppDataContext.Jewels.FirstOrDefault(e=>e.Name.Equals(jewelName, StringComparison.OrdinalIgnoreCase));
            return jewel?.PricePerGram;
        }

        public IEnumerable<Jewel> GetAllJewelsPrice()
        {
            var jewelsPriceList = this.GetAll();
            return jewelsPriceList;
        }


    }
}
