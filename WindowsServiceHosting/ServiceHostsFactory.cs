namespace WindowsServiceHosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using WCFServices.CategoriesService;
    using WCFServices.OrdersService;
    using WCFServices.OrdersSubscriptionService;

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
            return OrdersServiceHost().Union(CategoriesServiceHost());
        }

        private static IEnumerable<ServiceHost> OrdersServiceHost()
        {
            var ordersServiceBaseAddress =
                new Uri("http://epruizhw0228:8733/Design_Time_Addresses/NorthwindWCFServices/OrdersService/");

            var orderServiceNetTcpAddress = new Uri("net.tcp://epruizhw0228:809/NorthwindWCFServices/OrdersService");

            var ordersServiceHost = new ServiceHost(typeof(OrdersService), ordersServiceBaseAddress);

            ordersServiceHost.AddServiceEndpoint(typeof(IOrdersService), new BasicHttpBinding(), string.Empty);

            ordersServiceHost.AddServiceEndpoint(typeof(IOrdersService), new NetTcpBinding(), orderServiceNetTcpAddress);

            ordersServiceHost.AddServiceEndpoint(typeof(IOrdersSubscriptionService), new WSDualHttpBinding(), "subscription");

            if (ordersServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                ordersServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            }

            yield return ordersServiceHost;
        }

        private static IEnumerable<ServiceHost> CategoriesServiceHost()
        {
            var categoriesServiceBaseAddress =
                new Uri("http://epruizhw0228:8733/Design_Time_Addresses/NorthwindWCFServices/CategoriesService/");

            var categoriesServiceNetTcpAddress = new Uri("net.tcp://epruizhw0228:810/NorthwindWCFServices/CategoriesService");

            var categoriesServiceHost = new ServiceHost(typeof(CategoriesService), categoriesServiceBaseAddress);

            categoriesServiceHost.AddServiceEndpoint(
                typeof(ICategoriesService),
                new BasicHttpBinding { TransferMode = TransferMode.Streamed },
                string.Empty);

            categoriesServiceHost.AddServiceEndpoint(
                typeof(ICategoriesService),
                new NetTcpBinding { TransferMode = TransferMode.Streamed },
                categoriesServiceNetTcpAddress);

            if (categoriesServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                categoriesServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            }

            yield return categoriesServiceHost;
        }
    }
}