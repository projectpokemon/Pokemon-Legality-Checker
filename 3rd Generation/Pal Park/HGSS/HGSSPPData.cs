using System;

namespace LegalityChecker
{
	public class HGSSPPData : PPData
	{
		public override HT4 Gen4Game{ get{ return HT4.HGSS; } }

		public HGSSPPData( PokemonData pkm ) : base( pkm )
		{
		}

		public static new PPData GetPalParkData( PokemonData pkm )
		{
			switch ( pkm.Language )
			{
				default: return null;
				case Language.Japanese: return new JapHGSSPPData( pkm );
				case Language.English: return new EngHGSSPPData( pkm );
				case Language.French: return new FreHGSSPPData( pkm );
				case Language.German: return new GerHGSSPPData( pkm );
				case Language.Italian: return new ItaHGSSPPData( pkm );
				case Language.Spanish: return new SpaHGSSPPData( pkm );
				case Language.Korean: if ( pkm.Nicknamed ) return new KorHGSSPPData( pkm ) else return null;
			}
		}
	}
}