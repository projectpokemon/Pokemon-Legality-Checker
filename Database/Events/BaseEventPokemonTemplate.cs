using System;

namespace LegalityChecker
{
	public class BaseEventPokemonTemplate : BasePokemonTemplate
	{
		private ushort m_TID;
		private ushort m_SID;
		private bool m_OTFemale;
		private PokeString m_OTName;

		public BaseEventPokemonTemplate( byte hometown, ushort natid, byte minlevel, ushort tid, ushort sid, bool otfemale, PokeString otname )
			: base( hometown, natid, minlevel )
		{
			m_TID = tid;
			m_SID = sid;
			m_OTName = otname;
		}

		public override bool IsMatch( PokemonData data )
		{
			return base.IsMatch( data ) && data.TID == m_TID && data.SID == m_SID /*&& data.OTName.Equals( otname )*/;
		}
	}
}