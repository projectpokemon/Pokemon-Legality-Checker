using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class LgPokemonData : 3GPokemonData
	{
		public LgPokemonData( byte[] data ) : base( data )
		{
		}
	}
}