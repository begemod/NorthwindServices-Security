namespace WindowsServiceHosting
{
    using System;
    using System.ServiceProcess;
    using System.Threading;

    public static class Program
    {
        public const string ServiceName = "NortwindWCFServicesHost";

        private static NortwindWCFServicesHost servicesHost;
        private static ManualResetEvent msResetEvent = new ManualResetEvent(false);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].Contains("commandline"))
            {
                RunAsCommandLine();
            }
            else
            {
                RunService();
            }
        }

        private static void RunAsCommandLine()
        {
            try
            {
                Console.CancelKeyPress += ConsoleCancelKeyPress;

                servicesHost = new NortwindWCFServicesHost();
                servicesHost.StartServer();

                Console.WriteLine("Server started. Press Ctrl+C to shutdown the server...");

                msResetEvent.WaitOne();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine();
                Console.WriteLine(exception.StackTrace);
            }
        }

        private static void ConsoleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (servicesHost != null)
            {
                servicesHost.StopServer();
                Console.WriteLine("Server stopped...");
            }

            msResetEvent.Set();
        }

        private static void RunService()
        {
            ////ServiceBase.Run(new NortwindWCFServicesHost());
        }
    }
}
