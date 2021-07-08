//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
namespace CovidPassReader.CovidPass
{
    public class Color
    {
        #region Public Properties

        public byte B { get; set; }
        public byte G { get; set; }
        public byte R { get; set; }
        public System.Drawing.Color RGBColor => System.Drawing.Color.FromArgb(R, G, B);

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return RGBColor.ToString();
        }

        #endregion Public Methods
    }
}