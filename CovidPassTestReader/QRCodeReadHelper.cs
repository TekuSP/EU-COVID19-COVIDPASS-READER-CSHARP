//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace CovidPassReader
{
    public static class QRCodeReadHelper
    {
        #region Public Methods

        /// <summary>
        /// Reads QR Code from an image
        /// </summary>
        /// <param name="path">Path to image read from</param>
        /// <returns>Returns null if code is invalid, or returns data</returns>
        public static string ReadQRCode(string path)
        {
            return ReadQRCode((Image)new Bitmap(path));
        }

        /// <summary>
        /// Reads QR Code from an image
        /// </summary>
        /// <param name="image">Image to read from</param>
        /// <returns>Returns null if code is invalid, or returns data</returns>
        public static string ReadQRCode(Image image)
        {
            try
            {
                var gFilter = new GaussianBlur(1);
                var processedImage = gFilter.ProcessImage(image);
                BarcodeReader reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls))
                {
                    AutoRotate = true,
                };
                reader.Options.TryHarder = true;
                reader.Options.PureBarcode = false;
                reader.AutoRotate = true;
                reader.Options.TryInverted = true;
                reader.Options.PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.QR_CODE
                };

                var result = reader.Decode((Bitmap)image);
                if (result == null)
                    return null; //Nothing read
                return result.Text; //Correct QR code
            }
            catch (Exception ex)
            {
                Trace.TraceError("Unable to read QR code!");
                Trace.TraceError(ex.ToString());
                return null;
            }
        }

        #endregion Public Methods
    }
}