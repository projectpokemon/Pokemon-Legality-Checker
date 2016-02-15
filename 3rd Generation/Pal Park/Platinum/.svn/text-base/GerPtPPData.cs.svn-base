using System;

namespace LegalityChecker
{
	public class GerPtPPData : PtPPData
	{
		private static readonly string m_Slot1_1 = "00,00,00,00,42,00,00,00,00,00,00,00,6C,1A,0C,02,E0,FF";
		private static readonly string m_Slot1_1 = "00,00,00,00,00,00,00,00,00,00,6C,1A,0C,02,E0,FF,03,02";
		private static readonly string m_Slot2_1 = "03,00,00,00,00-FF,A9-AF,28,02,00-FF,E2-E3,27,02,ED,75,07,02,00,00";
		private static readonly string m_Slot2_2 = "2C-2D,01,00,00,00-FF,A9-AF,28,02,00-FF,E2-E3,27,02,ED,75,07,02,00,00";
						//byte #8/#9 is wild within the pal parked set.
						//byte #6 variable:
						/*
							A9,AA,AA,AB,AC
							A9,AA,AB,AC,AD
							A9,AA,AB,AC,AC
							A9,AA,AB,AB,AC
							AA,AB,AB,AC,AD
							AA,AB,AC,AC,AD
							AA,AB,AC,AD,AD
							AA,AB,AC,AD,AE
							AB,AC,AC,AD,AE
							AB,AC,AD,AD,AE
							AB,AC,AD,AE,AF
						*/

		public GerPtPPData( PokemonData pkm ) : base( pkm )
		{
			GetTrashByteEntry_Slot1( m_Slot1_1, m_Slot1_2 );
			GetTrashByteEntry_Slot2( m_Slot2_1, m_Slot2_2 );

			SetSlotCheck( 0, 0xA9, 0xAA, 0xAB );
			SetSlotCheck( 1, 0xAA, 0xAB, 0xAC );
			SetSlotCheck( 2, 0xAA, 0xAB, 0xAC, 0xAD );
			SetSlotCheck( 3, 0xAB, 0xAC, 0xAD, 0xAE );
			SetSlotCheck( 4, 0xAC, 0xAD, 0xAE, 0xAF );
		}
	}
}