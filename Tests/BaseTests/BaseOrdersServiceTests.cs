namespace Tests.BaseTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.OrdersServiceSelfHosting;

    public class BaseOrdersServiceTests
    {
        protected void GetAllTest(IOrdersService client)
        {
            var allOrders = client.GetAll();

            Assert.IsTrue(allOrders != null && allOrders.Any());
        }
    }
}