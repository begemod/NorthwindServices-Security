namespace WindowsServiceHosting
{
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class NortwindWCFServicesHost : ServiceBase
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public NortwindWCFServicesHost()
        {
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

        private void ConfigureHosts()
        {
            var hosts = ServiceHostsFactory.GetHosts();
            
            var serviceHosts = hosts as IList<ServiceHost> ?? hosts.ToArray();
            
            if (hosts == null || !serviceHosts.Any())
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var serviceHost in serviceHosts)
            {
                var host = serviceHost;

                var serviceTask = new Task(
                    () =>
                        {
                            using (host)
                            {
                                host.Open();
                                cancellationTokenSource.Token.ThrowIfCancellationRequested();
                            }
                        },
                    this.cancellationTokenSource.Token);

                tasks.Add(serviceTask);
            }

            Task.WaitAll(tasks.ToArray(), this.cancellationTokenSource.Token);
        }
    }
}
