using System;

namespace LegalityChecker
{
	public class DPPPData : PPData
	{
		public override HT4 Gen4Game{ get{ return HT4.DP; } }

		public DPPPData( PokemonData pkm ) : base( pkm )
		{
		}

		public static new PPData GetPalParkData( PokemonData pkm )
		{
			switch ( pkm.Language )
			{
				default: return null;
				case Language.Japanese: return new JapDPPPData( pkm );
				case Language.English: return new EngDPPPData( pkm );
				case Language.French: return new FreDPPPData( pkm );
				case Language.German: return new GerDPPPData( pkm );
				case Language.Italian: return new ItaDPPPData( pkm );
				case Language.Spanish: return new SpaDPPPData( pkm );
				case Language.Korean: if ( pkm.Nicknamed ) return new KorDPPPData( pkm ) else return null;
			}
		}
	}
}