using System;

namespace LegalityChecker
{
	public class KorDPPPData : DPPPData
	{
		private static readonly string m_Slot1 = "00,00,00,00,42,00,00,00,00,00,00,00,18,77,0C,02,E0,FF";
		private static readonly string m_Slot2 = "05,00,00,00,00-FF,07-0B,28,02,00-FF,4B,27,02,B1,9F,06,02,00,00";

		public KorDPPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1 );
			GetTrashByteEntry_Slot2( m_Slot2 );
		}
	}
}