//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using System;
using System.Runtime.Serialization;

namespace CovidPassReader.CovidPass
{
    /// <summary>
    /// Information about used vaccine on user
    /// </summary>
    [DataContract]
    public record VaccineInformation
    {
        /// <summary>
        /// Dose Number
        /// </summary>
        public string DoseNumber { get; set; }
        /// <summary>
        /// Total Series of Doses
        /// </summary>
        public string TotalDoses { get; set; }
        /// <summary>
        /// Did user have all Doses?
        /// </summary>
        public bool AllDosesDone => DoseNumber == TotalDoses;
        /// <summary>
        /// disease or agent targeted
        /// </summary>
        public Json.ValueSetValue AgentTargeted { get; set; }
        /// <summary>
        /// vaccine or prophylaxis
        /// </summary>
        public Json.ValueSetValue VaccineProphylaxis { get; set; }
        /// <summary>
        /// ISO8601 complete date: Date of Vaccination
        /// </summary>
        public DateTime DateOfVaccination { get; set; }
        /// <summary>
        /// Country of Vaccination
        /// </summary>
        public Json.ValueSetValue CountryOfTest { get; set; }
        /// <summary>
        /// vaccine medicinal product
        /// </summary>
        public Json.ValueSetValue VaccineName { get; set; }
        /// <summary>
        /// Marketing Authorization Holder - if no MAH present, then manufacturer
        /// </summary>
        public Json.ValueSetValue Manufacturer { get; set; }
        /// <summary>
        /// Certificate Issuer
        /// </summary>
        public string CertificationIssuer { get; set; }
        /// <summary>
        /// Unique Certificate Identifier: UVCI
        /// </summary>
        public string UVCI { get; set; }
    }
}