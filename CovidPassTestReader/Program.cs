using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using BarcodeLib;


using ZXing;
using ZXing.Common;

namespace CovidPassReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            var passData = new CovidPass.PassData(QRCodeReadHelper.ReadQRCode("")); //process covid data

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
