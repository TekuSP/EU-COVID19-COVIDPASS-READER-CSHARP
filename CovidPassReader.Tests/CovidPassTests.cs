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
            //Test vaccine information
            Assert.AreEqual(mockVaccine.AgentTargeted.Display, testData.VaccineInformation.AgentTargeted.Display, "AgentTargeted are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.CertificationIssuer, testData.VaccineInformation.CertificationIssuer, "CertificationIssuer are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.CountryOfTest.Display, testData.VaccineInformation.CountryOfTest.Display, "CountryOfTest are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.DateOfVaccination, testData.VaccineInformation.DateOfVaccination, "DateOfVaccination are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.DoseNumber, testData.VaccineInformation.DoseNumber, "DoseNumber are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.Manufacturer.Display, testData.VaccineInformation.Manufacturer.Display, "Manufacturer are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.TotalDoses, testData.VaccineInformation.TotalDoses, "TotalDoses are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.UVCI, testData.VaccineInformation.UVCI, "UVCI are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.VaccineName.Display, testData.VaccineInformation.VaccineName.Display, "VaccineName are different! Vaccine data loaded incorrectly from QR code!");
            Assert.AreEqual(mockVaccine.VaccineProphylaxis.Display, testData.VaccineInformation.VaccineProphylaxis.Display, "VaccineProphylaxis are different! Vaccine data loaded incorrectly from QR code!");
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
            //Test test information
            Assert.AreEqual(mockTest.AgentTargeted.Display, testData.TestInformation.AgentTargeted.Display, "AgentTargeted are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.CertificationIssuer, testData.TestInformation.CertificationIssuer, "CertificationIssuer are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.CountryOfTest.Display, testData.TestInformation.CountryOfTest.Display, "CountryOfTest are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.DateOfCollection, testData.TestInformation.DateOfCollection, "DateOfCollection are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.NAATestName, testData.TestInformation.NAATestName, "NAATestName are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.Manufacturer.Display, testData.TestInformation.Manufacturer.Display, "Manufacturer are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.TestingCenter, testData.TestInformation.TestingCenter, "TestingCenter are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.UVCI, testData.TestInformation.UVCI, "UVCI are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.TestResult.Display, testData.TestInformation.TestResult.Display, "TestResult are different! Test data loaded incorrectly from QR code!");
            Assert.AreEqual(mockTest.TestType.Display, testData.TestInformation.TestType.Display, "TestType are different! Test data loaded incorrectly from QR code!");
        }

        #endregion Public Methods
    }
}