using System;
namespace FelicaCardReader.NfcReaderServices
{
    public interface INfcCommand
    {

        byte CommandCode { get; }
        byte[] RequestPacket();

    }
}
