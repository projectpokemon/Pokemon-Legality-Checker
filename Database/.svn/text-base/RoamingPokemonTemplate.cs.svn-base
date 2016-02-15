using System;

namespace LegalityChecker
{
	public class RoamingPokemonTemplate : BasePokemonTemplate
	{
		private ushort[] m_Locations;

		public RoamingPokemonTemplate( byte hometown, ushort natid, byte minlevel, params ushort[] locations )
			: base( hometown, natid, minlevel )
		{
			m_Locations = locations;
		}

		public override bool IsMatch( PokemonData data )
		{
			bool match = false;

			for ( int i = 0;!match && i < m_Locations.Length; i++ )
				if ( m_Locations[i] == data.Location )
					match = true;

			return match && base.IsMatch( data );
		}
	}
}