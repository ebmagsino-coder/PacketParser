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
			{ 0x01, "App Name" },
			{ 0x02, "App AID" },
			{ 0x03, "TC" },
			{ 0x04, "Installment Type" },
			{ 0x05, "Installment Term" },
			{ 0x06, "Gross Amount" },
			{ 0x07, "Installment Rate" },
			{ 0x08, "Monthly Amortization" },
			{ 0x09, "DCC Details" },
			{ 0x0A, "QR String" },
			{ 0x0B, "Previous Balance" },
			{ 0x0C, "Available Balance" },
			{ 0x0D, "Original RRN" }
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

				int tagPos = r.Position;
				byte tag = r.ReadByte();
				int len = r.ReadByte();

				if (r.Position + len > end)
					break;

				byte[] valueBytes = r.ReadBytes(len);
				string value;

				// Handle specific tag formatting
				if (tag == 0x03) // TC - convert ASCII hex to actual hex
				{
					string asciiHex = Encoding.ASCII.GetString(valueBytes);
					value = "";
					for (int i = 0; i < asciiHex.Length; i += 2)
					{
						if (i + 1 < asciiHex.Length)
						{
							byte hexByte = Convert.ToByte(asciiHex.Substring(i, 2), 16);
							value += hexByte.ToString("X2");
						}
					}
				}
				else if (tag == 0x09) // DCC Details - format with labels
				{
					string rawValue = Encoding.ASCII.GetString(valueBytes);
					string[] parts = rawValue.Split(',');
					if (parts.Length >= 5)
					{
						value = $"[DCC Amount: {parts[0]}],[Currency: {parts[1]}],[Exchange rate: {parts[2]}],[Exchange bank: {parts[3]}],[Margin: {parts[4]}]";
					}
					else
					{
						value = rawValue; // Fallback to original if format unexpected
					}
				}
				else // Default ASCII for all other tags
				{
					value = Encoding.ASCII.GetString(valueBytes);
				}

				string tagName = TagNames.ContainsKey(tag) ? TagNames[tag] : $"Tag {tag:X2}";
				string hexValue = r.GetHexString(tagPos, 2 + len); // Tag + Length + Value

				list.Add(new TlvField
				{
					Tag = $"0x{tag:X2}",
					TagName = tagName,
					HexValue = hexValue,
					Value = value
				});
			}

			return list;
		}
	}
}
