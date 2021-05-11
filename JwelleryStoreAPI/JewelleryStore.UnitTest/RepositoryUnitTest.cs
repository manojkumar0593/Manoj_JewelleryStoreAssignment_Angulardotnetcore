using JewelleryStore.DataAccess;
using JewelleryStore.DataAccess.Domain.Context;
using JewelleryStore.DataAccess.Domain.Models;
using Moq;
using System;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.DataAccess.UnitOfWork;
using Jewellery.ReportHelper.IReportServices;

namespace JewelleryStore.UnitTest
{
    public class RepositoryUnitTest : IDisposable
    {
        private readonly JewelleryStoreDBContext dbContext;
        public RepositoryUnitTest()
        {
            var options = new DbContextOptionsBuilder<JewelleryStoreDBContext>()
            .UseInMemoryDatabase(nameof(TestCheckUserExist))
            .Options;
            dbContext = new JewelleryStoreDBContext(options);
            CreateUsers(dbContext);
            CreateRoles(dbContext);
            AssociateUserRoles(dbContext);
            CreateJewelleryBase(dbContext);
        }

        [Fact]
        public void TestCheckUserExist()
        {
            IUnitOfWork uow = new UnitOfWork(dbContext);
            User user = new() { EmailId="admin", Password="admin123" };

            var loggedInUser = uow.UserRepository.Login(user);

            Assert.NotNull(loggedInUser); ;
            Assert.True(!string.IsNullOrEmpty(loggedInUser?.UserName));
        }

        [Fact]
        public void TestCheckUnauthorizedAccess()
        {

            IUnitOfWork uow = new UnitOfWork(dbContext);
            User user = new() { EmailId = "Sohan", Password = "admin" };

            var loggedInUser = uow.UserRepository.Login(user);

            Assert.Null(loggedInUser);
        }


        [Fact]
        public void TestGetAllUsers()
        {
            IUnitOfWork uow = new UnitOfWork(dbContext);
            var users = uow.UserRepository.GetAllUser();

            Assert.True(users.Any());
        }


        [Fact]
        public void TestIfUserNotProvided()
        {
            IUnitOfWork uow = new UnitOfWork(dbContext);
            User user = null;

            //var loggedInUser = uow.UserRepository.Login(user);

            Assert.ThrowsAny<Exception>(() => uow.UserRepository.Login(user));
        }

        [Fact]
        public void TestGetJewelPrice()
        {

            IUnitOfWork uow = new UnitOfWork(dbContext);
            var price = uow.JewelleryRepository.GetPrice("gold");

            Assert.NotNull(price);
        }

        [Fact]
        public void TestGetOtherJewelPrice()
        {
            IUnitOfWork uow = new UnitOfWork(dbContext);
            var price = uow.JewelleryRepository.GetPrice("silver");

            Assert.Null(price);
        }

        [Fact]
        public void TestPrintToPaperReport()
        {
            var reportService = new Mock<IReportService>();

            reportService.Setup(e => e.PrintToPaper(It.IsAny<string>())).Returns(() =>  null);

            Assert.Null(reportService.Object.PrintToPaper(It.IsAny<string>()));
            //Assert.ThrowsAny< NotImplementedException>(() => reportService.Object.PrintToPaper(It.IsAny<string>()));
        }

        [Fact]
        public void TestPdfReportDownload()
        {
            var reportService = new Mock<IReportService>();

            reportService.Setup(e => e.GeneratePdfReport(It.IsAny<string>())).Returns(new byte[] { });

            var response = reportService.Object.GeneratePdfReport(It.IsAny<string>());
            Assert.NotNull(response);
        }

        private void CreateUsers(JewelleryStoreDBContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                dbContext.Users.AddRange(
                             new User
                             {
                                 UserId = 1,
                                 UserName = "Manoj Kumar",
                                 EmailId = "manojkumar0593@gmail.com",
                                 Password = "qwerty123"
                             },
                              new User
                              {
                                  UserId = 2,
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


                dbContext.SaveChanges();
            }
        }

        private void CreateRoles(JewelleryStoreDBContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(
                        new Roles
                        {
                            Id = 1,
                            Name = "Regular"
                        },
                        new Roles
                        {
                            Id = 2,
                            Name = "Privileged"
                        }
             );

                dbContext.SaveChanges();
            }
        }

        private void AssociateUserRoles(JewelleryStoreDBContext dbContext)
        {
            if (!dbContext.UserRoles.Any())
            {
                dbContext.UserRoles.AddRange(
                        new UserRole
                        {
                            Id = 1,
                            UserId = 1,
                            RoleId = 1,
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

                dbContext.SaveChanges();
            }
        }


        private void CreateJewelleryBase(JewelleryStoreDBContext dbContext)
        {

            if (!dbContext.Jewels.Any())
            {
                dbContext.Jewels.Add(
                       new Jewel
                       {
                           Id = 1,
                           Name = "gold",
                           PricePerGram = 5000
                       });
                dbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
