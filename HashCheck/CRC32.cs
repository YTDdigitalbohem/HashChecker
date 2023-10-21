using System;
using System.IO;

namespace HashCheck
{
    public static class Crc32Calculator
    {

        //public static UInt32 CalculateCrc32Int(string filePath)
        //{
        //    using (FileStream fs = File.OpenRead(filePath))
        //    {
        //        var crc32 = new System.IO.Hashing.Crc32();
        //        byte[] fileBytes = File.ReadAllBytes(filePath);
        //        crc32.Append(fileBytes);

        //        var checkSum = crc32.GetCurrentHash();
        //        Array.Reverse(checkSum);

        //        return BitConverter.ToUInt32(checkSum,0);
        //    }
        //}

        public static string CalculateCrc32Hex(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                var crc32 = new System.IO.Hashing.Crc32();
                byte[] fileBytes = File.ReadAllBytes(filePath);
                crc32.Append(fileBytes);

                var checkSum = crc32.GetCurrentHash();
                Array.Reverse(checkSum);

                return BitConverter.ToString(checkSum,0).Replace("-", "").ToLower();
            }
        }
    }

}