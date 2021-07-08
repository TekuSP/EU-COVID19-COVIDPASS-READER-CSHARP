//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using System;
using System.Collections.Generic;
using System.Formats.Cbor;

namespace CovidPassReader.CovidPass
{
    /// <summary>
    /// Tools
    /// </summary>
    public static class Tools
    {
        #region Public Methods

        /// <summary>
        /// CBOR IS EVIL. This helps mitigate that evil by reading dictionary from CBOR.
        /// </summary>
        /// <param name="input">CBOR data</param>
        /// <returns>Dictionary of data from CBOR</returns>
        public static Dictionary<object, object> ReadDict(ReadOnlyMemory<byte> input)
        {
            var test = new CborReader(input);
            bool isArray = false;
            if (test.PeekState() == CborReaderState.StartArray)
            {
                test.ReadStartArray();
                isArray = true;
            }
            var mapStart = test.ReadStartMap();
            Dictionary<object, object> keyValuePairs = new Dictionary<object, object>();
            object key = null;
            for (int i = 0; i < mapStart * 2; i++)
            {
                object temp = null;
                temp = test.PeekState() switch
                {
                    CborReaderState.UnsignedInteger => test.ReadUInt32(),
                    CborReaderState.NegativeInteger => test.ReadInt32(),
                    CborReaderState.ByteString => test.ReadByteString(),
                    CborReaderState.TextString => test.ReadTextString(),
                    CborReaderState.SimpleValue => test.ReadSimpleValue(),
                    CborReaderState.HalfPrecisionFloat => test.ReadHalf(),
                    CborReaderState.SinglePrecisionFloat => test.ReadSingle(),
                    CborReaderState.DoublePrecisionFloat => test.ReadDouble(),
                    CborReaderState.Boolean => test.ReadBoolean(),
                    _ => test.ReadEncodedValue(),
                };
                if (key == null)
                {
                    key = temp;
                }
                else
                {
                    keyValuePairs.Add(key, temp);
                    key = null;
                }
            }
            test.ReadEndMap();
            if (isArray)
                test.ReadEndArray();
            return keyValuePairs;
        }

        #endregion Public Methods
    }
}