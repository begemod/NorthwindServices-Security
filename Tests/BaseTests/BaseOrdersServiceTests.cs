namespace Tests.BaseTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.OrdersServiceSelfHosting;

    public class BaseOrdersServiceTests
    {
        protected void GetAllTest(string endpointConfigurationName)
        {
            using (var client = new OrdersServiceClient(endpointConfigurationName))
            {
                var allOrders = client.GetAll();

                Assert.IsTrue(allOrders != null && allOrders.Any());
            }
        }
    }
}