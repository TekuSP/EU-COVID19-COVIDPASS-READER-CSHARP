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
            var testData = new CovidPass.PassData(result);
            Assert.AreEqual(CovidPassReader.CovidPass.CovidPassType.Vaccine, testData.PassportType, "CovidPass wrongly read!");
        }
        [TestMethod]
        public void ReadCovidTest_Test()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidTest.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");
            var testData = new CovidPass.PassData(result);
            Assert.AreEqual(CovidPassReader.CovidPass.CovidPassType.TestProof, testData.PassportType, "CovidPass wrongly read!");
        }
    }
}
