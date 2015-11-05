namespace WindowsServiceHosting
{
    using System;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class NortwindWCFServicesHost : ServiceBase
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private readonly IMessagesContainer messagesContainer;

        public NortwindWCFServicesHost(IMessagesContainer messagesContainer)
        {
            this.messagesContainer = messagesContainer;
            this.InitializeComponent();
        }

        public void StartServer()
        {
            this.ConfigureHosts();
        }

        public void StopServer()
        {
            this.cancellationTokenSource.Cancel();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        private void WriteMessage(string message)
        {
            if (this.messagesContainer != null)
            {
                this.messagesContainer.AddMessage(message);
            }
        }

        private void ConfigureHosts()
        {
            var hosts = ServiceHostsFactory.Hosts.ToList();

            if (!hosts.Any())
            {
                return;
            }

            foreach (var serviceHost in hosts)
            {
                var host = serviceHost;
                this.RunHost(host);
            }
        }

        private void RunHost(ServiceHost serviceHost)
        {
            Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        serviceHost.Open();

                        this.WriteMessage(string.Format("Host for {0} is running", serviceHost.Description.ServiceType));

                        while (true)
                        {
                            cancellationTokenSource.Token.ThrowIfCancellationRequested();
                            Thread.Sleep(100);
                        }
                    }
                    catch (CommunicationException exception)
                    {
                        this.WriteMessage(exception.Message);
                        this.WriteMessage(exception.StackTrace);
                    }
                    finally
                    {
                        serviceHost.Close();
                    }
                },
                this.cancellationTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default)
                .ContinueWith(T => this.WriteMessage("Host stopped."), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
