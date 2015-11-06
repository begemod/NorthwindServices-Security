namespace WindowsServiceHosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class NortwindWCFServiceHostsManager : ServiceBase
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly List<ServiceHostRunner> hostRunners = new List<ServiceHostRunner>();

        private readonly IMessagesContainer messagesContainer;

        public NortwindWCFServiceHostsManager(IMessagesContainer messagesContainer)
        {
            this.messagesContainer = messagesContainer;
            this.InitializeComponent();
        }

        public void StartServer()
        {
            this.RunAllHosts();
        }

        public void StopServer()
        {
            if (this.hostRunners.Any())
            {
                this.hostRunners.ForEach(h => h.Stop());

                while (this.hostRunners.Any(h => h.State == ServiceHostRunnerState.Running))
                {
                    Thread.Sleep(100);
                }
            }
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

        private void WriteError(string message)
        {
            if (this.messagesContainer != null)
            {
                this.messagesContainer.AddError(message);
            }
        }

        private void RunAllHosts()
        {
            var hosts = ServiceHostsFactory.Hosts.ToList();

            if (!hosts.Any())
            {
                return;
            }

            this.hostRunners.AddRange(hosts.Select(h => new ServiceHostRunner(h, this.messagesContainer)));

            this.hostRunners.ForEach(hr => hr.Start());
        }
    }
}
