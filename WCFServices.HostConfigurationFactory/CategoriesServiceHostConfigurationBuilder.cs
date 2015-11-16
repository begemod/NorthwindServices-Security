namespace WCFServices.HostConfigurationFactory
{
    using System;
    using System.ServiceModel;
    using WCFServices.CategoriesService;
    using WCFServices.Cotracts;

    public class CategoriesServiceHostConfigurationBuilder : ServiceHostConfigurationBuilder<CategoriesService>
    {
        public CategoriesServiceHostConfigurationBuilder(params Uri[] baseAddresses)
            : base(baseAddresses)
        {
        }

        public CategoriesServiceHostConfigurationBuilder AddNetTcpEndpoint(string address)
        {
            this.AddNetTcpEndpoint<ICategoriesService>(address, new NetTcpBinding { TransferMode = TransferMode.Streamed });

            return this;
        }

        public override ServiceHost Configure()
        {
            this.AddBasicHttpEndpoint<ICategoriesService>(string.Empty, new BasicHttpBinding { TransferMode = TransferMode.Streamed });
            this.AddServiceMetadataBehavior();

            return this.ServiceHost;
        }
    }
}