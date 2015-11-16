namespace WCFServices.HostConfigurationFactory
{
    using System;
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

        protected void AddServiceMetadataBehavior()
        {
            if (this.ServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                this.ServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            }
        }
    }
}