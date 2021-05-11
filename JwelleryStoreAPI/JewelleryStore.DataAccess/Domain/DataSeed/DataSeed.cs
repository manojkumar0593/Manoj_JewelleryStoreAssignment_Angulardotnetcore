using JewelleryStore.DataAccess.Domain.Context;
using JewelleryStore.DataAccess.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore.DataAccess.Domain.DataSeed
{
    public class DataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new JewelleryStoreDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<JewelleryStoreDBContext>>()))
            {
                // Look for any board games.
                if (!context.Jewels.Any())
                {
                    context.Jewels.Add(
                        new Jewel
                        {
                            Id=1,
                            Name = "gold",
                            PricePerGram = 5000
                        });
                }

                if (!context.Roles.Any())
                {
                    context.Roles.Add(
                        new Roles
                        {
                            Id=1,
                            Name = "Regular"
                        });
                    context.Roles.Add(
                        new Roles
                        {
                            Id=2,
                            Name = "Privileged"
                        });
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                           UserId=1,
                           UserName="Manoj Kumar",
                           EmailId="manojkumar0593@gmail.com",
                           Password="qwerty123"
                        },
                         new User
                         {
                             UserId=2,
                             UserName = "Mohit Kumar",
                             EmailId = "mohit.kumar@gmail.com",
                             Password = "admin123"
                         }
                         ,
                         new User
                         {
                             UserId = 3,
                             UserName = "ADMIN",
                             EmailId = "admin",
                             Password = "admin123"
                         }

                        );
                }
                if (!context.UserRoles .Any())
                {
                    context.UserRoles.AddRange(
                        new UserRole
                        {
                           Id=1,
                           UserId=1,
                           RoleId=1,
                        },
                        new UserRole
                        {
                            Id = 2,
                            UserId = 2,
                            RoleId = 2,
                        },
                        new UserRole
                        {
                            Id = 3,
                            UserId = 3,
                            RoleId = 2,
                        }
                     );
                }


                context.SaveChanges();
            }
        }
    }
}
