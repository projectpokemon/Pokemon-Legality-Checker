using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class BWPokemonData : HGSSPokemonData
	{
		public BWPokemonData( byte[] data ) : base( data )
		{
		}

		internal override void ValidateArray()
		{
			NotSupported = true;
		}

		//Handle Unicode Text, and PokeShifting (same as pal parking)

		public override bool IsValidLeaves()
		{
			return m_Data[0x41] == 0; //They are eliminated in B/W Pokemon
		}
	}
}