using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace CovidPassTestReader.CovidPass
{
    /// <summary>
    /// COVID-19 Certification data, containing vacinnes and tests done
    /// </summary>
    public class PassData
    {
        //Based on schema: https://github.com/ehn-dcc-development/ehn-dcc-schema/blob/release/1.3.0/DCC.combined-schema.json
        /// <summary>
        /// Is it vaccination proof, or test proof, or recovery proof?
        /// </summary>
        public string PassportType { get; set; }
        /// <summary>
        /// Passport description, right now based on EU recommendation text
        /// </summary>
        public string PassportDescription { get; set; } =
@"This certificate is not a travel document. The scientific evidence
on COVID-19 vaccination, testing and recovery continues to
evolve, also in view of new variants of concern of the virus.
Before traveling, please check the applicable public health
measures and related restrictions applied at the point of
destination.
Relevant information can be found here:
https://reopen.europa.eu/en

This certificate was verified/generated using third party application.
Relevant information to software can be found here:
https://github.com/TekuSP/EU-COVID19-COVIDPASS-READER-CSHARP";
        /// <summary>
        /// Is certificate valid by date?
        /// </summary>
        public bool IsValid => DateTime.Now < Payload.ValidTo && DateTime.Now > Payload.ValidFrom;
        /// <summary>
        /// Data Payload we are basing on (Usually from QR code)
        /// </summary>
        public Payload Payload { get; set; }
        /// <summary>
        /// Database data from EU
        /// </summary>
        public OnlineDataValueSets DataValueSets { get; set; }
        /// <summary>
        /// Information about user
        /// </summary>
        public UserInformation UserInformation { get; set; }
        /// <summary>
        /// Information about vaccine
        /// </summary>
        public VaccineInformation VaccineInformation { get; set; }
        //TODO: ADD TEST INFORMATION DATA

        /// <summary>
        /// Constructs Certification data based on Covid Pass data
        /// </summary>
        /// <param name="data">Incoming data from QR code</param>
        /// <exception cref="ArgumentException">Throws Argument Exception when incorrect data are passed.</exception>
        public PassData(string data)
        {
            //Step 1: Remove header
            if (data.StartsWith("HC1")) //Is this Covid pass?
            {
                data = data.Substring(3); //Remove HC1
                if (data.StartsWith(':'))
                    data = data.Substring(1); //Remove : if present

            }
            else
            {
                throw new ArgumentException("Not valid Covid Pass data! Did not detect header!");
            }

            //Step 2: BASE 45 Decode
            data = data.FromBase45(); //Decode Base45

            //STEP 3: ZLib Decode
            byte[] resultData;
            if ((byte)data[0] == 0x78) //It's valid ZLib
            {
                using MemoryStream ms = new MemoryStream(data.Select(x => (byte)x).ToArray()); //Get bytes
                ms.Seek(2, SeekOrigin.Begin); //Seek over header.... Microsoft bugs...
                using DeflateStream inflater = new DeflateStream(ms, CompressionMode.Decompress); //Decompress
                using MemoryStream temp = new MemoryStream(); //Temp memoryStream
                inflater.CopyTo(temp); //Copy to our temp
                resultData = temp.ToArray(); //Get it back
            }
            else
            {
                throw new ArgumentException("Not valid Covid Pass data! Zlib decompression failed.");
            }

            Payload = new Payload(resultData); //Init Payload
            DataValueSets = new OnlineDataValueSets(); //Init datasets

            var dict = Tools.ReadDict(Payload.DecodedData); //Get our data dict

            // Get Vaccine, Name and Date of Birth information in raw format

            Dictionary<object, object> vaccinationInformation = null;
            Dictionary<object, object> testInformation = null;
            Dictionary<object, object> recoveryInformation = null;


            if (dict.ContainsKey("v")) //Are there any vaccines? There can be only one.
                vaccinationInformation = Tools.ReadDict((ReadOnlyMemory<byte>)dict["v"]);

            if (dict.ContainsKey("t")) //Are there any tests? There can be only one.
                testInformation = Tools.ReadDict((ReadOnlyMemory<byte>)dict["t"]);

            if (dict.ContainsKey("r")) //Are there any recoveries? There can be only one.
                recoveryInformation = Tools.ReadDict((ReadOnlyMemory<byte>)dict["r"]);

            var nameInformation = Tools.ReadDict((ReadOnlyMemory<byte>)dict["nam"]); //If we don't have name, then its rightful to crash
            var dateOfBirthInformation = DateTime.Parse(dict["dob"].ToString()); //Same here

            UserInformation = new UserInformation() { DateOfBirth = dateOfBirthInformation, Name = nameInformation["gn"].ToString(), StandardisedName = nameInformation["gnt"].ToString(), Surname = nameInformation["fn"].ToString(), StandardisedSurname = nameInformation["fnt"].ToString() }; //Init user information
            if (vaccinationInformation != null)
            {
                VaccineInformation = new VaccineInformation() { CertificationIssuer = vaccinationInformation["is"].ToString(), DateOfVaccination = DateTime.Parse(vaccinationInformation["dt"].ToString()), DoseNumber = vaccinationInformation["dn"].ToString(), TotalDoses = vaccinationInformation["sd"].ToString(), UVCI = vaccinationInformation["ci"].ToString() }; //Init vaccine information
                VaccineInformation.CountryCode = DataValueSets.CountryCodes.ValueSetValues[vaccinationInformation["co"].ToString()]; //Init Country
                VaccineInformation.Manufacturer = DataValueSets.ManufacturersVaccines.ValueSetValues[vaccinationInformation["ma"].ToString()]; //Init Manufacturer
                VaccineInformation.VaccineName = DataValueSets.VaccineMedicalProducts.ValueSetValues[vaccinationInformation["mp"].ToString()]; //Init Vaccine Name
                VaccineInformation.VaccineProphylaxis = DataValueSets.VaccineProphylaxis.ValueSetValues[vaccinationInformation["vp"].ToString()];
                VaccineInformation.AgentTargeted = DataValueSets.AgentsTargeted.ValueSetValues[vaccinationInformation["tg"].ToString()];
            }
        }
    }
}
