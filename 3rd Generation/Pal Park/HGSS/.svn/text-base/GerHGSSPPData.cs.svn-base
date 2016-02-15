using System;

namespace LegalityChecker
{
	public class GerHGSSPPData : HGSSPPData
	{
		private static readonly string m_Slot1 = "00,00,00,00,42,00,00,00,00,00,00,00,8C,15,0D,02,E0,FF";
		private static readonly string m_Slot2 = "03,00,00,00,00-FF,75-7A,28,02,00-FF,C1-C2,27,02,05,18,07,02,00,00";
						//byte #8/#9 is wild within the pal parked set.
						//byte #6 variable:
						/*
							75,76,77,78,79
							76,77,77,78,79
							76,77,78,78,79
							76,77,78,79,7A
						*/

		public GerHGSSPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry( m_Slot1, m_Slot2 );

			SetSlotCheck( 0, 0x75, 0x76 );
			SetSlotCheck( 1, 0x76, 0x77 );
			SetSlotCheck( 2, 0x77, 0x78 );
			SetSlotCheck( 3, 0x78, 0x79 );
			SetSlotCheck( 4, 0x79, 0x7A );
		}
	}
}