namespace Tests.BaseTests
{
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestsEnviroment
    {
        private static readonly ChannelFactoriesProvider ChannelFactories = new ChannelFactoriesProvider();

        public static ChannelFactory<T> GetChannelFactory<T>(string endpointConfigurationName)
            where T : IClientChannel
        {
            return ChannelFactories.GetChannelFactory<T>(endpointConfigurationName);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            ChannelFactories.CloseAllChannels();
        }
    }
}
