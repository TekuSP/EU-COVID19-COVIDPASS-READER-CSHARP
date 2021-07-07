using System;
using System.Collections.Generic;

using System.Globalization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CovidPassTestReader.CovidPass.Json
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
    public partial class ValueSetValue
    {
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("system")]
        public Uri System { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
        public override string ToString()
        {
            return Display;
        }
    }
    public static class Serialize
    {
        public static string ToJson(this MedicalProducts self) => JsonConvert.SerializeObject(self, CovidPassTestReader.CovidPass.Json.Converter.Settings);
        public static string ToJson(this CountryCodes self) => JsonConvert.SerializeObject(self, CovidPassTestReader.CovidPass.Json.Converter.Settings);
        public static string ToJson(this Manufacturers self) => JsonConvert.SerializeObject(self, CovidPassTestReader.CovidPass.Json.Converter.Settings);
    }
}
