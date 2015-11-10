namespace Tests.SelfHostingServiceTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;
    using Tests.OrdersServiceSelfHosting;

    [TestClass]
    public class OrdersServiceTests : BaseOrdersServiceTests
    {
        private const string BasicHttpBindingIOrdersService = "BasicHttpBinding_IOrdersService1";
        private const string NetTcpBindingIOrdersService = "NetTcpBinding_IOrdersService1";

        [TestMethod]
        public void GetAllTest()
        {
            using (var client = new OrdersServiceClient(BasicHttpBindingIOrdersService))
            {
                this.GetAllTest(client);
            }
        }

        [TestMethod]
        public void SimulateLongRunningOperationTest()
        {
            using (var client = new OrdersServiceClient(NetTcpBindingIOrdersService))
            {
                const byte OperationRunningDurationInSeconds = 10;

                var startAt = DateTime.Now;
                Console.WriteLine(DateTime.Now.ToLongTimeString());

                client.SimulateLongRunningOperation(OperationRunningDurationInSeconds);

                var endAt = DateTime.Now;
                Console.WriteLine(DateTime.Now.ToLongTimeString());

                var duration = endAt - startAt;

                Assert.IsTrue(duration.TotalSeconds >= OperationRunningDurationInSeconds);
            }
        }
    }
}