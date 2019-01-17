using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using SEWKTand.Data;
using SEWKTand.Data.Entities;
using SEWKTand.Features.Admin;
using SEWKTand.Features.Shared.Security.Interfaces;
using System.Linq;
using Xunit;

namespace XUnitTestSEWKTand
{
    public class UnitTestAdmin
    {
        static DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DbContext_testDatabase")
                .Options;

        [Fact]
        public async void FindAdminById_WithExistingId()
        {
            //Arrange
            var fakeGenerateSecurePassword = A.Fake<IGenerateSecurePassword>();
            var expectedId = 3;

            DataContext context;
            using (context = new DataContext(options))
            {
                context.Admin.Add(new EntityAdmin { Id = 1, Email = "email1", FirstName = "name1", LastName = "lastname1", HashedPassword = "hashedPass1", PhoneNumber = "0736666666", Role = 0 });
                context.Admin.Add(new EntityAdmin { Id = 2, Email = "email2", FirstName = "name2", LastName = "lastname2", HashedPassword = "hashedPass2", PhoneNumber = "0736666666", Role = 0 });
                context.Admin.Add(new EntityAdmin { Id = 3, Email = "email3", FirstName = "name3", LastName = "lastname3", HashedPassword = "hashedPass3", PhoneNumber = "0736666666", Role = 0 });
                context.Admin.Add(new EntityAdmin { Id = 4, Email = "email4", FirstName = "name4", LastName = "lastname4", HashedPassword = "hashedPass4", PhoneNumber = "0736666666", Role = 0 });
                context.SaveChanges();
            }

            using (context = new DataContext(options))
            {
                //Act
                var fakeService = new AdminService(context, fakeGenerateSecurePassword);
                var user = await fakeService.GetByIdAsync(3);

                //Assert
                Assert.NotNull(user);
                Assert.Equal(expectedId, user.Id);
            }
        }

        [Fact]
        public async void AddAdminThroughService_WithValidDetails()
        {
            var fakeGenerateSecurePassword = A.Fake<IGenerateSecurePassword>();
            var model = new EntityAdmin { Id = 5, Email = "email5", FirstName = "name5", LastName = "lastname5", HashedPassword = "hashedPass5", PhoneNumber = "0736666666", Role = 0 };
            DataContext context;

            using (context = new DataContext(options))
            {
                var fakeService = new AdminService(context, fakeGenerateSecurePassword);
                await fakeService.RegisterAsync(model);
                context.SaveChanges();
            }

            using (context = new DataContext(options))
            {
                var result = context.Admin.Count();
                Assert.Equal(1, result);
            }
        }
    }
}
