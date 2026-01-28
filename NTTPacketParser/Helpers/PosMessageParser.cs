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
			byte stx = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "STX", Value = stx.ToString("X2") });

			// 2. Command ID
			byte cmd = reader.ReadByte();
			string cmdDesc = cmd switch
			{
				0xD2 => "D2 - Void Command",
				_ => $"{cmd:X2} - Unknown Command"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Command ID", Value = cmdDesc });

			// 3. Data Length
			ushort dataLen = reader.ReadUInt16();
			Fields.Add(new ParsedField { No = no++, Field = "Data length", Value = $"{dataLen} length" });

			// 4. Constant 01
			byte constant = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "01", Value = constant.ToString("X2") });

			// 5. MID
			string mid = reader.ReadAscii(15);
			Fields.Add(new ParsedField { No = no++, Field = "MID", Value = mid });

			// 6. TID
			string tid = reader.ReadAscii(8);
			Fields.Add(new ParsedField { No = no++, Field = "TID", Value = tid });

			// 7. Transaction Amount
			decimal amount = reader.ReadAmount(6);
			Fields.Add(new ParsedField { No = no++, Field = "Transaction Amount", Value = amount.ToString("F2") });

			// 8. PAN Length
			byte panLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "PAN Length", Value = $"{panLen} length" });

			// 9. Card PAN
			string pan = reader.ReadAscii(panLen);
			Fields.Add(new ParsedField { No = no++, Field = "Card PAN", Value = pan });

			// 10. Card Holder Length
			byte holderLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "Card Holder Length", Value = $"{holderLen} length" });

			// 11. Card Holder Name
			string holderName = reader.ReadAscii(holderLen);
			Fields.Add(new ParsedField { No = no++, Field = "Card Holder Name", Value = holderName });

			// 12. Card Expiry Date
			byte[] expiryBytes = reader.ReadBytes(2);
			string expiry = string.Join(" ", expiryBytes.Select(b => b.ToString("X2")));
			Fields.Add(new ParsedField { No = no++, Field = "Card Expiry Date", Value = $"{expiry} (masked)" });

			// 13. Card Type Length
			byte cardTypeLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "Card Type Length", Value = $"{cardTypeLen} length" });

			// 14. Card Type
			string cardType = reader.ReadAscii(cardTypeLen);
			Fields.Add(new ParsedField { No = no++, Field = "Card Type", Value = cardType });

			// 15. Entry Mode
			byte entryMode = reader.ReadByte();
			string entryModeDesc = entryMode switch
			{
				0x07 => "07 - Contactless",
				_ => $"{entryMode:X2} - Unknown"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Entry Mode", Value = entryModeDesc });

			// 16. ECR Invoice Length
			byte ecrInvoiceLen = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "ECR Invoice Length", Value = $"{ecrInvoiceLen} length" });

			// 17. ECR Invoice Number
			string ecrInvoice = reader.ReadAscii(ecrInvoiceLen);
			Fields.Add(new ParsedField { No = no++, Field = "ECR Invoice Number", Value = ecrInvoice });

			// 18. Batch Number
			byte[] batchBytes = reader.ReadBytes(3);
			int batchNum = (batchBytes[0] << 16) | (batchBytes[1] << 8) | batchBytes[2];
			Fields.Add(new ParsedField { No = no++, Field = "Batch Number", Value = batchNum.ToString() });

			// 19. Terminal Invoice Number
			byte[] termInvoiceBytes = reader.ReadBytes(3);
			int termInvoiceNum = (termInvoiceBytes[0] << 16) | (termInvoiceBytes[1] << 8) | termInvoiceBytes[2];
			Fields.Add(new ParsedField { No = no++, Field = "Terminal Invoice Number", Value = termInvoiceNum.ToString() });

			// 20. Acquirer Code
			byte acquirerCode = reader.ReadByte();
			string acquirerDesc = acquirerCode switch
			{
				0x07 => "07 - BDO Credit",
				_ => $"{acquirerCode:X2} - Unknown"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Acquirer Code", Value = acquirerDesc });

			// 21. Approval Code
			string approvalCode = reader.ReadAscii(6);
			Fields.Add(new ParsedField { No = no++, Field = "Approval Code", Value = approvalCode });

			// 22. Retrieval Reference Number
			byte[] rrnBytes = reader.ReadBytes(14);
			string rrn = Encoding.ASCII.GetString(rrnBytes.Skip(2).ToArray()); // Skip first 2 null bytes
			Fields.Add(new ParsedField { No = no++, Field = "Retrieval Reference Number", Value = rrn });

			// 23. Receipt Format
			byte receiptFormat = reader.ReadByte();
			string receiptDesc = receiptFormat switch
			{
				0x01 => "01 - Card format",
				_ => $"{receiptFormat:X2} - Unknown"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Receipt Format", Value = receiptDesc });

			// 24. Response Code
			string responseCode = reader.ReadAscii(2);
			string responseDesc = responseCode switch
			{
				"00" => "00 - Success",
				_ => $"{responseCode} - Error"
			};
			Fields.Add(new ParsedField { No = no++, Field = "Response Code", Value = responseDesc });

			// 25. Length of Other Details
			ushort otherDetailsLen = reader.ReadUInt16();
			Fields.Add(new ParsedField { No = no++, Field = "Length of Other Details", Value = $"{otherDetailsLen} length" });

			// 26. Other Details (TLV)
			OtherDetails = TlvParser.Parse(reader, otherDetailsLen);
			Fields.Add(new ParsedField { No = no++, Field = "Other Details", Value = "refer to data table" });

			// 27. Transaction Date and Time
			byte[] dateTimeBytes = reader.ReadBytes(6);
			// Format: YY MM DD HH MM SS
			int year = 2000 + dateTimeBytes[0];
			int month = dateTimeBytes[1];
			int day = dateTimeBytes[2];
			int hour = dateTimeBytes[3];
			int minute = dateTimeBytes[4];
			int second = dateTimeBytes[5];
			string dateTime = $"{GetMonthName(month)}. {day:D2}, {year} {hour:D2}:{minute:D2}:{second:D2}";
			Fields.Add(new ParsedField { No = no++, Field = "Transaction Date and Time", Value = dateTime });

			// 28. CRC
			byte[] crc = reader.ReadBytes(2);
			string crcValue = string.Join(" ", crc.Select(b => b.ToString("X2")));
			Fields.Add(new ParsedField { No = no++, Field = "CRC", Value = crcValue });

			// 29. ETX
			byte etx = reader.ReadByte();
			Fields.Add(new ParsedField { No = no++, Field = "ETX", Value = etx.ToString("X2") });
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
