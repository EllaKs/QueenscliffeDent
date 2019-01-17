using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void AddAdminToDatabase_ExpectedToWork()
        {
            var fakeAdminService = A.Fake<IAdminService>();
            var sut = new EntityAdmin { Id = 002, Email = "email2", FirstName = "name2", LastName = "lastname2", HashedPassword = "hashedPass2", PhoneNumber = "0736666666", Role = 0 };

            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "RegisterAdmin_Database")
            .Options;

            using (var context = new DataContext(options))
            {
                fakeAdminService.RegisterAsync(sut);
            }

            using (var context = new DataContext(options))
            {
                Assert.AreEqual(1, context.Admin.Count());
            }
        }
    }
}
