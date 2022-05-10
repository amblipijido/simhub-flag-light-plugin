using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagEventEmitter
{
    public class Data
    {
        public List<Device> devices { get; set; }

        public Data(List<Device> devices)
        {
            this.devices = devices;
        }
    }
}
