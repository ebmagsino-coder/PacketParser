using NTTPacketParser.Helpers;
using System;

namespace NTTPacketParser
{
    public class TestParser
    {
        public static void TestSampleData()
        {
            string sampleHex = "02 B9 00 78 01 31 30 30 30 30 30 30 30 30 30 30 30 30 30 31 32 30 30 30 30 30 30 32 00 00 00 10 00 00 10 39 30 32 31 30 30 30 30 37 36 32 30 31 34 32 36 00 00 00 00 09 47 49 46 54 20 43 41 52 44 02 04 38 37 30 30 00 00 15 00 02 32 00 59 37 53 4A 45 31 00 00 36 32 37 33 36 32 38 31 39 32 37 32 09 30 30 00 0F 0C 04 39 2E 35 30 0B 07 31 30 30 39 2E 35 30 11 10 25 11 33 09 4D E2 03";

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