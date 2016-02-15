using System;

namespace LegalityChecker
{
	public class ItaDPPPData : DPPPData
	{
		private static readonly string m_Slot1 = "54,20,0D,02,42,00,00,00,00,00,00,00,84,A1,0C,02,E0,FF";
		private static readonly string m_Slot2 = "05,00,00,00,00-FF,8E-96,27,02,00-FF,D3-D4,26,02,25,9B,06,02,00,00";

		public ItaDPPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1 );
			GetTrashByteEntry_Slot2( m_Slot2 );
		}
	}
}