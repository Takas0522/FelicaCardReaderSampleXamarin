using System;
using FelicaCardReader.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace FelicaCardReader.NfcReaderServices
{

    public class ReadWithoutEncryptionResponse : NfcResponse
    {
        private List<byte[]> _blocks;
        public List<byte[]> blocks 
        {
            get { return _blocks; }
        }

        public ReadWithoutEncryptionResponse(byte[] response): base(response)
        {
            this._blocks = Blocks();
        }

        public override int ResponseSize() => (int)Response[0];

        public override byte ResponseCode() => Response[1];

        public override byte[] IDm()
        {
            return Response.CopyOfRange(2, 10);
        }

        public byte StatusFlag1()
        {
            return Response[10];
        }

        public byte StatusFlag2()
        {
            return Response[11];
        }

        public int NumberOfBlocks()
        {
            return (int)Response[12];
        }

        public List<byte[]> Blocks() 
        {
            var i = 0;
            var result = new List<byte[]>();
            var row = Response.CopyOfRange(13, Response.Length);
            while(i < NumberOfBlocks())
            {
                var addResult = row.CopyOfRange(i * 16, i * 16 + 16);
                result.Add(addResult);
                i++;
            }
            return result;
        }
    }
}
