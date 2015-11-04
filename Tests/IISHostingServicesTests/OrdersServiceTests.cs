namespace Tests.IISHostingServicesTests
{
    using System;
    using System.Linq;
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.OrdersService;

    [TestClass]
    public class OrdersServiceTests
    {
        private const string BasicHttpBindingIOrdersService = "BasicHttpBinding_IOrdersService";
        private const string NetTcpBindingIOrdersService = "NetTcpBinding_IOrdersService";
        private const string WsDualHttpBindingIOrdersSubscriptionService = "WSDualHttpBinding_IOrdersSubscriptionService";

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
        [ExpectedException(typeof(FaultException))]
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
        [ExpectedException(typeof(FaultException))]
        public void UpdateOrderFaultOnNullParameterTest()
        {
            this.UpdateOrderFaultOnNullParameterTest(BasicHttpBindingIOrdersService);
            this.UpdateOrderFaultOnNullParameterTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateOrderFaultOnAttemptNotInNewStateTest()
        {
            this.UpdateOrderFaultOnAttemptNotInNewStateTest(BasicHttpBindingIOrdersService);
            this.UpdateOrderFaultOnAttemptNotInNewStateTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest()
        {
            this.DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest(BasicHttpBindingIOrdersService);
            this.DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
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
        [ExpectedException(typeof(FaultException))]
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
        [ExpectedException(typeof(FaultException))]
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

        private void GetAllTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var allOrders = client.GetAll();

                Assert.IsTrue(allOrders != null && allOrders.Any());
            }
        }

        private void GetByIdTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var allOrders = client.GetAll();

                var orderId = allOrders.First().OrderId;

                var orderById = client.GetById(orderId);

                Assert.IsNotNull(orderById);
            }
        }

        private void CreateNewOrderFaultTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                client.CreateNewOrder(null);
            }
        }

        private void CreateNewOrderTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var newOrder = this.CreateNewOrder(endpointConfigurationName);
                var orderId = client.CreateNewOrder(newOrder);

                Assert.IsTrue(orderId > 0);

                var newOrderFromDB = client.GetById(orderId);

                Assert.IsNotNull(newOrderFromDB);
                Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.New));
            }
        }

        private void UpdateOrderFaultOnNullParameterTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                client.UpdateOrder(null);
            }
        }

        private void UpdateOrderFaultOnAttemptNotInNewStateTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.InWork) || dto.OrderState.Equals(OrderState.Closed));

                client.UpdateOrder(order);
            }
        }

        private void DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                client.DeleteOrder(-1);
            }
        }

        private void DeleteOrderFaultOnAttemptToDeleteClosedOrderTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.Closed));

                if (order != null)
                {
                    client.DeleteOrder(order.OrderId);
                }
            }
        }

        private void DeleteOrderTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var newOrder = this.CreateNewOrder(endpointConfigurationName);
                var orderId = client.CreateNewOrder(newOrder);

                var affectedRows = client.DeleteOrder(orderId);

                Assert.AreEqual(affectedRows, 1);
            }
        }

        private void ProcessOrderFaultTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.InWork) || dto.OrderState.Equals(OrderState.Closed));

                client.ProcessOrder(order.OrderId);
            }
        }

        private void ProcessOrderTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var newOrder = this.CreateNewOrder(endpointConfigurationName);
                var newOrderId = client.CreateNewOrder(newOrder);

                client.ProcessOrder(newOrderId);

                var newOrderFromDB = client.GetById(newOrderId);

                Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.InWork));
            }
        }

        private void CloseOrderFaultTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.New) || dto.OrderState.Equals(OrderState.Closed));

                client.CloseOrder(order.OrderId);
            }
        }

        private void CloseOrderTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var newOrder = this.CreateNewOrder(endpointConfigurationName);
                var newOrderId = client.CreateNewOrder(newOrder);

                client.ProcessOrder(newOrderId);

                client.CloseOrder(newOrderId);

                var newOrderFromDB = client.GetById(newOrderId);

                Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.Closed));
            }
        }

        private void SubscribeUnsubscribeOnOrderStatusChangingEventsTest(string endpointConfigurationName)
        {
            var callbacksClient = new SubscriptionServiceClient();
            using (var client = new OrdersSubscriptionServiceClient(new InstanceContext(callbacksClient), endpointConfigurationName))
            {
                var clientIdentifier = Guid.NewGuid().ToString();

                var subscribeResult = client.Subscribe(clientIdentifier);

                Assert.IsTrue(subscribeResult);

                var unsubscribeResult = client.Unsubscribe(clientIdentifier);

                Assert.IsTrue(unsubscribeResult);
            }
        }

        private OrderDTO CreateNewOrder(string endpointConfigurationName)
        {
            var order = this.GetExistingOrder(endpointConfigurationName);
            order.OrderId = 0;
            order.RequiredDate = DateTime.Now.AddDays(1);

            return order;
        }

        private OrderDTO GetExistingOrder(string endpointConfigurationName, Func<OrderDTO, bool> predicate = null)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var allOrders = client.GetAll();

                predicate = predicate ?? (dto => true);

                return allOrders.First(predicate);
            }
        }
    }

    internal class SubscriptionServiceClient : IOrdersSubscriptionServiceCallback
    {
        public void OrderStatusIsChanged(int orderId)
        {
            Console.WriteLine("Status of order with Id = {0} has been changed", orderId);
        }
    }
}