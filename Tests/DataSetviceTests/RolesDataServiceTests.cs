namespace Tests.DataSetviceTests
{
    using System.Linq;
    using DAL.DataServices;
    using DAL.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RolesDataServiceTests
    {
        [TestMethod]
        public void GetUserRolesTest()
        {
            var connectionFactory = new NortwindDbConnectionFactory();
            var rolesDataService = new RolesDataService(connectionFactory);

            const int UserId = 1;

            var userRoles = rolesDataService.GetUserRoles(UserId);

            Assert.IsTrue(userRoles.Any());
        }
    }
}
