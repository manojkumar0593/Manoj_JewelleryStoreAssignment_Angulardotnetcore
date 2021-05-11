using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.Domain.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        //public virtual ICollection<User> Users { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Roles Role { get; set; }
    }
}
