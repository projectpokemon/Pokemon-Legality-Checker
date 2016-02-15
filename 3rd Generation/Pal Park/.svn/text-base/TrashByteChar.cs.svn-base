using System;

namespace LegalityChecker
{
	public struct TrashByteChar
	{
		private byte m_MinChar;
		private byte m_MaxChar;

		public byte MinChar{ get{ return m_MinChar; } }
		public byte MaxChar{ get{ return m_MaxChar; } }

		public TrashByteChar( byte min, byte max )
		{
			m_MinChar = min;
			m_MaxChar = max;
		}

		public bool IsWithin( byte check )
		{
			return check >= min && check <= max;
		}
	}
}