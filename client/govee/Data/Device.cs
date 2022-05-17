using System.Collections.Generic;

namespace FlagEventEmitter.client.govee.data
{
    public class Device
    {
        public string device { get; set; }
        public string model { get; set; }
        public string deviceName { get; set; }
        public bool controllable { get; set; }
        public bool retrievable { get; set; }
        public List<string> supportCmds { get; set; }
        public Properties properties { get; set; }

        public Device(string device, string model, string deviceName, bool controllable, bool retrievable, List<string> supportCmds, Properties properties)
        {
            this.device = device;
            this.model = model;
            this.deviceName = deviceName;
            this.controllable = controllable;
            this.retrievable = retrievable;
            this.supportCmds = supportCmds;
            this.properties = properties;
        }


    }
}
