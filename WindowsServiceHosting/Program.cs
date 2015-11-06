namespace WindowsServiceHosting
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class Program
    {
        public const string ServiceName = "NortwindWCFServicesHost";

        private static NortwindWCFServiceHostsManager servicesHost;
        private static ManualResetEvent msResetEvent = new ManualResetEvent(false);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower().Contains("commandline"))
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

                servicesHost = new NortwindWCFServiceHostsManager(new ConsoleMessagesContainer());
                servicesHost.StartServer();

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
                msResetEvent.WaitOne();
            }
        }

        private static void ConsoleCancelKeyPressEventHandler(object sender, ConsoleCancelEventArgs e)
        {
            if (servicesHost != null)
            {
                servicesHost.StopServer();

                Console.WriteLine("Server stopped. Press Enter to exit.");
                Console.ReadLine();
            }

            msResetEvent.Set();
        }

        private static void RunService()
        {
            ////ServiceBase.Run(new NortwindWCFServicesHost());
        }
    }
}
