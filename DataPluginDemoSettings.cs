using FlagEventEmitter.client;

namespace FlagEventEmitter
{
    /// <summary>
    /// Settings class, make sure it can be correctly serialized using JSON.net
    /// </summary>
    public class DataPluginDemoSettings
    {
        public string deviceName = "THE_NAME_OF_THE DEVICE";
        public string goveeApiKey = "YOUR_GOVEE_API_KEY";
        public DeviceType deviceType;

        public DataPluginDemoSettings(string deviceName, string goveeApiKey, DeviceType deviceType)
        {
            this.deviceName = deviceName;   
            this.goveeApiKey = goveeApiKey;
            this.deviceType = deviceType;
        }
    }
}