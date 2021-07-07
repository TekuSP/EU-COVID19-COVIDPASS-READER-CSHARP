using System;

namespace CovidPassTestReader.CovidPass
{
    /// <summary>
    /// Information about used vaccine on user
    /// </summary>
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
        public string AgentTargeted { get; set; }
        /// <summary>
        /// vaccine or prophylaxis
        /// </summary>
        public string Vaccine { get; set; }
        /// <summary>
        /// ISO8601 complete date: Date of Vaccination
        /// </summary>
        public DateTime DateOfVaccination { get; set; }
        /// <summary>
        /// Country of Vaccination
        /// </summary>
        public Json.ValueSetValue CountryCode { get; set; }
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
