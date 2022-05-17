using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagEventEmitter.service.data
{
    public class RgbValue
    {
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }

        public RgbValue(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}
