﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CovidPassTestReader.CovidPass.Json;
//
//    var medicalProducts = MedicalProducts.FromJson(jsonString);

namespace CovidPassTestReader.CovidPass.Json
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MedicalProducts
    {
        [JsonProperty("valueSetId")]
        public string ValueSetId { get; set; }

        [JsonProperty("valueSetDate")]
        public DateTimeOffset ValueSetDate { get; set; }

        [JsonProperty("valueSetValues")]
        public Dictionary<string, ValueSetValue> ValueSetValues { get; set; }
        public static MedicalProducts FromJson(string json) => JsonConvert.DeserializeObject<MedicalProducts>(json, CovidPassTestReader.CovidPass.Json.Converter.Settings);

    }
}
