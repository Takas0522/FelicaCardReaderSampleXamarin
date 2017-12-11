using System;
using FelicaCardReader.Extensions;
namespace FelicaCardReader.NfcReaderServices
{
    public class ReadWithoutEncryptionCommandRireki
    {
        public byte[] RequestPacket(byte[] idm, int size)
        {
            var bout = new byte[1] { 0 }; //DataLength Dummy
            bout = bout.Write((byte)0x06);
            bout = bout.Write(idm);
            bout = bout.Write((byte)1);
            bout = bout.Write((byte)0x0f);
            bout = bout.Write((byte)0x09);
            bout = bout.Write((byte)size);

            for (int i = 0; i < size; i++)
            {
                bout = bout.Write((byte)0x80);
                bout = bout.Write((byte)i);
            }
            bout[0] = (byte)bout.Length;

            return bout;
        }
    }
}
