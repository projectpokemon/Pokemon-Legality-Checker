using System;

namespace LegalityChecker
{
	public class WildPokemonTemplate : BasePokemonTemplate
	{
		private ushort m_Location;
		private byte m_EncounterByte; //0x85

		public WildPokemonTemplate( byte hometown, ushort natid, byte minlevel, byte encbyte, ushort location )
			: base( hometown, natid, location, minlevel )
		{
			m_Location = location;
			m_EncounterByte = encbyte;
		}

		public virtual bool IsValidAlgorithm( PokemonData data )
		{
			//Todo - ABCD algorithm.
			return true;
		}

		public virtual bool IsValidRibbons( PokemonData data )
		{
			//Todo - 4G Ribbons only
			return true; //Some ribbons are REQUIRED
		}

		public virtual bool IsValidFatefulEncounter( PokemonData data )
		{
			return !data.IsFateful;
		}
	}
}