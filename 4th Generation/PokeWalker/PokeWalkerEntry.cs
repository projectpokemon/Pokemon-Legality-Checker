using System;

namespace LegalityChecker
{
	public class PokeWalkerEntry
	{
		private ushort m_NatID;
		private bool m_Female; //I understand that some will not apply.
		private byte m_Level;

		public ushort NatID{ get{ return m_NatID; } }
		public bool Female{ get{ return m_Female; } }
		public byte Level{ get{ return m_Level; } }

		public PokeWalkerEntry( ushort natid, bool female, byte level )
		{
			m_NatID = natid;
			m_Female = female;
			m_Level = level;
		}
	}
}