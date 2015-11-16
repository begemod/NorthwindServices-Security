namespace WebHosting
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Description;
    using WCFServices.Cotracts;
    using WCFServices.OrdersService;

    public class OrdersServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var ordersServiceHost = new ServiceHost(serviceType, baseAddresses);

            var orderServiceNetTcpAddress = new Uri("net.tcp://epruizhw0228:808/NorthwindWCFServices/OrdersService_2.svc");

            ordersServiceHost.Description.Endpoints.Clear();

            ordersServiceHost.AddServiceEndpoint(typeof(IOrdersService), new BasicHttpBinding(), string.Empty);

            ordersServiceHost.AddServiceEndpoint(typeof(IOrdersService), new NetTcpBinding(), orderServiceNetTcpAddress);

            ordersServiceHost.AddServiceEndpoint(typeof(IOrdersSubscriptionService), new WSDualHttpBinding(), "subscription");

            if (ordersServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                ordersServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            }

            return ordersServiceHost;
        }
    }
}