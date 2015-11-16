namespace WebHosting
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using WCFServices.HostConfigurationFactory;

    public class CategoriesServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new CategoriesServiceHostConfigurationBuilder(baseAddresses)
                    .AddNetTcpEndpoint("net.tcp://epruizhw0228:808/NorthwindWCFServices/CategoriesService_2.svc")
                    .Configure();
        }
    }
}