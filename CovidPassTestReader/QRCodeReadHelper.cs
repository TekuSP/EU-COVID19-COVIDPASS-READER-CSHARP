using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZXing;
using ZXing.Common;

namespace CovidPassReader
{
    public static class QRCodeReadHelper
    {
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
                BarcodeReader reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls))
                {
                    AutoRotate = true
                };
                reader.Options.TryHarder = true;
                reader.Options.PureBarcode = false;
                reader.AutoRotate = true;
                reader.TryInverted = true;
                reader.Options.PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.QR_CODE
                };
                var gFilter = new GaussianBlur(1);
                var processedImage = gFilter.ProcessImage(image);

                var result = reader.Decode((Bitmap)processedImage);
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
    }
}
