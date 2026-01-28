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

		public string GetHexString(int startPos, int length)
		{
			return string.Join(" ", _data.Skip(startPos).Take(length).Select(b => b.ToString("X2")));
		}

		public decimal ReadAmount(int byteLen)
		{
			byte[] bytes = ReadBytes(byteLen);
			// For hex 00 00 00 05 34 32, treat as BCD: 05 34 32 = 534.32
			long amount = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				byte b = bytes[i];
				amount = amount * 100 + ((b >> 4) * 10) + (b & 0x0F);
			}
			return amount / 100m;
		}
	}
}
