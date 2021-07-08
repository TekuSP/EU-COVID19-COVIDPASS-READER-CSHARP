using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CovidPassReader.Tests
{
    [TestClass]
    public class QRCodeTests
    {
        [TestMethod]
        public void ReadValidQRCode()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidVaccine.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");
        }
        [TestMethod]
        public void ReadInValidQRCode()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\invalid.png");
            Assert.IsFalse(result != null, "QR Code is wrongly read!");
        }
    }
}
