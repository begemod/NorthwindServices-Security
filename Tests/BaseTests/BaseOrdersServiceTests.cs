namespace Tests.BaseTests
{
    using System.Linq;
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WCFServices.Cotracts;

    internal interface IOrdersServiceChannel : IClientChannel, IOrdersService
    {
    }

    public class BaseOrdersServiceTests
    {
        protected void GetAllTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<IOrdersServiceChannel>(endpointConfigurationName))
            {
                var client = channel.CreateChannel();
                var allOrders = client.GetAll();

                Assert.IsTrue(allOrders != null && allOrders.Any());
            }
        }

        protected void GetByIdTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<IOrdersServiceChannel>(endpointConfigurationName))
            {
                var client = channel.CreateChannel();

                var allOrders = client.GetAll();

                var orderId = allOrders.First().OrderId;

                var orderById = client.GetById(orderId);

                Assert.IsNotNull(orderById);
            }
        }
    }
}