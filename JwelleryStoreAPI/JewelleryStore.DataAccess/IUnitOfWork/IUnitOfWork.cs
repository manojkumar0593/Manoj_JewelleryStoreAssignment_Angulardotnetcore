using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IJewelleryRepository JewelleryRepository { get; }
    }
}
