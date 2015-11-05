namespace WindowsServiceHosting
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using WCFServices.OrdersService;

    internal static class ServiceHostsFactory
    {
        private static readonly Lazy<IEnumerable<ServiceHost>> HostsList = new Lazy<IEnumerable<ServiceHost>>(GetHosts);

        public static IEnumerable<ServiceHost> Hosts
        {
            get
            {
                return HostsList.Value;
            }
        }

        private static IEnumerable<ServiceHost> GetHosts()
        {
            var ordersServiceBaseAddress =
                new Uri("http://localhost:8733/Design_Time_Addresses/OrdersService/");

            var ordersServiceHost = new ServiceHost(typeof(OrdersService), ordersServiceBaseAddress);
            ordersServiceHost.AddServiceEndpoint(typeof(IOrdersService), new BasicHttpBinding(), string.Empty);

            ordersServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

            yield return ordersServiceHost;
        }
    }
}