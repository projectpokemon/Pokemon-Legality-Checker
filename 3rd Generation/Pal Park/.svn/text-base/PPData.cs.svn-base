using System;

namespace LegalityChecker
{
	[Flags]
	public enum SlotFlag : byte
	{
		None		= 0x00,
		Slot1	= 0x01,
		Slot2	= 0x02,
		Slot3	= 0x04,
		Slot4	= 0x08,
		Slot5	= 0x10,
		Slot6	= 0x20,
		AnySlot	= Slot1 | Slot2 | Slot3 | Slot4 | Slot5 | Slot6
	}

	public abstract class PPData
	{
		private TrashByteEntry[] m_Slot1Entries;
		private TrashByteEntry[] m_Slot2Entries;

		private byte[][] m_SlotCheck = new byte[5][]; //2-6 slot check

		private byte[] m_NameBytes;
		private byte[] m_OTNameBytes;

		private bool m_IsValid;
		private bool m_SetBytes; //does it have a set byte (its not text)
		private SlotFlag m_SlotFlag;
		private int m_Slot1IndexFound;
		private int m_Slot2IndexFound;

		public bool IsValid{ get{ return m_IsValid; } }
		public SlotFlag SlotFlag{ get{ return m_SlotFlag; } }
		public bool SetBytes{ get{ return m_SetBytes; } }

		public bool IsSlot1{ get{ return ( m_SlotFlag & SlotFlag.Slot1 ) != 0; } }
		public bool IsSlot2{ get{ return ( m_SlotFlag & SlotFlag.Slot2 ) != 0; } }
		public bool IsSlot3{ get{ return ( m_SlotFlag & SlotFlag.Slot3 ) != 0; } }
		public bool IsSlot4{ get{ return ( m_SlotFlag & SlotFlag.Slot4 ) != 0; } }
		public bool IsSlot5{ get{ return ( m_SlotFlag & SlotFlag.Slot5 ) != 0; } }
		public bool IsSlot6{ get{ return ( m_SlotFlag & SlotFlag.Slot6 ) != 0; } }
		public bool IsAnySlot{ get{ return m_SlotFlag == SlotFlag.AnySlot; } }

		public abstract HT4 Gen4Game{ get; }

		public PPData( PokemonData pkm )
		{
			m_NameBytes = new byte[22];
			m_OTNameBytes = new byte[16];

			Array.Copy( pkm.Data, 0x48, m_NameBytes, 0, 22 );
			Array.Copy( pkm.Data, 0x68, m_OTNameBytes, 0, 16 );
		}

		public void Checks()
		{
			//Determine the last set of FFFF.  We find this going backwards.
			int termindex = -1;

			for ( int i = m_NameBytes.Length-2; termindex == -1 && i >= 0; i-=2 )
				if ( m_NameBytes[i] == 0xFF && m_NameBytes[i+1] == 0xFF )
					termindex = i;

			if ( termindex < 2 ) //This should technically be checked BEFORE doing pal parking.
			{
				m_IsValid = false;
				return; //There are no valid terminators
			}

			//Determine a valid name based on valid characters for a particular table (Japanese, English, etc).

			if ( termindex < 0x5C )
			{
				bool invalid = false;

				for ( int j = 0;j < m_Slot1Entries.Length; j++ )
				{
					//Check Slot1
					for ( int i = termindex+=2; !invalid && i < m_NameBytes.Length; i+=2 )
						if ( !m_Slot1Entries[j].IsWithin( i-4, m_NameBytes[i] ) )
							invalid = true;

					if ( !invalid )
					{
						m_SlotFlag |= SlotFlag.Slot1;
						m_Slot1IndexFound = j;
						break; //Short circuit
					}
				}

				invalid = false;

				for ( int j = 0;j < m_Slot1Entries.Length; j++ )
				{
					//Check Slot2-6
					for ( int i = termindex+=2; !invalid && i < m_NameBytes.Length; i+=2 )
						if ( !m_Slot2Entries[j].IsWithin( i-4, m_NameBytes[i] ) )
							invalid = true;

					if ( !invalid )
					{
						m_Slot2IndexFound = j;
						for ( int i = 0; i < m_SlotCheck.Length; i++ )
						{
							for ( int j = 0; j < m_SlotCheck[i].Length; j++ )
							{
								if ( m_NameBytes[0x9] == m_SlotCheck[i][j] )
								{
									m_SlotFlag |= (SlotFlag)(2 << i);
									break;
								}
							}
						}
					}
				}
			}
			else
				m_SlotFlag = SlotFlag.Any; //Indeterminate, still need to check OTName

			if ( m_SlotFlag > SlotFlag.None ) //We have something valid! Lets verify the OTName
			{
				//Determine the last set of FFFF.  We find this going backwards.
				termindex = -1;

				for ( int i = m_OTNameBytes.Length-2; termindex == -1 && i >= 0; i-=2 )
					if ( m_OTNameBytes[i] == 0xFF && m_OTNameBytes[i+1] == 0xFF )
						termindex = i;

				if ( termindex < 2 ) //This should technically be checked BEFORE doing pal parking.
				{
					m_IsValid = false;
					return; //There are no valid OTName terminators
				}

				for ( int i = termindex+=2; i < m_OTNameBytes.Length; i++ )
				{
					if ( m_OTNameBytes[i] != m_NameBytes[i] ) //Must be the same!
					{
						m_IsValid = false;
						return;
					}
				}
			}
		}

		internal void GetTrashByteEntry_Slot1( params string[] slot1format )
		{
			m_Slot1Entries = new TrashByteEntry[slot1format.Length];
			for ( int i = 0;i < slot1format.Length; i++ )
				m_Slot1Entries[i] = new TrashByteEntry( slot1format );
		}

		internal void GetTrashByteEntry_Slot2( params string[] slot2format )
		{
			m_Slot2Entries = new TrashByteEntry[slot2format.Length];
			for ( int i = 0;i < slot2format.Length; i++ )
				m_Slot2Entries[i] = new TrashByteEntry( slot2format );
		}

		internal void SetSlotCheck( int index, params byte[] bytes )
		{
			m_SlotCheck[index] = bytes;
		}

		public static PPData GetPalParkData( PokemonData pkm )
		{
			ushort platloc = pkm.ToUInt16( 0x46 );

			if ( platloc == 55 ) //Platinum/HG-SS Location
			{
				if ( pkm.Data[0x86] == 4 ) //HG/SS PokeBall
					m_PalParkData = HGSSPPData.GetPalParkData( pkm );
				else if ( pkm.Data[0x86] == 0 ) //Not Invalid HG/SS Pokeball
					m_PalParkData = PtPPData.GetPalParkData( pkm );
			}
			else if ( platloc == 0 ) //Not invalid Plat/HG-SS Location
				m_PalParkData = DPPPData.GetPalParkData( pkm );

			return null;
		}
	}
}