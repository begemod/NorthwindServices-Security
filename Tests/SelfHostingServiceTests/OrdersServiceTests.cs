namespace Tests.SelfHostingServiceTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;

    [TestClass]
    public class OrdersServiceTests : BaseOrdersServiceTests
    {
        private const string BasicHttpBindingIOrdersService = "BasicHttpBinding_IOrdersService_SH";
        private const string NetTcpBindingIOrdersService = "NetTcpBinding_IOrdersService_SH";
        private const string WsDualHttpBindingIOrdersSubscriptionService = "WSDualHttpBinding_IOrdersSubscriptionService_SH";
        private const string HttpMexEndpointAddress = "http://epruizhw0228:8733/Design_Time_Addresses/NorthwindWCFServices/OrdersService/mex";
        private const string MetadataAddress = "http://epruizhw0228:8733/Design_Time_Addresses/NorthwindWCFServices/OrdersService/?wsdl";
        private const string TcpMexEndpointAddress = "net.tcp://epruizhw0228:809/NorthwindWCFServices/OrdersService/mex";

        [TestMethod]
        public void GetAllTest()
        {
            this.GetAllTest(BasicHttpBindingIOrdersService);
            this.GetAllTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            this.GetByIdTest(BasicHttpBindingIOrdersService);
            this.GetByIdTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void CreateNewOrderFaultTest()
        {
            this.CreateNewOrderFaultTest(BasicHttpBindingIOrdersService);
            this.CreateNewOrderFaultTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void CreateNewOrderTest()
        {
            this.CreateNewOrderTest(BasicHttpBindingIOrdersService);
            this.CreateNewOrderTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void UpdateOrderFaultOnNullParameterTest()
        {
            this.UpdateOrderFaultOnNullParameterTest(BasicHttpBindingIOrdersService);
            this.UpdateOrderFaultOnNullParameterTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void UpdateOrderFaultOnAttemptNotInNewStateTest()
        {
            this.UpdateOrderFaultOnAttemptNotInNewStateTest(BasicHttpBindingIOrdersService);
            this.UpdateOrderFaultOnAttemptNotInNewStateTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest()
        {
            this.DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest(BasicHttpBindingIOrdersService);
            this.DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void DeleteOrderFaultOnAttemptToDeleteClosedOrderTest()
        {
            this.DeleteOrderFaultOnAttemptToDeleteClosedOrderTest(BasicHttpBindingIOrdersService);
            this.DeleteOrderFaultOnAttemptToDeleteClosedOrderTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void DeleteOrderTest()
        {
            this.DeleteOrderTest(BasicHttpBindingIOrdersService);
            this.DeleteOrderTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void ProcessOrderFaultTest()
        {
            this.ProcessOrderFaultTest(BasicHttpBindingIOrdersService);
            this.ProcessOrderFaultTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void ProcessOrderTest()
        {
            this.ProcessOrderTest(BasicHttpBindingIOrdersService);
            this.ProcessOrderTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void CloseOrderFaultTest()
        {
            this.CloseOrderFaultTest(BasicHttpBindingIOrdersService);
            this.CloseOrderFaultTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void CloseOrderTest()
        {
            this.CloseOrderTest(BasicHttpBindingIOrdersService);
            this.CloseOrderTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void SubscribeUnsubscribeOnOrderStatusChangingEventsTest()
        {
            this.SubscribeUnsubscribeOnOrderStatusChangingEventsTest(WsDualHttpBindingIOrdersSubscriptionService);
        }

        [TestMethod]
        public void GetMetadataTest()
        {
            this.BaseGetMetadataOverMetadataExchangeTest(HttpMexEndpointAddress);
            this.BaseGetMetadataOverHttpGetTest(MetadataAddress);
            this.BaseGetMetadataOverMetadataExchangeTest(TcpMexEndpointAddress);
        }

        [TestMethod]
        public void CheckEndpointsTest()
        {
            this.BaseCheckEndpointsTest(MetadataAddress);
        }
    }
}