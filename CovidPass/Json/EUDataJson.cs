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
    public partial class EUData
    {
        [JsonProperty("valueSetId")]
        public string ValueSetId { get; set; }

        [JsonProperty("valueSetDate")]
        public DateTimeOffset ValueSetDate { get; set; }

        [JsonProperty("valueSetValues")]
        public Dictionary<string, ValueSetValue> ValueSetValues { get; set; }
        public static EUData FromJson(string json) => JsonConvert.DeserializeObject<EUData>(json, CovidPassTestReader.CovidPass.Json.Converter.Settings);
        public override string ToString()
        {
            return ValueSetId;
        }
    }
    public static class Serialize
    {
        public static string ToJson(this EUData self) => JsonConvert.SerializeObject(self, CovidPassTestReader.CovidPass.Json.Converter.Settings);
    }
}