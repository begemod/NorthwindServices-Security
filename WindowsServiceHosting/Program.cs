namespace WindowsServiceHosting
{
    using System;
    using System.ServiceProcess;
    using System.Threading;
    using WindowsServiceHosting.Loggers;

    public static class Program
    {
        public const string ServiceName = "NortwindWCFServicesHost";
        public const string ServiceDescription = "Northwind WCF services host";

        private static readonly ManualResetEvent UserInteractiveResetEvent = new ManualResetEvent(false);
        private static NortwindWCFServiceHostsManager servicesHost;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            if (Environment.UserInteractive)
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
                Console.CancelKeyPress += ConsoleCancelKeyPressEventHandler;

                servicesHost = new NortwindWCFServiceHostsManager(new ConsoleLogger());
                servicesHost.Start();

                Console.WriteLine("Server started. Press Ctrl+C to shutdown the server...");
                Console.WriteLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine();
                Console.WriteLine(exception.StackTrace);
            }
            finally
            {
                UserInteractiveResetEvent.WaitOne();
            }
        }

        private static void ConsoleCancelKeyPressEventHandler(object sender, ConsoleCancelEventArgs e)
        {
            if (servicesHost != null)
            {
                servicesHost.Stop();

                Console.WriteLine("Server stopped. Press Enter to exit.");
                Console.ReadLine();
            }

            UserInteractiveResetEvent.Set();
        }

        private static void RunService()
        {
            var servicesToRun = new ServiceBase[]
            {
                new ServiceInstance()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
