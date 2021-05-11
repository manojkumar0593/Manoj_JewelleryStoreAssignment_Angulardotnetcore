using JewelleryStore.DataAccess.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.Domain.Context
{
    public class JewelleryStoreDBContext : DbContext
    {

        public JewelleryStoreDBContext(DbContextOptions<JewelleryStoreDBContext> context):base(context)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Jewel> Jewels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(u => new
            {
                u.UserId,
                u.RoleId
            });
            modelBuilder.Entity<UserRole>().HasOne(u => u.Role).WithMany(r => r.UserRoles).HasForeignKey(f => f.RoleId).IsRequired();
            modelBuilder.Entity<UserRole>().HasOne(u => u.User).WithOne(r => r.UserRole).HasForeignKey("UserRole").IsRequired();
        }
}
}
