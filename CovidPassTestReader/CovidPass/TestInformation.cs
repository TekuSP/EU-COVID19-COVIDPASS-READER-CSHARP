//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using System;
using System.Runtime.Serialization;

namespace CovidPassReader.CovidPass
{
    /// <summary>
    /// Information about used test on user
    /// </summary>
    [DataContract]
    public record TestInformation
    {
        /// <summary>
        /// disease or agent targeted
        /// </summary>
        public Json.ValueSetValue AgentTargeted { get; set; }
        /// <summary>
        /// Type of Test
        /// </summary>
        public Json.ValueSetValue TestType { get; set; }
        /// <summary>
        /// NAA Test Name
        /// </summary>
        public string NAATestName { get; set; }
        /// <summary>
        /// RAT Test name and manufacturer
        /// </summary>
        public Json.ValueSetValue Manufacturer { get; set; }
        /// <summary>
        /// Date/Time of Sample Collection
        /// </summary>
        public DateTime DateOfCollection { get; set; }
        /// <summary>
        /// Test Result
        /// </summary>
        public Json.ValueSetValue TestResult { get; set; }
        /// <summary>
        /// Testing Centre
        /// </summary>
        public string TestingCenter { get; set; }
        /// <summary>
        /// Country of Test
        /// </summary>
        public Json.ValueSetValue CountryOfTest { get; set; }
        /// <summary>
        /// Certificate Issuer
        /// </summary>
        public string CertificationIssuer { get; set; }
        /// <summary>
        /// Unique Certificate Identifier, UVCI
        /// </summary>
        public string UVCI { get; set; }
    }
}