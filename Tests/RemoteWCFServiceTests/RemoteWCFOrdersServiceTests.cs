namespace Tests.RemoteWCFServiceTests
{
    using System;
    using System.Linq;
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.OrdersService;

    [TestClass]
    public class RemoteWCFOrdersServiceTests
    {
        [TestMethod]
        public void GetAllTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var allOrders = client.GetAll();

                Assert.IsTrue(allOrders != null && allOrders.Any());
            }
        }

        [TestMethod]
        public void GetByIdTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var allOrders = client.GetAll();

                var orderId  = allOrders.First().OrderId;

                var orderById = client.GetById(orderId);

                Assert.IsNotNull(orderById);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void CreateNewOrderFaultTest()
        {
            using (var client = new OrdersServiceClient())
            {
                client.CreateNewOrder(null);
            }
        }

        [TestMethod]
        public void CreateNewOrderTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var newOrder = this.CreateNewOrder();
                var orderId = client.CreateNewOrder(newOrder);

                Assert.IsTrue(orderId > 0);

                var newOrderFromDB = client.GetById(orderId);

                Assert.IsNotNull(newOrderFromDB);
                Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.New));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateOrderFaultOnNullParameterTest()
        {
            using (var client = new OrdersServiceClient())
            {
                client.UpdateOrder(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateOrderFaultOnAttemptNotInNewStateTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var order = this.GetExistingOder(dto => dto.OrderState.Equals(OrderState.InWork) || dto.OrderState.Equals(OrderState.Closed));

                client.UpdateOrder(order);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest()
        {
            using (var client = new OrdersServiceClient())
            {
                client.DeleteOrder(-1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void DeleteOrderFaultOnAttemptToDeleteClosedOrderTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var order = this.GetExistingOder(dto => dto.OrderState.Equals(OrderState.Closed));

                if (order != null)
                {
                    client.DeleteOrder(order.OrderId);
                }
            }
        }

        [TestMethod]
        public void DeleteOrderTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var newOrder = this.CreateNewOrder();
                var orderId = client.CreateNewOrder(newOrder);

                var affectedRows = client.DeleteOrder(orderId);

                Assert.AreEqual(affectedRows, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ProcessOrderFaultTestP()
        {
            using (var client = new OrdersServiceClient())
            {
                var order = this.GetExistingOder(dto => dto.OrderState.Equals(OrderState.InWork) || dto.OrderState.Equals(OrderState.Closed));

                client.ProcessOrder(order.OrderId);
            }
        }

        [TestMethod]
        public void ProcessOrderTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var newOrder = this.CreateNewOrder();
                var newOrderId = client.CreateNewOrder(newOrder);

                client.ProcessOrder(newOrderId);

                var newOrderFromDB = client.GetById(newOrderId);

                Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.InWork));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void CloseOrderFaultTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var order = this.GetExistingOder(dto => dto.OrderState.Equals(OrderState.New) || dto.OrderState.Equals(OrderState.Closed));

                client.CloseOrder(order.OrderId);
            }
        }

        [TestMethod]
        public void CloseOrderTest()
        {
            using (var client = new OrdersServiceClient())
            {
                var newOrder = this.CreateNewOrder();
                var newOrderId = client.CreateNewOrder(newOrder);

                client.ProcessOrder(newOrderId);

                client.CloseOrder(newOrderId);

                var newOrderFromDB = client.GetById(newOrderId);

                Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.Closed));
            }
        }

        private OrderDTO CreateNewOrder()
        {
            var order = this.GetExistingOder();
            order.OrderId = 0;
            order.RequiredDate = DateTime.Now.AddDays(1);

            return order;
        }

        private OrderDTO GetExistingOder(Func<OrderDTO, bool> predicate = null)
        {
            using (var client = new OrdersServiceClient())
            {
                var allOrders = client.GetAll();

                predicate = predicate ?? (dto => true);

                return allOrders.First(predicate);
            }
        }
    }
}