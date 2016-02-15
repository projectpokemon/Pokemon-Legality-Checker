using System;

namespace LegalityChecker
{
	public class EngPtPPData : PtPPData
	{
		private static readonly string m_Slot1_1 = "00,00,00,00,42,00,00,00,00,00,00,00,C8,19,0C,02,E0,FF";
		private static readonly string m_Slot1_2 = "00,00,00,00,00,00,00,00,00,00,C8,19,0C,02,E0,FF,03,02";
		private static readonly string m_Slot2_1 = "02-03,00,00,00,00-FF,A8-AD,28,02,00-FF,E0-E1,27,02,4D,75,07,02,00,00";
		private static readonly string m_Slot2_2 = "2C-2D,01,00,00,00-FF,A8-AD,28,02,00-FF,E0-E1,27,02,4D,75,07,02,00,00";
						//byte #8/#9 is wild within the pal parked set.
						//byte #6 variable:
						/*
							A8,A9,AA,AB,AC
							A9,AA,AA,AB,AC
							A9,AA,AB,AC,AD
							A9,AA,AB,AC,AC
							A9,AA,AB,AB,AC
						*/

		public EngPtPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1_1, m_Slot1_2 );
			GetTrashByteEntry_Slot2( m_Slot2_1, m_Slot2_2 );

			SetSlotCheck( 0, 0xA8, 0xA9 );
			SetSlotCheck( 1, 0xA9, 0xAA );
			SetSlotCheck( 2, 0xAA, 0xAB );
			SetSlotCheck( 3, 0xAB, 0xAC );
			SetSlotCheck( 4, 0xAC, 0xAD );
		}
	}
}