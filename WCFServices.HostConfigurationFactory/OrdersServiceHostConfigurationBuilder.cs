namespace WCFServices.HostConfigurationFactory
{
    using System;
    using System.ServiceModel;
    using WCFServices.Cotracts;
    using WCFServices.OrdersService;

    public class OrdersServiceHostConfigurationBuilder : ServiceHostConfigurationBuilder<OrdersService>
    {
        public OrdersServiceHostConfigurationBuilder(params Uri[] baseAddresses)
            : base(baseAddresses)
        {
        }

        public OrdersServiceHostConfigurationBuilder AddNetTcpEndpoint(string address)
        {
            this.AddNetTcpEndpoint<IOrdersService>(address);

            return this;
        }

        public OrdersServiceHostConfigurationBuilder AddMetadataPublicationOverMexHttpBinding(string address)
        {
            this.AddMexHttpEndpoint(address);

            return this;
        }

        public OrdersServiceHostConfigurationBuilder AddMetadataPublicationOverMexTcpBinding(string address)
        {
            this.AddMexTcpEndpoint(address);

            return this;
        }

        public override ServiceHost Configure()
        {
            this.AddBasicHttpEndpoint<IOrdersService>(string.Empty);
            this.AddWSDualHttpBinding<IOrdersSubscriptionService>("subscription");
            this.AddServiceMetadataBehavior();

            return this.ServiceHost;
        }
    }
}