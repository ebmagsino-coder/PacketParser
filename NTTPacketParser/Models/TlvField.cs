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
		public int Length { get; set; }
		public string Value { get; set; }
	}
}
