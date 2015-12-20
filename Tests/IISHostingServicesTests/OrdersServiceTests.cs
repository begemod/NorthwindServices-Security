namespace Tests.IISHostingServicesTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;

    [TestClass]
    public class OrdersServiceTests : BaseOrdersServiceTests
    {
        private const string BasicHttpBindingIOrdersService = "BasicHttpBinding_IOrdersService_IIS";
        private const string NetTcpBindingIOrdersService = "BasicHttpBinding_IOrdersService_IIS";//"NetTcpBinding_IOrdersService_IIS";
        private const string WsDualHttpBindingIOrdersSubscriptionService = "WSDualHttpBinding_IOrdersSubscriptionService_IIS";
        private const string WsHttpBindingIOrdersService = "WsHttpBinding_IOrdersService_IIS";
        private const string HttpMexEndpointAddress = "http://localhost/NorthwindWCFServices/OrdersService.svc/mex";
        private const string MetadataAddress = "http://localhost/NorthwindWCFServices/OrdersService.svc/mex?wsdl";

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
        public void GetUnhandledExceptionTest()
        {
            this.BaseGetUnhandledExceptionTest(WsHttpBindingIOrdersService);
        }

        [TestMethod]
        public void GetMetadataTest()
        {
            this.BaseGetMetadataTest(HttpMexEndpointAddress);
            this.BaseGetMetadataTest(MetadataAddress);
        }
    }
}