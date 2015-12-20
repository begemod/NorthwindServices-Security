namespace WCFServices.HostConfigurationFactory
{
    using System;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    public abstract class ServiceHostConfigurationBuilder<T>
        where T : class
    {
        protected readonly ServiceHost ServiceHost;

        protected ServiceHostConfigurationBuilder(params Uri[] baseAddresses)
        {
            this.ServiceHost = new ServiceHost(typeof(T), baseAddresses);
            this.ServiceHost.Description.Endpoints.Clear();
        }

        public abstract ServiceHost Configure();

        protected void AddNetTcpEndpoint<TK>(string address)
        {
            this.ServiceHost.AddServiceEndpoint(typeof(TK), new NetTcpBinding(), address);
        }

        protected void AddNetTcpEndpoint<TK>(string address, NetTcpBinding binding)
        {
            this.ServiceHost.AddServiceEndpoint(typeof(TK), binding, address);
        }

        protected void AddWSDualHttpBinding<TK>(string address)
        {
            this.ServiceHost.AddServiceEndpoint(typeof(TK), new WSDualHttpBinding(), address);
        }

        protected void AddBasicHttpEndpoint<TK>(string address)
        {
            this.ServiceHost.AddServiceEndpoint(typeof(TK), new BasicHttpBinding(), address);
        }

        protected void AddBasicHttpEndpoint<TK>(string address, BasicHttpBinding binding)
        {
            this.ServiceHost.AddServiceEndpoint(typeof(TK), binding, address);
        }

        protected void AddMexHttpEndpoint(string address)
        {
            this.AddServiceMetadataBehavior();

            var mexHttpBinding = MetadataExchangeBindings.CreateMexHttpBinding();

            this.ServiceHost.AddServiceEndpoint(typeof(IMetadataExchange), mexHttpBinding, address);
        }

        protected void AddMexTcpEndpoint(string address)
        {
            this.AddServiceMetadataBehavior();

            var mexTcpBinding = MetadataExchangeBindings.CreateMexTcpBinding();

            this.ServiceHost.AddServiceEndpoint(typeof(IMetadataExchange), mexTcpBinding, address);
        }

        protected void AddServiceMetadataBehavior()
        {
            if (this.ServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                var serviceMetadataBehavior = new ServiceMetadataBehavior { HttpGetEnabled = true };

                this.ServiceHost.Description.Behaviors.Add(serviceMetadataBehavior);
            }
        }
    }
}