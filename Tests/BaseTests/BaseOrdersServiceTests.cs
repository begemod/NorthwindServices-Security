namespace Tests.BaseTests
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WCFServices.Cotracts;
    using WCFServices.DataContracts;

    internal interface IOrdersServiceChannel : IClientChannel, IOrdersService
    {
    }

    internal interface IOrdersSubscriptionChannel : IClientChannel, IOrdersSubscriptionService
    {
    }

    public class BaseOrdersServiceTests
    {
        protected void GetAllTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();
            var allOrders = client.GetAll();

            Assert.IsTrue(allOrders != null && allOrders.Any());
        }

        protected void GetByIdTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            var allOrders = client.GetAll();

            var orderId = allOrders.First().OrderId;

            var orderById = client.GetById(orderId);

            Assert.IsNotNull(orderById);
        }

        protected void CreateNewOrderFaultTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            try
            {
                client.CreateNewOrder(null);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void CreateNewOrderTest(string endpointConfigurationName)
        {
            var newOrder = this.CreateNewOrder(endpointConfigurationName);

            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();
            var orderId = client.CreateNewOrder(newOrder);

            Assert.IsTrue(orderId > 0);

            var newOrderFromDB = client.GetById(orderId);

            Assert.IsNotNull(newOrderFromDB);
            Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.New));
        }

        protected void UpdateOrderFaultOnNullParameterTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            try
            {
                client.UpdateOrder(null);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void UpdateOrderFaultOnAttemptNotInNewStateTest(string endpointConfigurationName)
        {
            var channel = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName);

            var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.InWork) || dto.OrderState.Equals(OrderState.Closed));

            var client = channel.CreateChannel();

            try
            {
                client.UpdateOrder(order);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void DeleteOrderFaultOnAttemptToDeleteNotExistingOrderTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            try
            {
                client.DeleteOrder(-1);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void DeleteOrderFaultOnAttemptToDeleteClosedOrderTest(string endpointConfigurationName)
        {
            var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.Closed));

            if (order != null)
            {
                var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

                try
                {
                    client.DeleteOrder(order.OrderId);

                    Assert.Fail();
                }
                catch (FaultException)
                {
                }
            }
        }

        protected void DeleteOrderTest(string endpointConfigurationName)
        {
            var newOrder = this.CreateNewOrder(endpointConfigurationName);

            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            var orderId = client.CreateNewOrder(newOrder);

            var affectedRows = client.DeleteOrder(orderId);

            Assert.AreEqual(affectedRows, 1);
        }

        protected void ProcessOrderFaultTest(string endpointConfigurationName)
        {
            var channel = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName);

            var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.InWork) || dto.OrderState.Equals(OrderState.Closed));

            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            try
            {
                client.ProcessOrder(order.OrderId);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void ProcessOrderTest(string endpointConfigurationName)
        {
            var channel = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName);

            var newOrder = this.CreateNewOrder(endpointConfigurationName);

            var client = channel.CreateChannel();

            var newOrderId = client.CreateNewOrder(newOrder);

            client.ProcessOrder(newOrderId);

            var newOrderFromDB = client.GetById(newOrderId);

            Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.InWork));
        }

        protected void CloseOrderFaultTest(string endpointConfigurationName)
        {
            var order = this.GetExistingOrder(endpointConfigurationName, dto => dto.OrderState.Equals(OrderState.New) || dto.OrderState.Equals(OrderState.Closed));

            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            try
            {
                client.CloseOrder(order.OrderId);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void CloseOrderTest(string endpointConfigurationName)
        {
            var newOrder = this.CreateNewOrder(endpointConfigurationName);

            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            var newOrderId = client.CreateNewOrder(newOrder);

            client.ProcessOrder(newOrderId);

            client.CloseOrder(newOrderId);

            var newOrderFromDB = client.GetById(newOrderId);

            Assert.IsTrue(newOrderFromDB.OrderState.Equals(OrderState.Closed));
        }

        protected void SubscribeUnsubscribeOnOrderStatusChangingEventsTest(string endpointConfigurationName)
        {
            var callbacksClient = new SubscriptionServiceClient();
            using (var channel = new DuplexChannelFactory<IOrdersSubscriptionChannel>(callbacksClient, endpointConfigurationName))
            {
                var client = channel.CreateChannel();

                this.SubscribeUnsubscribeTest(client);
            }
        }

        protected void BaseGetUnhandledExceptionTest(string endpointConfigurationName)
        {
            var channelFactory = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName);

            var client = channelFactory.CreateChannel();

            try
            {
                client.GetUnhandledException();

                Assert.Fail();
            }
            catch (CommunicationException)
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client = channelFactory.CreateChannel();
                }
            }

            // ensure that client channel was recreated successfully after exception
            client.GetAll();
        }

        protected void BaseGetMetadataOverHttpGetTest(string address)
        {
            this.BaseGetMetadataTest(address, MetadataExchangeClientMode.HttpGet);
        }

        protected void BaseGetMetadataOverMetadataExchangeTest(string address)
        {
            this.BaseGetMetadataTest(address, MetadataExchangeClientMode.MetadataExchange);
        }

        protected void BaseCheckEndpointsTest(string address)
        {
            var client = new MetadataExchangeClient(new Uri(address), MetadataExchangeClientMode.HttpGet);

            var metadata = client.GetMetadata();

            var wsdlImporter = new WsdlImporter(metadata);

            var endpoints = wsdlImporter.ImportAllEndpoints();

            foreach (var endpoint in endpoints)
            {
                this.TryCallOrdersServiceOperations(endpoint);
            }
        }

        private void TryCallOrdersServiceOperations(ServiceEndpoint endpoint)
        {
            if (endpoint.Contract.Name == typeof(IOrdersService).Name)
            {
                Type endpointType = endpoint.Binding.GetType();

                PropertyInfo maxReceivedMessageSizepropertyInfo = endpointType.GetProperty("MaxReceivedMessageSize");

                if (maxReceivedMessageSizepropertyInfo != null)
                {
                    PropertyInfo maxBufferSizePropertyInfo = endpointType.GetProperty("MaxBufferSize");

                    if (maxBufferSizePropertyInfo != null)
                    {
                        maxBufferSizePropertyInfo.SetValue(endpoint.Binding, 6553600, null);
                    }

                    maxReceivedMessageSizepropertyInfo.SetValue(endpoint.Binding, 6553600, null);
                }

                var channelFactory = new ChannelFactory<IOrdersService>(endpoint.Binding, endpoint.Address);

                var channel = channelFactory.CreateChannel();

                var allOrders = channel.GetAll();
            }
            else if (endpoint.Contract.Name == typeof(IOrdersSubscriptionService).Name)
            {
                var callbacksClient = new SubscriptionServiceClient();

                var channelFactory = new DuplexChannelFactory<IOrdersSubscriptionService>(
                    callbacksClient,
                    endpoint.Binding,
                    endpoint.Address);

                var channel = channelFactory.CreateChannel();

                this.SubscribeUnsubscribeTest(channel);
            }
        }

        private void BaseGetMetadataTest(string endpointAddress, MetadataExchangeClientMode metadataExchangeClientMode)
        {
            var client = new MetadataExchangeClient(new Uri(endpointAddress), metadataExchangeClientMode);

            var metadata = client.GetMetadata();

            var wsdlImporter = new WsdlImporter(metadata);

            var contracts = wsdlImporter.ImportAllContracts();

            Assert.IsTrue(contracts.Any());
        }

        private void SubscribeUnsubscribeTest(IOrdersSubscriptionService client)
        {
            var clientIdentifier = Guid.NewGuid().ToString();

            var subscribeResult = client.Subscribe(clientIdentifier);

            Assert.IsTrue(subscribeResult);

            var unsubscribeResult = client.Unsubscribe(clientIdentifier);

            Assert.IsTrue(unsubscribeResult);
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
            var client = TestsEnviroment.GetChannelFactory<IOrdersServiceChannel>(endpointConfigurationName).CreateChannel();

            var allOrders = client.GetAll();

            predicate = predicate ?? (dto => true);

            return allOrders.First(predicate);
        }
    }

    internal class SubscriptionServiceClient : IBroadcastCallback
    {
        public void OrderStatusIsChanged(int orderId)
        {
            Console.WriteLine("Status of order with Id = {0} has been changed", orderId);
        }
    }
}