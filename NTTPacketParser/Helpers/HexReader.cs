using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTPacketParser.Helpers
{
	public class HexReader
	{
		private readonly byte[] _data;
		public int Position { get; private set; }

		public HexReader(byte[] data)
		{
			_data = data;
		}

		public byte ReadByte()
		{
			if (Position >= _data.Length)
				throw new InvalidOperationException(
					$"Attempted to read past end of buffer. Position={Position}, Length={_data.Length}");

			return _data[Position++];
		}


		public ushort ReadUInt16()
		{
			ushort val = (ushort)(_data[Position] << 8 | _data[Position + 1]);
			Position += 2;
			return val;
		}

		public byte[] ReadBytes(int len)
		{
			if (Position + len > _data.Length)
				throw new InvalidOperationException(
					$"Not enough data. Need {len} bytes, have {_data.Length - Position}");

			var b = _data.Skip(Position).Take(len).ToArray();
			Position += len;
			return b;
		}

		public string ReadAscii(int len)
			=> Encoding.ASCII.GetString(ReadBytes(len));

		public decimal ReadAmount(int byteLen)
		{
			byte[] bytes = ReadBytes(byteLen);
			// Convert bytes to decimal amount (last 2 digits are cents)
			long amount = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				amount = (amount << 8) | bytes[i];
			}
			return amount / 100m;
		}
	}
}
