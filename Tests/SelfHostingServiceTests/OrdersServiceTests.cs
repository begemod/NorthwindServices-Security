namespace Tests.SelfHostingServiceTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;
    using Tests.OrdersServiceSelfHosting;

    [TestClass]
    public class OrdersServiceTests : BaseOrdersServiceTests
    {
        private const string BasicHttpBinding_IOrdersService_IIS = "BasicHttpBinding_IOrdersService_IIS";
        private const string NetTcpBinding_IOrdersService_IIS = "NetTcpBinding_IOrdersService_IIS";

        [TestMethod]
        public void GetAllTest()
        {
            this.GetAllTest(BasicHttpBinding_IOrdersService_IIS);
            this.GetAllTest(NetTcpBinding_IOrdersService_IIS);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            this.GetByIdTest(BasicHttpBinding_IOrdersService_IIS);
            this.GetByIdTest(NetTcpBinding_IOrdersService_IIS);
        }

        [TestMethod]
        public void SimulateLongRunningOperationTest()
        {
            using (var client = new OrdersServiceClient(NetTcpBinding_IOrdersService_IIS))
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