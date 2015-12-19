namespace Tests.BaseTests
{
    using System.Collections.Generic;
    using System.ServiceModel;

    public class ChannelFactoriesProvider
    {
        private readonly Dictionary<string, ChannelFactory> _channelFactoriesCollection = new Dictionary<string, ChannelFactory>();

        public ChannelFactory<T> GetChannelFactory<T>(string endpointConfigurationName)
            where T : IClientChannel
        {
            ChannelFactory<T> channelFactory;

            if (!_channelFactoriesCollection.ContainsKey(endpointConfigurationName))
            {
                channelFactory = new ChannelFactory<T>(endpointConfigurationName);
                _channelFactoriesCollection.Add(endpointConfigurationName, channelFactory);
            }
            else
            {
                channelFactory = _channelFactoriesCollection[endpointConfigurationName] as ChannelFactory<T>;

                if (channelFactory == null || channelFactory.State == CommunicationState.Closing || channelFactory.State == CommunicationState.Closed)
                {
                    channelFactory = new ChannelFactory<T>(endpointConfigurationName);
                    _channelFactoriesCollection[endpointConfigurationName] = channelFactory;
                }
            }

            return channelFactory;
        }

        public void CloseAllChannels()
        {
            foreach (var channelFactory in _channelFactoriesCollection.Values)
            {
                if (channelFactory.State == CommunicationState.Faulted)
                {
                    channelFactory.Abort();
                }

                channelFactory.Close();
            }
        }
    }
}
