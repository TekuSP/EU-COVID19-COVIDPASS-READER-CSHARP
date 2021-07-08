using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPassReader.Tests
{
    [TestClass]
    public class CovidPassTests
    {
        [TestMethod]
        public void ReadCovidVaccineTest()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidVaccine.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");

        }
        [TestMethod]
        public void ReadCovidTest_Test()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidTest.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");

        }
    }
}
