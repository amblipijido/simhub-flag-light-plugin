

namespace FlagEventEmitter.client.govee.data
{
    public class GoveeSwithcComand : GoveeComand
    {
        public string value { get; set; }

        public GoveeSwithcComand(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
