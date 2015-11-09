namespace WindowsServiceHosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using WindowsServiceHosting.Loggers;

    public class NortwindWCFServiceHostsManager
    {
        private readonly List<ServiceHostRunner> hostRunners = new List<ServiceHostRunner>();

        private readonly ILogger messagesContainer;

        public NortwindWCFServiceHostsManager(ILogger messagesContainer)
        {
            this.messagesContainer = messagesContainer;
        }

        public bool Start()
        {
            try
            {
                this.StartServer();
                this.WriteMessage(string.Format("Service started at {0}.", DateTime.Now));
            }
            catch (Exception exception)
            {
                this.WriteError("An error occured on attempting to start the service.");
                this.WriteError(exception.ToString());
                return false;
            }

            return true;
        }
        
        public bool Stop()
        {
            try
            {
                this.StopServer();
                this.WriteMessage(string.Format("Service stopped at {0}.", DateTime.Now));
            }
            catch (Exception exception)
            {
                this.WriteError("An error occured on attempting to stop the service.");
                this.WriteError(exception.ToString());
                return false;
            }

            return true;
        }

        private void StartServer()
        {
            this.RunAllHosts();
        }

        private void StopServer()
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
