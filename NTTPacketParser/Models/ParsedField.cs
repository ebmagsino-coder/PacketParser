using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTPacketParser.Models
{
	public class ParsedField
	{
		public int No { get; set; }
		public string Field { get; set; }
		public string HexValue { get; set; }
		public string Value { get; set; }

	}
}
