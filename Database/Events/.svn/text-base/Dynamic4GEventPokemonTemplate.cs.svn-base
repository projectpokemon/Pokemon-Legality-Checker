using System;

namespace LegalityChecker
{
	public class Dynamic4GEventPokemonTemplate : BaseEventPokemonTemplate
	{
		private ushort m_Location; //Some are not Pokemon Event (3060)

		public Dynamic4GEventPokemonTemplate( byte hometown, ushort natid, byte minlevel, ushort tid, ushort sid, bool otfemale, PokeString otname, ushort location )
			: base( hometown, natid, minlevel, tid, sid, otfemale, otname )
		{
			m_Location = location;
		}

		public override bool IsMatch( PokemonData data )
		{
			return base.IsMatch( data ) && data.Location == location /*&& data.Ribbons.Contains( Ribbon.Classic ) && data.PokeBall == PokeBall.Cherish*/;
		}
	}
}