using System;
using System.Collections.Generic;
using System.Linq;

namespace FelicaCardReader.NfcReaderServices
{
    public class ReadWithoutEncryptionCommand : INfcCommand
    {

        private List<byte[]> _serviceList;
        private List<BlockListElement> _blocks;
        private byte[] _IDm;

        public ReadWithoutEncryptionCommand(
            byte[] IDm,
            List<byte[]> serviceList,
            List<BlockListElement> blocks
        ) {
            _serviceList = serviceList;
            _blocks = blocks;
            _IDm = IDm;
        }

        public byte CommandCode => (byte)0x06;

        private int NumberOfServices => _serviceList.Count;

        private int NumberOfBlocks => _blocks.Count;

        private int BlockSize => _blocks.Select(x => x.Size()).Aggregate((x, y) => x + y);

        private int PacketSize => 13 + (NumberOfServices * 2) + BlockSize;

        public byte[] RequestPacket()
        {
            var retByte = new byte[PacketSize];
            var i = 0;
            retByte[i++] = (byte)PacketSize;
            retByte[i++] = CommandCode;
            foreach (var byteData in _IDm)
            {
                retByte[i++] = byteData;
            }
            retByte[i++] = (byte)NumberOfServices;

            foreach (var item in _serviceList)
            {
                var index = 0;
                foreach(var byteData in item)
                {
                    if ((index % 2) == 0) {
                        retByte[i + 1] = byteData;
                    } else {
                        retByte[i - 1] = byteData;
                    }
                    i++;
                }
            }
            retByte[i++] = (byte)NumberOfBlocks;
            foreach (var item in _blocks)
            {
                var  block = item.ToByteArray();
                foreach (var blockByte in block)
                {
                    retByte[i++] = blockByte;
                }
            }
            return retByte;
        }
    }
}
