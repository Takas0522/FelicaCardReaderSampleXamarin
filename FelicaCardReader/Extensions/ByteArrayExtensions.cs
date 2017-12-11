using System;
namespace FelicaCardReader.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] CopyOfRange(this byte[] byt, int start, int end)
        {
            var len = end - start;
            var dest = new byte[len];
            for (int i = 0; i < len; i++)
            {
                dest[i] = byt[start + i];
            }
            return dest;
        }

        public static byte[] Write(this byte[] byt, byte writeData)
        {
            var len = byt.Length;
            Array.Resize<byte>(ref byt, len + 1);
            byt[len] = writeData;
            return byt;
        }

        public static byte[] Write(this byte[] byt, byte[] writeData)
        {

            foreach (var item in writeData)
            {
                byt = byt.Write(item);
            }

            return byt;
        }
    }
}
