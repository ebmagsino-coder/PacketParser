using NTTPacketParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTPacketParser.Helpers
{
	public class TlvParser
	{
		private static readonly Dictionary<byte, string> TagNames = new()
		{
			{ 0x01, "VISA CREDIT" },
			{ 0x02, "Application ID" },
			{ 0x03, "Application Cryptogram" },
			{ 0x0D, "Issuer Application Data" }
		};

		public static List<TlvField> Parse(HexReader r, int totalLength)
		{
			var list = new List<TlvField>();

			int start = r.Position;
			int end = start + totalLength;

			while (r.Position < end)
			{
				if (r.Position + 2 > end)
					break;

				byte tag = r.ReadByte();
				int len = r.ReadByte();

				if (r.Position + len > end)
					break;

				byte[] valueBytes = r.ReadBytes(len);
				string value = Encoding.ASCII.GetString(valueBytes);

				// For hex values, show both hex and ASCII if printable
				if (tag == 0x03 || tag == 0x0D)
				{
					value = string.Join("", valueBytes.Select(b => b.ToString("X2")));
				}

				string tagName = TagNames.ContainsKey(tag) ? TagNames[tag] : $"Tag {tag:X2}";

				list.Add(new TlvField
				{
					Tag = tag.ToString("X2"),
					TagName = tagName,
					Length = len,
					Value = value
				});
			}

			return list;
		}
	}
}
