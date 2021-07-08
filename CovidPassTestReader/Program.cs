using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using BarcodeLib;


using ZXing;
using ZXing.Common;

namespace CovidPassTestReader
{
    class Program
    {
        static void Main(string[] args)
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
            var gFilter = new GaussianBlur(2);
            var processedImage = gFilter.ProcessImage((Image)new Bitmap("TestData\\covidTest.jpg")); //REPLACE IT WITH YOUR TEST

            var result =  reader.Decode((Bitmap)processedImage);
            Console.WriteLine();
            var passData = new CovidPass.PassData(result.Text); //process covid data

            Console.WriteLine($"Read user: {passData.UserInformation.Name}, {passData.UserInformation.Surname}");
            Console.WriteLine($"Born: {passData.UserInformation.DateOfBirth.ToLongDateString()}");
            Console.WriteLine($"Vaccinated? {passData.VaccineInformation != null}");
            if (passData.VaccineInformation == null)
                return; //END
            Console.WriteLine($"Dose / Total Doses: {passData.VaccineInformation.DoseNumber} / {passData.VaccineInformation.TotalDoses}");
            Console.WriteLine($"User did all doses? {passData.VaccineInformation.AllDosesDone}");
            Console.WriteLine($"Vaccine used: {passData.VaccineInformation.VaccineName.Display} from {passData.VaccineInformation.Manufacturer.Display}");
            Console.WriteLine($"Certified by: {passData.VaccineInformation.CertificationIssuer} in Country: {passData.VaccineInformation.CountryCode.Display}");
        }
    }
}
