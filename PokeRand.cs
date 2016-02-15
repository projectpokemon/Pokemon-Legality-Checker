using System;

namespace LegalityChecker
{
	public class PokePRNG
	{
		private uint m_Seed;
		public uint Seed{ get{ return m_Seed; } set{ m_Seed = value; } }
		public uint HighBits{ get{ return m_Seed >> 0x10; } }
		public uint LowBits{ get{ return m_Seed & 0xFFFF; } }

		public uint NextNum()
		{
			m_Seed = Next();
			return m_Seed;
		}

		public uint PrevNum()
		{
			m_Seed = Prev();
			return m_Seed;
		}

		public PokePRNG SetNext()
		{
			NextNum();
			return this;
		}

		public PokePRNG SetPrev()
		{
			PrevNum();
			return this;
		}

		public uint Next()
		{
			return ( ( m_Seed * 0x41C64E6Du ) + 0x6073u );
		}

		public uint Prev()
		{
			return ( ( m_Seed * 0xEEB9EB65u ) + 0xA3561A1u );
		}

		public PokePRNG( uint seed )
		{
			m_Seed = seed;
		}

		public PokePRNG() : this( 0 )
		{
		}
	}
}