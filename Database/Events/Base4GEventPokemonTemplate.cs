using System;

namespace LegalityChecker
{
	public class Base4GEventPokemonTemplate : BaseEventPokemonTemplate
	{
		private DateTime m_StartDate; //Starting Date
		private ushort m_Location; //Some are not Pokemon Event (3060)

		public Base4GEventPokemonTemplate( byte hometown, ushort natid, byte minlevel, ushort tid, ushort sid, bool otfemale, PokeString otname, ushort location, DateTime startdate )
			: base( hometown, natid, minlevel, tid, sid, otfemale, otname )
		{
			m_StartDate = startdate;
			m_Location = location;
		}

		public override bool IsMatch( PokemonData data )
		{
			return base.IsMatch( data ) && data.Location == location && data.Ribbons.Contains( Ribbon.Classic ) && data.PokeBall == PokeBall.Cherish
				&& m_StartDate <= data.Date;
		}
	}
}