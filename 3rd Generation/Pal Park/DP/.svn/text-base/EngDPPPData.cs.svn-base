using System;

namespace LegalityChecker
{
	public class EngDPPPData : DPPPData
	{
		private static readonly string m_Slot1 = "18,20,0D,02,42,00,00,00,00,00,00,00,48,A1,0C,02,E0,FF";
		private static readonly string m_Slot2 = "05,00,00,00,00-FF,8E-96,27,02,00-FF,D1-D2,26,02,E9,9A,06,02,00,00";

		public EngDPPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1 );
			GetTrashByteEntry_Slot2( m_Slot2 );
		}
	}
}