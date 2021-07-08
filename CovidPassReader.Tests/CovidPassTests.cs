//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Xml.Serialization;

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

        [TestMethod]
        public void ReadCovidVaccineDataTest()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidVaccine.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");
            var testData = new CovidPass.PassData(result);
            Assert.AreEqual(CovidPassReader.CovidPass.CovidPassType.Vaccine, testData.PassportType, "CovidPass wrongly read!");
           
            XmlSerializer userSerializer = new XmlSerializer(typeof(CovidPass.UserInformation)); //Read mock data
            XmlSerializer vaccineSerializer = new XmlSerializer(typeof(CovidPass.VaccineInformation)); //Read mock data

            FileStream fs = new FileStream("TestData\\Results\\Vaccine\\user.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Read mock user
            CovidPass.UserInformation mockUser = (CovidPass.UserInformation)userSerializer.Deserialize(fs);
            fs.Close();

            fs = new FileStream("TestData\\Results\\Vaccine\\vaccine.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Read mock vaccine
            CovidPass.VaccineInformation mockVaccine = (CovidPass.VaccineInformation)vaccineSerializer.Deserialize(fs);
            fs.Close();

            Assert.AreEqual(mockUser, testData.UserInformation, "User data loaded incorrectly from QR code!");
            //Assert.AreEqual(mockVaccine, testData.VaccineInformation, "User data, vaccine, loaded incorrectly from QR code!"); TODO: FIX MOCK
        }

        [TestMethod]
        public void ReadCovidTest_DataTest()
        {
            var result = QRCodeReadHelper.ReadQRCode("TestData\\covidTest.png");
            Assert.IsTrue(result != null, "QR Code is wrongly read!");
            var testData = new CovidPass.PassData(result);
            Assert.AreEqual(CovidPassReader.CovidPass.CovidPassType.TestProof, testData.PassportType, "CovidPass wrongly read!");

            XmlSerializer userSerializer = new XmlSerializer(typeof(CovidPass.UserInformation)); //Read mock data
            XmlSerializer testSerialzer = new XmlSerializer(typeof(CovidPass.TestInformation)); //Read mock data

            FileStream fs = new FileStream("TestData\\Results\\Test\\user.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Read mock user
            CovidPass.UserInformation mockUser = (CovidPass.UserInformation)userSerializer.Deserialize(fs);
            fs.Close();

            fs = new FileStream("TestData\\Results\\Test\\test.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Read mock vaccine
            CovidPass.TestInformation mockTest = (CovidPass.TestInformation)testSerialzer.Deserialize(fs);
            fs.Close();

            Assert.AreEqual(mockUser, testData.UserInformation, "User data loaded incorrectly from QR code!");
            //Assert.AreEqual(mockTest, testData.TestInformation, "User data, test, loaded incorrectly from QR code!"); TODO: FIX MOCK
        }

        #endregion Public Methods
    }
}