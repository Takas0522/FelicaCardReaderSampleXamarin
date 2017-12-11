using System;
using FelicaCardReader.Extensions;
namespace FelicaCardReader.NfcReaderServices
{
    public class PoolingResponse
    {
        private byte[] _response;
        public PoolingResponse(byte[] response)
        {
            _response = response;
        }

        public int ResponseSize()
        {
            return (int)_response[0];
        }

        public byte ResponseCode()
        {
            return _response[1];
        }

        public byte[] IDm()
        {
            return _response.CopyOfRange(2, 10);
        }
    }
}
