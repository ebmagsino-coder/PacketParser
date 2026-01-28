using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTPacketParser.Models
{
	public class TlvField
	{
		public string Tag { get; set; }
		public string TagName { get; set; }
		public string HexValue { get; set; }
		public string Value { get; set; }
	}
}
