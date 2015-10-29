namespace WindowsServiceHosting
{
    using System.ServiceProcess;

    public partial class NortwindWCFServicesHost : ServiceBase
    {
        public NortwindWCFServicesHost()
        {
            this.InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
