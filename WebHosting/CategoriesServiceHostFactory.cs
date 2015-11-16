namespace WebHosting
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Description;
    using WCFServices.Cotracts;

    public class CategoriesServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var categoriesServiceHost = base.CreateServiceHost(serviceType, baseAddresses);

            categoriesServiceHost.Description.Endpoints.Clear();

            var categoriesServiceNetTcpAddress = new Uri("net.tcp://epruizhw0228:808/NorthwindWCFServices/CategoriesService_2.svc");

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

            return categoriesServiceHost;
        }
    }
}