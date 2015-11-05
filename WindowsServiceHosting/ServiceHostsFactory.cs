namespace WindowsServiceHosting
{
    using System.Collections.Generic;
    using System.ServiceModel;

    internal static class ServiceHostsFactory
    {
        public static IEnumerable<ServiceHost> GetHosts()
        {
            return new List<ServiceHost>();
        }
    }
}