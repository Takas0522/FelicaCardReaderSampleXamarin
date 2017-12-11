using System;
namespace FelicaCardReader.NfcReaderServices
{
    public class PollingCommand : INfcCommand
    {
        private byte[] _systemCode;
        private Request _request;
        public PollingCommand(byte[] systemCode, Request request = Request.systemCode)
        {
            _systemCode = systemCode;
            _request = request;
        }

        public byte CommandCode => (byte)0x00;

        public byte TimeSlot => (byte)0x0f;

        public byte RequestCode => (byte)((int)_request);

        public byte[] RequestPacket()
        {
            var retByte = new byte[6] {
                (byte)0x06,
                CommandCode,
                _systemCode[0],
                _systemCode[1],
                RequestCode,
                TimeSlot
            };
            return retByte;
        }
   }

    public enum Request {
        none = 0x00,
        systemCode = 0x01,
        communicationAbility = 0x02
    }
}
