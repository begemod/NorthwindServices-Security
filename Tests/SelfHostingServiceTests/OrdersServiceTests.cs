namespace Tests.SelfHostingServiceTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;

    [TestClass]
    public class OrdersServiceTests : BaseOrdersServiceTests
    {
        private const string BasicHttpBindingIOrdersService = "BasicHttpBinding_IOrdersService1";

        [TestMethod]
        public void GetAllTest()
        {
            this.GetAllTest(BasicHttpBindingIOrdersService);
        }
    }
}