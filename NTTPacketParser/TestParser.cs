using NTTPacketParser.Helpers;
using System;

namespace NTTPacketParser
{
    public class TestParser
    {
        public static void TestSampleData()
        {
            string sampleHex = "02 D2 00 A2 01 31 30 30 30 30 30 30 30 30 30 30 30 30 30 31 32 30 30 30 30 30 30 32 00 00 00 05 34 32 11 35 34 35 35 32 32 2A 2A 2A 2A 2A 2A 2A 34 34 34 35 01 2F 00 00 04 56 49 53 41 07 04 38 37 30 30 00 00 15 00 02 32 07 59 37 53 4A 45 31 00 00 36 32 37 33 36 32 38 31 39 32 37 32 01 30 30 00 3D 01 0B 56 49 53 41 20 43 52 45 44 49 54 02 0E 41 30 30 30 30 30 30 30 30 33 31 30 31 30 03 10 39 38 33 34 39 36 42 33 31 43 36 33 42 46 36 42 0D 0C 36 33 37 32 38 31 37 32 36 33 36 32 11 10 25 11 33 09 16 05 03";

            var parser = new PosMessageParser();
            parser.Parse(sampleHex);

            Console.WriteLine("=== MAIN FIELDS ===");
            foreach (var field in parser.Fields)
            {
                Console.WriteLine($"{field.No}. {field.Field}: {field.Value}");
            }

            Console.WriteLine("\n=== OTHER DETAILS (TLV) ===");
            if (parser.OtherDetails != null)
            {
                foreach (var tlv in parser.OtherDetails)
                {
                    Console.WriteLine($"Tag {tlv.Tag} ({tlv.TagName}): {tlv.Value}");
                }
            }
        }
    }
}