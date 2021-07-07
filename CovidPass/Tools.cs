using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPassTestReader.CovidPass
{
    /// <summary>
    /// Tools
    /// </summary>
    public static class Tools
    {
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
                switch (test.PeekState())
                {
                    case CborReaderState.UnsignedInteger:
                        temp = test.ReadUInt32();
                        break;
                    case CborReaderState.NegativeInteger:
                        temp = test.ReadInt32();
                        break;
                    case CborReaderState.ByteString:
                        temp = test.ReadByteString();
                        break;
                    case CborReaderState.TextString:
                        temp = test.ReadTextString();
                        break;
                    case CborReaderState.SimpleValue:
                        temp = test.ReadSimpleValue();
                        break;
                    case CborReaderState.HalfPrecisionFloat:
                        temp = test.ReadHalf();
                        break;
                    case CborReaderState.SinglePrecisionFloat:
                        temp = test.ReadSingle();
                        break;
                    case CborReaderState.DoublePrecisionFloat:
                        temp = test.ReadDouble();
                        break;
                    case CborReaderState.Boolean:
                        temp = test.ReadBoolean();
                        break;
                    default:
                        temp = test.ReadEncodedValue();
                        break;
                }
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
    }
}
