namespace WindowsServiceHosting
{
    using System.ServiceProcess;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            RunService();
        }

        private static void RunService()
        {
            ServiceBase.Run(new NortwindWCFServicesHost());
        }
    }
}
