using JewelleryStore.DataAccess.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess
{
    public interface IJewelleryRepository
    {
        decimal? GetPrice(string jewelName);
        IEnumerable<Jewel> GetAllJewelsPrice();
    }
}
