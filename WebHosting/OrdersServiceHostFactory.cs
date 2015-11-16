namespace WebHosting
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using WCFServices.HostConfigurationFactory;

    public class OrdersServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new OrdersServiceHostConfigurationBuilder(baseAddresses)
                        .AddNetTcpEndpoint("net.tcp://epruizhw0228:808/NorthwindWCFServices/OrdersService_2.svc")
                        .Configure();
        }
    }
}