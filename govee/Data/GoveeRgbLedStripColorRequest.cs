

namespace FlagEventEmitter
{
    public class GoveeRgbLedStripColorRequest
    {
        public string device { get; set; }
        public string model { get; set; }
        public GoveeComand cmd { get; set; }

        public GoveeRgbLedStripColorRequest(string device, string model, GoveeComand cmd)
        {
            this.device = device;
            this.model = model;
            this.cmd = cmd;
        }
    }
}
