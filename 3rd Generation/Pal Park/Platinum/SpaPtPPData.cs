using System;

namespace LegalityChecker
{
	public class SpaPtPPData : PtPPData
	{
		private static readonly string m_Slot1_1 = "00,00,00,00,42,00,00,00,00,00,00,00,6C,1A,0C,02,E0,FF";
		private static readonly string m_Slot1_2 = "00,00,00,00,00,00,00,00,00,00,6C,1A,0C,02,E0,FF,03,02";
		private static readonly string m_Slot2_1 = "02-03,00,00,00,00-FF,AB-B0,28,02,00-FF,E3-E4,27,02,ED,75,07,02,00,00";
		private static readonly string m_Slot2_2 = "2C-2D,01,00,00,00-FF,AB-B0,28,02,00-FF,E3-E4,27,02,ED,75,07,02,00,00";
						//byte #8/#9 is wild within the pal parked set.
						//byte #6 variable:
						/*
							AB, AC, AD, AE, AF
							AC, AC, AD, AE, AF
							AC, AD, AD, AE, AF
							AC, AD, AE, AF, B0
						*/

		public SpaPtPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1_1, m_Slot1_2 );
			GetTrashByteEntry_Slot2( m_Slot2_1, m_Slot2_2 );

			SetSlotCheck( 0, 0xAB, 0xAC );
			SetSlotCheck( 1, 0xAC, 0xAD );
			SetSlotCheck( 2, 0xAD, 0xAE );
			SetSlotCheck( 3, 0xAE, 0xAF );
			SetSlotCheck( 4, 0xAF, 0xB0 );
		}
	}
}