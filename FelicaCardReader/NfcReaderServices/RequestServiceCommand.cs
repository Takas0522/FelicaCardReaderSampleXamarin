using System;
using System.Collections.Generic;

namespace FelicaCardReader.NfcReaderServices
{
    public class RequestServiceCommand: INfcCommand
    {
        private byte[] _Idm;
        private List<byte[]> _NodeCode;
        public RequestServiceCommand(byte[] IDm, List<byte[]> nodeCode)
        {
            InitValue(IDm, nodeCode);
        }

        public RequestServiceCommand(byte[] IDm, byte[] nodeCode)
        {
            var listNode = new List<byte[]>();
            listNode.Add(nodeCode);
            InitValue(IDm, listNode);
        }

        private void InitValue(byte[] Idm, List<byte[]> nodeCode) 
        {
            _Idm = Idm;
            _NodeCode = nodeCode;
        }

        public byte CommandCode => 0x02;

        private byte NuberOfNodes => (byte)_NodeCode.Count;

        private int PacketSize => (11 + _NodeCode.Count * 2);

        public byte[] RequestPacket()
        {
            var retByte = new byte[PacketSize];
            var i = 0;
            retByte[i++] = (byte)PacketSize;
            retByte[i++] = CommandCode;
            foreach (var byteData in _Idm)
            {
                retByte[i++] = (byte)byteData;
            }
            retByte[i++] = NuberOfNodes;

            foreach (var item in _NodeCode)
            {
                var index = 0;
                foreach (var byteData in item)
                {
                    if ((index % 2) == 0) {
                        retByte[i + 1] = byteData;
                    } else {
                        retByte[i - 1] = byteData;
                    }
                    i++;
                    index++;
                }
            }
            return retByte;
        }
    }
}
