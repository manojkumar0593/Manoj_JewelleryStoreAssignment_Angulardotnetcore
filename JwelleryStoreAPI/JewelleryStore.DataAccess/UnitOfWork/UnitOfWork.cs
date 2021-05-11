using JewelleryStore.DataAccess.Domain.Context;
using JewelleryStore.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(JewelleryStoreDBContext context)
        {
            _context = context;
            JewelleryRepository = new JewelleryRepository(_context);
            UserRepository = new UserRepository(_context);
        }

        private readonly JewelleryStoreDBContext _context;
        public IJewelleryRepository JewelleryRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
