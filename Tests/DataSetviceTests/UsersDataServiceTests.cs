namespace Tests.DataSetviceTests
{
    using System.Linq;
    using DAL;
    using DAL.DataServices;
    using DAL.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UsersDataServiceTests
    {
        private static IConnectionFactory connectionFactory;

        [ClassInitialize]
        public static void ClassInitialization(TestContext testContext)
        {
            connectionFactory = new NortwindDbConnectionFactory();
        }

        [TestMethod]
        public void GetGetByUserNameTest()
        {
            var usersDataService = new UsersDataService(connectionFactory);

            var user = usersDataService.GetByUserName("Manager1", "Manager1Password");

            Assert.IsNotNull(user);
            Assert.IsTrue(user.Roles.Any());
        }
    }
}
