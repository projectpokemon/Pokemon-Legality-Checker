using System;

namespace LegalityChecker
{
	public class EggPokemonTemplate : BasePokemonTemplate
	{
		private ushort m_EggLocation;

		public EggPokemonTemplate( byte hometown, ushort natid, byte minlevel, ushort egglocation )
			: base( hometown, natid, 0, minlevel )
		{
			m_EggLocation = egglocation;
		}

		public override bool IsMatch( PokemonData data )
		{
			return base.IsMatch( data ) && ( data.EggLocation == m_EggLocation || data.EggLocation == 2002 );
		}
	}
}