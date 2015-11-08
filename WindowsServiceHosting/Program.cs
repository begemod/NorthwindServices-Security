namespace WindowsServiceHosting
{
    using System;
    using System.Linq;
    using System.Threading;
    using Topshelf;

    public static class Program
    {
        private const string ServiceName = "NortwindWCFServicesHost";
        private const string ServiceDescription = "Northwind WCF services host";

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
                UserInteractiveResetEvent.WaitOne();
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

            UserInteractiveResetEvent.Set();
        }

        private static void RunService()
        {
            try
            {
                var host = HostFactory.New(x =>
                        {
                            x.Service<NortwindWCFServiceHostsManager>(
                                s =>
                                {
                                    s.ConstructUsing(n => new NortwindWCFServiceHostsManager(new ConsoleLogger()));
                                    s.WhenStarted(n => n.Start());
                                    s.WhenStopped(n => n.Stop());
                                });

                            x.SetServiceName(ServiceName);
                            x.SetDisplayName(ServiceName);
                            x.SetDescription(ServiceDescription);
                            x.SetInstanceName(ServiceName);

                            x.StartAutomatically();
                            x.RunAsLocalService();
                        });

                host.Run();
            }
            catch (TopshelfException exception)
            {
                Console.WriteLine("There are some errors on starting the service:");
                Console.WriteLine("Press Enter to exit.");
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }

            ////ServiceBase.Run(new NortwindWCFServicesHost());
        }
    }
}
