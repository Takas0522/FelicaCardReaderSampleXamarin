using System;
using FelicaCardReader.Extensions;

namespace FelicaCardReader.NfcReaderServices
{
    public class RequestServiceResponse : NfcResponse
    {
        public RequestServiceResponse(byte[] response) : base(response)
        { }

        public override int ResponseSize() => (int) Response[0];

        public override byte ResponseCode() => Response[1];

        public override byte[] IDm() => Response.CopyOfRange(2,10);

        public int NumberOfNodes() => (int)Response[11];

        public class NodeKeyVersion
        {
            public NodeKeyVersion(byte[] values)
            {
                _values = values;
            }
            private byte[] _values;
            public byte[] Value
            {
                get
                {
                    var ret = new byte[2] {_values[1], _values[0] };
                    return ret;
                }
            }
        }
    }
}
