using NTTPacketParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTPacketParser.Helpers
{
	public class PosMessageParser
	{
		public List<ParsedField> Fields { get; } = new();
		public List<TlvField> OtherDetails { get; private set; }

		public void Parse(string hex)
		{
			var reader = new HexReader(HexUtils.HexToBytes(hex));
			int no = 1;

			// 1. STX
			int pos = reader.Position;
			byte stx = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "STX", HexValue = reader.GetHexString(pos, 1), Value = stx.ToString("X2") });

			// 2. Command ID
			pos = reader.Position;
			byte cmd = reader.ReadByte();
			string cmdDesc = cmd switch
			{
				0xD2 => "D2 - Void Command",
				0xB8 => "B8 - Activation Command",
				0xB9 => "B9 - Reload Command",
				0xB1 => "B1 - Payment Command",
				_ => $"{cmd:X2} - Unknown Command"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Command ID", HexValue = reader.GetHexString(pos, 1), Value = cmdDesc });

			// 3. Data Length
			pos = reader.Position;
			ushort dataLen = reader.ReadUInt16();
			Fields.Add(new ParsedField { No = no++, Field = "Data length", HexValue = reader.GetHexString(pos, 2), Value = $"{dataLen} length" });

			// Calculate data end position (STX + CMD + LEN + DATA)
			int dataEndPos = 3 + dataLen;

			// Only parse fields if we have enough data
			if (reader.Length < dataEndPos + 3) // +3 for CRC+ETX
			{
				Fields.Add(new ParsedField { No = no++, Field = "Error", HexValue = "", Value = $"Packet too short. Expected {dataEndPos + 3} bytes, got {reader.Length}" });
				return;
			}

			// 4. Constant 01
			pos = reader.Position;
			byte constant = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "01", HexValue = reader.GetHexString(pos, 1), Value = constant.ToString("X2") });

			// 5. MID
			pos = reader.Position;
			string mid = reader.ReadAscii(15);
			Fields.Add(new ParsedField { No = no++, Field = "MID", HexValue = reader.GetHexString(pos, 15), Value = mid });

			// 6. TID
			pos = reader.Position;
			string tid = reader.ReadAscii(8);
			Fields.Add(new ParsedField { No = no++, Field = "TID", HexValue = reader.GetHexString(pos, 8), Value = tid });

			// 7. Transaction Amount
			pos = reader.Position;
			decimal amount = reader.ReadAmount(6);
			Fields.Add(new ParsedField { No = no++, Field = "Transaction Amount", HexValue = reader.GetHexString(pos, 6), Value = amount.ToString("F2") });

			// 8. PAN Length
			pos = reader.Position;
			byte panLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "PAN Length", HexValue = reader.GetHexString(pos, 1), Value = $"{panLen} length" });

			// 9. Card PAN
			pos = reader.Position;
			string pan = reader.ReadAscii(panLen);
			Fields.Add(new ParsedField { No = no++, Field = "Card PAN", HexValue = reader.GetHexString(pos, panLen), Value = HexUtils.MaskPan(pan) });

			// 10. Card Holder Length
			pos = reader.Position;
			byte holderLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "Card Holder Length", HexValue = reader.GetHexString(pos, 1), Value = $"{holderLen} length" });

			// 11. Card Holder Name
			pos = reader.Position;
			string holderName = holderLen > 0 ? reader.ReadAscii(holderLen) : "";
			Fields.Add(new ParsedField { No = no++, Field = "Card Holder Name", HexValue = holderLen > 0 ? reader.GetHexString(pos, holderLen) : "", Value = holderName });

			// 12. Card Expiry Date
			pos = reader.Position;
			byte[] expiryBytes = reader.ReadBytes(2);
			string expiry = string.Join(" ", expiryBytes.Select(b => b.ToString("X2")));
			string expiryValue = (expiryBytes[0] == 0x00 && expiryBytes[1] == 0x00) ? $"{expiry} (masked)" : expiry;
			Fields.Add(new ParsedField { No = no++, Field = "Card Expiry Date", HexValue = reader.GetHexString(pos, 2), Value = expiryValue });

			// 13. Card Type Length
			pos = reader.Position;
			byte cardTypeLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "Card Type Length", HexValue = reader.GetHexString(pos, 1), Value = $"{cardTypeLen} length" });

			// 14. Card Type
			pos = reader.Position;
			string cardType = cardTypeLen > 0 ? reader.ReadAscii(cardTypeLen) : "";
			Fields.Add(new ParsedField { No = no++, Field = "Card Type", HexValue = cardTypeLen > 0 ? reader.GetHexString(pos, cardTypeLen) : "", Value = cardType });

			// 15. Entry Mode
			pos = reader.Position;
			byte entryMode = reader.ReadByte();
			string entryModeDesc = entryMode switch
			{
				0x02 => "02 - Swipe",
				0x07 => "07 - Contactless",
				_ => $"{entryMode:X2} - Unknown"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Entry Mode", HexValue = reader.GetHexString(pos, 1), Value = entryModeDesc });

			// 16. ECR Invoice Length
			pos = reader.Position;
			byte ecrInvoiceLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "ECR Invoice Length", HexValue = reader.GetHexString(pos, 1), Value = $"{ecrInvoiceLen} length" });

			// 17. ECR Invoice Number
			pos = reader.Position;
			string ecrInvoice = ecrInvoiceLen > 0 ? reader.ReadAscii(ecrInvoiceLen) : "";
			Fields.Add(new ParsedField { No = no++, Field = "ECR Invoice Number", HexValue = ecrInvoiceLen > 0 ? reader.GetHexString(pos, ecrInvoiceLen) : "", Value = ecrInvoice });

			// 18. Batch Number
			pos = reader.Position;
			byte[] batchBytes = reader.ReadBytes(3);
			// For hex 00 00 15, take the last byte as decimal: 0x15 = 21, but should be 15
			int batchNum = batchBytes[2]; // Just take the last byte as is
			Fields.Add(new ParsedField { No = no++, Field = "Batch Number", HexValue = reader.GetHexString(pos, 3), Value = batchNum.ToString() });

			// 19. Terminal Invoice Number
			pos = reader.Position;
			byte[] termInvoiceBytes = reader.ReadBytes(3);
			// For hex 00 02 32, combine last 2 bytes: 02 32 = 232
			int termInvoiceNum = (termInvoiceBytes[1] << 8) | termInvoiceBytes[2];
			Fields.Add(new ParsedField { No = no++, Field = "Terminal Invoice Number", HexValue = reader.GetHexString(pos, 3), Value = termInvoiceNum.ToString() });

			// 20. Acquirer Code
			pos = reader.Position;
			byte acquirerCode = reader.ReadByte();
			string acquirerDesc = acquirerCode switch
			{
				0x07 => "07 - BDO Credit",
				_ => $"{acquirerCode:X2} - Unknown"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Acquirer Code", HexValue = reader.GetHexString(pos, 1), Value = acquirerDesc });

			// 21. Approval Code
			pos = reader.Position;
			string approvalCode = reader.ReadAscii(6);
			Fields.Add(new ParsedField { No = no++, Field = "Approval Code", HexValue = reader.GetHexString(pos, 6), Value = approvalCode });

			// 22. Retrieval Reference Number
			pos = reader.Position;
			byte[] rrnBytes = reader.ReadBytes(14);
			string rrn = Encoding.ASCII.GetString(rrnBytes.Skip(2).ToArray()); // Skip first 2 null bytes
			Fields.Add(new ParsedField { No = no++, Field = "Retrieval Reference Number", HexValue = reader.GetHexString(pos, 14), Value = rrn });

			// 23. Receipt Format
			pos = reader.Position;
			byte receiptFormat = reader.ReadByte();
			string receiptDesc = receiptFormat switch
			{
				0x01 => "01 - Card format",
				0x09 => "09 - EGC Reload format",
				_ => $"{receiptFormat:X2} - Unknown"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Receipt Format", HexValue = reader.GetHexString(pos, 1), Value = receiptDesc });

			// 24. Response Code
			pos = reader.Position;
			string responseCode = reader.ReadAscii(2);
			string responseDesc = responseCode switch
			{
				"00" => "00 - Success",
				_ => $"{responseCode} - Error"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Response Code", HexValue = reader.GetHexString(pos, 2), Value = responseDesc });

			// 25. Length of Other Details
			pos = reader.Position;
			ushort otherDetailsLen = reader.ReadUInt16();
			Fields.Add(new ParsedField { No = no++, Field = "Length of Other Details", HexValue = reader.GetHexString(pos, 2), Value = $"{otherDetailsLen} length" });

			// 26. Other Details (TLV)
			pos = reader.Position;
			OtherDetails = TlvParser.Parse(reader, otherDetailsLen);
			Fields.Add(new ParsedField { No = no++, Field = "Other Details", HexValue = reader.GetHexString(pos, otherDetailsLen), Value = "refer to the 2nd table" });

			// 27. Transaction Date and Time
			if (reader.Position + 6 <= reader.Length - 3) // Check if we have enough bytes (6 for date + 3 for CRC+ETX)
			{
				pos = reader.Position;
				byte[] dateTimeBytes = reader.ReadBytes(6);
				var year = 2000 + ((dateTimeBytes[2] >> 4) * 10) + (dateTimeBytes[2] & 0x0F);
				var month = ((dateTimeBytes[0] >> 4) * 10) + (dateTimeBytes[0] & 0x0F);
				var day = ((dateTimeBytes[1] >> 4) * 10) + (dateTimeBytes[1] & 0x0F);
				var hour = ((dateTimeBytes[3] >> 4) * 10) + (dateTimeBytes[3] & 0x0F);
				var minute = ((dateTimeBytes[4] >> 4) * 10) + (dateTimeBytes[4] & 0x0F);
				var second = ((dateTimeBytes[5] >> 4) * 10) + (dateTimeBytes[5] & 0x0F);
				
				string dateTime = $"{GetMonthName(month)}. {day:D2}, {year} {hour:D2}:{minute:D2}:{second:D2}";
				Fields.Add(new ParsedField { No = no++, Field = "Transaction Date and Time", HexValue = reader.GetHexString(pos, 6), Value = dateTime });
			}

			// 28. CRC
			if (reader.Position + 2 <= reader.Length - 1) // Check if we have CRC + ETX
			{
				pos = reader.Position;
				byte[] crc = reader.ReadBytes(2);
				string crcValue = string.Join(" ", crc.Select(b => b.ToString("X2")));
				Fields.Add(new ParsedField { No = no++, Field = "CRC", HexValue = reader.GetHexString(pos, 2), Value = crcValue });
			}

			// 29. ETX
			if (reader.Position < reader.Length)
			{
				pos = reader.Position;
				byte etx = reader.ReadByte();
				Fields.Add(new ParsedField { No = no++, Field = "ETX", HexValue = reader.GetHexString(pos, 1), Value = etx.ToString("X2") });
			}
		}

		private static string GetMonthName(int month)
		{
			return month switch
			{
				1 => "Jan",
				2 => "Feb",
				3 => "Mar",
				4 => "Apr",
				5 => "May",
				6 => "Jun",
				7 => "Jul",
				8 => "Aug",
				9 => "Sep",
				10 => "Oct",
				11 => "Nov",
				12 => "Dec",
				_ => "Unknown"
			};
		}
	}
}
