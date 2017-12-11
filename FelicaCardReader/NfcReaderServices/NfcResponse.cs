using System;
namespace FelicaCardReader.NfcReaderServices
{
    public abstract class NfcResponse
    {
        private byte[] response;
        public byte[] Response
        {
            get => response;
        }

        public NfcResponse(byte[] sendResponse)
        {
            response = sendResponse;
        }

        public abstract int ResponseSize();

        public abstract byte ResponseCode();

        public abstract byte[] IDm();
    }
}
