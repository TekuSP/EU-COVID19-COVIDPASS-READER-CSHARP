//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;
using System.Globalization;

namespace CovidPassReader.CovidPass.Json
{
    public static class Serialize
    {
        #region Public Methods

        public static string ToJson(this EUData self) => JsonConvert.SerializeObject(self, CovidPassReader.CovidPass.Json.Converter.Settings);

        #endregion Public Methods
    }

    public partial class EUData
    {
        #region Public Properties

        [JsonProperty("valueSetDate")]
        public DateTimeOffset ValueSetDate { get; set; }

        [JsonProperty("valueSetId")]
        public string ValueSetId { get; set; }

        [JsonProperty("valueSetValues")]
        public Dictionary<string, ValueSetValue> ValueSetValues { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static EUData FromJson(string json) => JsonConvert.DeserializeObject<EUData>(json, CovidPassReader.CovidPass.Json.Converter.Settings);

        public override string ToString()
        {
            return ValueSetId;
        }

        #endregion Public Methods
    }

    public partial class ValueSetValue
    {
        #region Public Properties

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("system")]
        public Uri System { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return Display;
        }

        #endregion Public Methods
    }

    internal static class Converter
    {
        #region Public Fields

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        #endregion Public Fields
    }
}