using System;

namespace LegalityChecker
{
	public class PtPPData : PPData
	{
		public override HT4 Gen4Game{ get{ return HT4.Pt; } }

		public PtPPData( PokemonData pkm ) : base( pkm )
		{
		}

		public static new PPData GetPalParkData( PokemonData pkm )
		{
			switch ( pkm.Language )
			{
				default: return null;
				case Language.Japanese: return new JapPtPPData( pkm );
				case Language.English: return new EngPtPPData( pkm );
				case Language.French: return new FrePtPPData( pkm );
				case Language.German: return new GerPtPPData( pkm );
				case Language.Italian: return new ItaPtPPData( pkm );
				case Language.Spanish: return new SpaPtPPData( pkm );
				case Language.Korean: if ( pkm.Nicknamed ) return new KorPtPPData( pkm ) else return null;
			}
		}
	}
}