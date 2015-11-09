namespace WindowsServiceHosting
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new Container();

            this.serviceProcessInstaller = new ServiceProcessInstaller();
            this.serviceInstaller = new ServiceInstaller();

            // serviceProcessInstaller
            this.serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;

            // serviceInstaller
            this.serviceInstaller.DisplayName = Program.ServiceName;
            this.serviceInstaller.ServiceName = Program.ServiceName;
            this.serviceInstaller.Description = Program.ServiceDescription;
            this.serviceInstaller.StartType = ServiceStartMode.Manual;

            // ProjectInstaller
            this.Installers.AddRange(new Installer[]
            {
                this.serviceProcessInstaller,
                this.serviceInstaller
            });
        }
    }
}
