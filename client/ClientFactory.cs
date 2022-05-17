using System;

namespace FlagEventEmitter.client
{
    internal class ClientFactory
    {
        DataPluginDemoSettings settings;

        public ClientFactory(DataPluginDemoSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Create a new Client instance based on the settings
        /// </summary>
        /// <returns>The new Client instance</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DeviceClient generateClient()
        {
            switch(settings.deviceType)
            {
                case DeviceType.GOVEE:
                    return new GoveeClient();                     
                case DeviceType.ARDUINO:
                    throw new NotImplementedException();
                case DeviceType.PHILIPS_HUE:
                    throw new NotImplementedException();
                default: 
                    throw new ArgumentOutOfRangeException(nameof(settings.deviceType));

            }
        }
    }
}
