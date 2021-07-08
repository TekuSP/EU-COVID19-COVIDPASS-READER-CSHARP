//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using System;
using System.Runtime.Serialization;

namespace CovidPassReader.CovidPass
{
    /// <summary>
    /// Informations about user
    /// </summary>
    [DataContract]
    public record UserInformation
    {
        /// <summary>
        /// The forename(s) of the person addressed in the certificate
        /// </summary>
        /// <example>Jiřina-Maria Alena</example>
        public string Name { get; set; }
        /// <summary>
        /// The forename(s) of the person, transliterated ICAO 9303
        /// </summary>
        /// <example>JIRINA<MARIA<ALENA</example>
        public string StandardisedName { get; set; }
        /// <summary>
        /// The surname or primary name(s) of the person addressed in the certificate
        /// </summary>
        /// <example>d'Červenková Panklová</example>
        public string Surname { get; set; }
        /// <summary>
        /// The surname(s) of the person, transliterated ICAO 9303
        /// </summary>
        /// <example>DCERVENKOVA<PANKLOVA</example>
        public string StandardisedSurname { get; set; }
        /// <summary>
        /// Date of Birth of the person addressed in the DCC. ISO 8601 date format restricted to range 1900-2099 or empty
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}