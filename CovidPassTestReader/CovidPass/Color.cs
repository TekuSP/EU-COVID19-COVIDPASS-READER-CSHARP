using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPassReader.CovidPass
{
    public class Color
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public System.Drawing.Color RGBColor => System.Drawing.Color.FromArgb(R,G,B);
        public override string ToString()
        {
            return RGBColor.ToString();
        }
    }
}
