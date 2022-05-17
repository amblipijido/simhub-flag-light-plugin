
namespace FlagEventEmitter.client.govee.data
{
    public class GoveeRgbComand : GoveeComand
    {
        public RgbValue value { get; set; }

        public GoveeRgbComand(string name, RgbValue value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
