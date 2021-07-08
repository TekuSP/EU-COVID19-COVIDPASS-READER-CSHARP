//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CovidPassReader.Tests
{
    [TestClass]
    public class QRCodeTests
    {
        #region Public Methods

        [TestMethod]
        public void ReadInValidQRCode()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\invalid.png");
            Assert.IsFalse(result != null, "QR Code is wrongly read!");
        }

        [TestMethod]
        public void ReadValidQRCode()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidVaccine.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");
        }

        #endregion Public Methods
    }
}