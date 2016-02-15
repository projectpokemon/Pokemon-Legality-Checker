using System;

namespace LegalityChecker
{
	public class BasePokemonTemplate
	{
		private byte m_HomeTown;
		private ushort m_NatID;
		private byte m_MinLevel;

		public BasePokemonTemplate( byte hometown, ushort natid, byte minlevel )
		{
			m_HomeTown = hometown;
			m_NatID = natid;
			m_MinLevel = minlevel;
		}

		public virtual bool IsMatch( PokemonData data )
		{ //Utility.IsMatch() checks for evolution situations such as Eevee -> Umbreon and Acquisition Level + 1.
			return data.HomeTown == m_HomeTown && Utility.IsMatch( data.NatID, m_NatID, data.m_MinLevel, m_MinLevel );
		}

		public virtual bool IsValidGender( PokemonData data )
		{
			return true;
		}

		public virtual bool IsValidPokeBall( PokemonData data )
		{
			return true;
		}

		public virtual bool IsValidAlgorithm( PokemonData data )
		{
			return true;
		}

		public virtual bool IsValidRibbons( PokemonData data )
		{
			return true; //Some ribbons are REQUIRED
		}

		public virtual bool IsValidFatefulEncounter( PokemonData data )
		{
			return true; //All 4G events require this flag, some 3rd gen, some not.
		}

		public virtual bool IsValidLanguage( PokemonData data )
		{
			return true;
		}
	}
}