namespace WindowsServiceHosting
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
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
            var hosts = ServiceHostsFactory.Hosts;

            var serviceHosts = hosts as IList<ServiceHost> ?? hosts.ToArray();

            if (hosts == null || !serviceHosts.Any())
            {
                return;
            }

            foreach (var serviceHost in serviceHosts)
            {
                var host = serviceHost;

                Task.Factory.StartNew(
                    () =>
                        {
                            try
                            {
                                host.Open();

                                this.WriteMessage(string.Format("Host for {0} is running", host.GetType().Name));

                                while (true)
                                {
                                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                                }
                            }
                            finally
                            {
                                host.Close();
                            }
                        },
                    this.cancellationTokenSource.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default)
                    .ContinueWith(
                        T =>
                            {
                                if (T.Exception != null)
                                {
                                    var exception = T.Exception.Flatten().InnerException;
                                    this.messagesContainer.AddMessage(exception.Message);
                                }
                            },
                        TaskContinuationOptions.OnlyOnFaulted)
                    .ContinueWith(
                        T => this.WriteMessage("Host stopped."),
                        TaskContinuationOptions.OnlyOnCanceled);
            }
        }
    }
}
