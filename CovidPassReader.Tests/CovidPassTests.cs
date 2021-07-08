//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CovidPassReader.Tests
{
    [TestClass]
    public class CovidPassTests
    {
        #region Public Methods

        [TestMethod]
        public void ReadCovidTest_Test()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidTest.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");
            var testData = new CovidPass.PassData(result);
            Assert.AreEqual(CovidPassReader.CovidPass.CovidPassType.TestProof, testData.PassportType, "CovidPass wrongly read!");
        }

        [TestMethod]
        public void ReadCovidVaccineTest()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidVaccine.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");
            var testData = new CovidPass.PassData(result);
            Assert.AreEqual(CovidPassReader.CovidPass.CovidPassType.Vaccine, testData.PassportType, "CovidPass wrongly read!");
        }

        #endregion Public Methods
    }
}