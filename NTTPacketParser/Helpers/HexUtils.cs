using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTPacketParser.Helpers
{
	public class HexUtils
	{
		public static byte[] HexToBytes(string hex)
		{
			hex = hex.Replace(" ", "").Replace("\r", "").Replace("\n", "");
			return Enumerable.Range(0, hex.Length / 2)
				.Select(i => Convert.ToByte(hex.Substring(i * 2, 2), 16))
				.ToArray();
		}

		public static string MaskPan(string pan)
		{
			if (pan.Length < 10) return pan;
			return pan[..6] + new string('*', pan.Length - 10) + pan[^4..];
		}
	}
}
