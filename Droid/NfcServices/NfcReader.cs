using System;
using Android.Nfc;
using Android.Nfc.Tech;
using FelicaCardReader.NfcReaderServices;

namespace FelicaCardReader.Droid.NfcServices
{
    public static class NfcReader
    {
        public static ReadWithoutEncryptionResponse Read(Tag tag)
        {
            var nfcf = NfcF.Get(tag);
            try
            {
                nfcf.Connect();
                return Read(nfcf);

            }
            catch (Exception e)
            {
                var errText = e.Message;
                if (nfcf.IsConnected)
                {
                    nfcf.Close();
                }
                return null;
            }
        }

        public static ReadWithoutEncryptionResponse Read(NfcF nfcf)
        {
            var targetSystemCode = new byte[2] { (byte)0x00, (byte)0x03 };

            // [1]
            var poolingCommand = new PollingCommand(targetSystemCode);
            var pooolingRequest = poolingCommand.RequestPacket();
            var rawPoolingResponse = nfcf.Transceive(pooolingRequest);
            var poolingResponse = new PoolingResponse(rawPoolingResponse);

            var targetIdm = poolingResponse.IDm();

            var serviceCode = new byte[2] { (byte)0x0f, (byte)0x09 };

            // [2]
            var requestServiceCommand = new RequestServiceCommand(targetIdm, serviceCode);
            var requestServiceRequest = requestServiceCommand.RequestPacket();
            var rawRequestServiceResponse = nfcf.Transceive(requestServiceRequest);
            var requestServiceResponcse = new RequestServiceResponse(rawRequestServiceResponse);

            // [3]
            var requestCommandRireki = new ReadWithoutEncryptionCommandRireki();
            var requestCommandPacket = requestCommandRireki.RequestPacket(targetIdm, 10);

            var rawReadWithoutEncryptionCommandRireki = nfcf.Transceive(requestCommandPacket);
            var retData = new ReadWithoutEncryptionResponse(rawReadWithoutEncryptionCommandRireki);

            return retData;
        }
    }
}
