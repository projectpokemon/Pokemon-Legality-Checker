using System;

namespace LegalityChecker
{
	public class JapPtPPData : PtPPData
	{
		private static readonly string m_Slot1 = "00,00,00,00,42,00,00,00,00,00,00,00,58,11,0C,02,E0,FF";
		private static readonly string m_Slot2 = "FF,00,00,00,00-FF,9C-A0,28,02,00-FF,D4-D5,27,02,25,6E,07,02,00,00";

		public JapPtPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1 );
			GetTrashByteEntry_Slot2( m_Slot2 );
		}
	}
}