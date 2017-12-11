using System;
namespace FelicaCardReader.NfcReaderServices
{
    public abstract class BlockListElement
    {
        private AccessMode _accessMode;
        private int _serviceCodeIndex;
        private int _number;

        public BlockListElement(AccessMode accessMode, int serviceCodeIndex, int number)
        {
            _accessMode = accessMode;
            _serviceCodeIndex = serviceCodeIndex;
            _number = number;
        }

        public byte FirstByte()
        {
            return (byte)((0 << 7) & ((int)_accessMode << 6) & (_serviceCodeIndex << 3));
        }

        public abstract byte[] ToByteArray();

        public abstract int Size();
    }

    public enum AccessMode {
        toNotParseService,
        toParseService
    }
}
