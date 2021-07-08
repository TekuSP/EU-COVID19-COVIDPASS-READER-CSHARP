//Copyright 2021 Richard "TekuSP" Torhan
//See LICENSE for License information
//Used license: Apache License, Version 2.0, January 2004, http://www.apache.org/licenses/
using System;
using System.Collections.Generic;
using System.Formats.Cbor;

namespace CovidPassReader.CovidPass
{
    /// <summary>
    /// This contains low level data about CovidPass, mainly doing CBOR work
    /// </summary>
    public class Payload
    {
        #region Public Constructors

        /// <summary>
        /// Loads CBOR based on byte array input, unzipped covidpass at best
        /// </summary>
        /// <param name="input">Unzipped covid pass</param>
        public Payload(byte[] input)
        {
            //STEP 4: CBOR
            CborReader cbor = new CborReader(input, conformanceMode: CborConformanceMode.Lax);

            var tag = cbor.ReadTag(); //TAG?

            var arrayLenght = cbor.ReadStartArray(); //Array
            if (arrayLenght != 4) //Is it valid Payload?
            {
                throw new ArgumentException("Not valid Covid Pass data! Payload is invalid!");
            }
            var bytes = cbor.ReadByteString(); //1, COLOR
            Color = new Color() { R = bytes[0], G = bytes[1], B = bytes[2] }; //Read color

            cbor.ReadStartMap(); //2 ???
            Unknown = new KeyValuePair<byte, byte[]>((byte)cbor.ReadUInt32(), cbor.ReadByteString());
            cbor.ReadEndMap();

            MainData = cbor.ReadByteString(); //3, MAIN DATA

            CertificateProbably = cbor.ReadByteString();  //4, probably digital certificate?

            cbor.ReadEndArray(); //We got whole array

            var keyValuePairs = Tools.ReadDict(MainData);
            DecodedData = ((ReadOnlyMemory<byte>)keyValuePairs[-260]).ToArray(); //260 is what we are looking for, main data

            DateTimeOffset dateTimeOffSet = DateTimeOffset.FromUnixTimeSeconds(long.Parse(keyValuePairs[(uint)6].ToString())); //Read validity
            ValidFrom = dateTimeOffSet.DateTime;
            DateTimeOffset dateTimeOffSet2 = DateTimeOffset.FromUnixTimeSeconds(long.Parse(keyValuePairs[(uint)4].ToString())); //Read validity
            ValidTo = dateTimeOffSet2.DateTime;

            keyValuePairs = Tools.ReadDict(DecodedData);
            DecodedData = ((ReadOnlyMemory<byte>)keyValuePairs[(uint)1]).ToArray(); //1 is what we are looking for, sub main data in main data
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// This is where I think we have certificate confirming validity, TODO: work on that
        /// </summary>
        public byte[] CertificateProbably { get; set; }

        /// <summary>
        /// ??? Skin color ???
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Here are more decoded and parsed, but still raw data, this will be usually used
        /// </summary>
        public byte[] DecodedData { get; set; }

        /// <summary>
        /// This is raw main data
        /// </summary>
        public byte[] MainData { get; set; }

        /// <summary>
        /// No clue
        /// </summary>
        public KeyValuePair<byte, byte[]> Unknown { get; set; }

        /// <summary>
        /// Payload validity from
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Payload validity to
        /// </summary>
        public DateTime ValidTo { get; set; }

        #endregion Public Properties
    }
}