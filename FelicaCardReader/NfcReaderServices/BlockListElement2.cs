using System;
namespace FelicaCardReader.NfcReaderServices
{
    public class BlockListElement2: BlockListElement
    {
        private int _number;
        public BlockListElement2(AccessMode accessMode, int serviceCodeIndex, int number): base(accessMode, serviceCodeIndex, number)
        {
            _number = number;
        }

        public override byte[] ToByteArray() {
            var retByte = new byte[2] { base.FirstByte(), (byte)_number };
            return retByte;
        }

        public override int Size() 
        {
            return 2;
        }
    }
}
