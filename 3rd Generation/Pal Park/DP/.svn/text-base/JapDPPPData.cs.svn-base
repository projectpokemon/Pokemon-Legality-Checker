using System;

namespace LegalityChecker
{
	public class JapDPPPData : DPPPData
	{
		private static readonly string m_Slot1 = "00,00,00,00,B4,C5,0C,02,E0,FF,7F,02,42,00,00,00|08,00,00";
		private static readonly string m_Slot2 = "00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00";

		public JapDPPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1 );
			GetTrashByteEntry_Slot2( m_Slot2 );
		}
	}
}