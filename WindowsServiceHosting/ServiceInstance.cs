namespace WindowsServiceHosting
{
    using System.ServiceProcess;
    using WindowsServiceHosting.Loggers;

    public partial class ServiceInstance : ServiceBase
    {
        private readonly NortwindWCFServiceHostsManager manager;

        public ServiceInstance()
        {
            this.InitializeComponent();
            this.manager = new NortwindWCFServiceHostsManager(new WindowsEventLogger());
        }

        protected override void OnStart(string[] args)
        {
            if (this.manager.Start())
            {
                base.OnStart(args);
            }
        }

        protected override void OnStop()
        {
            this.manager.Stop();
            base.OnStop();
        }
    }
}
