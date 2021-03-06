using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public enum Language : byte
	{
		None			= 0,
		Japanense		= 1,
		English		= 2,
		French		= 3,
		Italian		= 4,
		German		= 5,
		Invalid		= 6,
		Spanish		= 7,
		Korean		= 8,
		Indeterminate	= 0xFF;
	}

	[Flags]
	public enum LangFlag : byte
	{
		None			= 0,
		Japanense		= 0x01,
		English		= 0x02,
		French		= 0x04,
		Italian		= 0x08,
		German		= 0x10,
		Invalid		= 0x20,
		Spanish		= 0x40,
		Korean		= 0x80,
		JapKor		= 0x81,
		All			= 0xFF
	}

	public enum EncounterType : byte
	{
		Special		= 0, //Hatched, Pal Park, Egg, MG
		TallGrass		= 2,
		DialgaEvent	= 4,
		Cave			= 5, //Hall of Origin
		Water		= 7, //Surfing/Fishing
		Building		= 9,
		SafariZone	= 10, //Great Marsh, etc
		Gift			= 12 //Starter, Gift, Fossil, etc
	}

	public enum PokeBall : byte
	{
		None		= 0,
		MasterBall,
		UltraBall,
		GreatBall,
		PokeBall, //Set this when its HG/SS specific
		SafariBall,
		NetBall,
		DiveBall,
		NestBall,
		RepeatBall,
		TimerBall,
		LuxuryBall,
		PremierBall,
		DuskBall,
		HealBall,
		QuickBall,
		CherishBall,
		//HG/SS Only
		FastBall,
		LevelBall,
		LureBall,
		HeavyBall,
		LovelyBall,
		FriendBall,
		MoonBall,
		CompBall
	}

	[Flags]
	public enum ShinyLeaves : byte
	{
		None			= 0x00,
		Leaf1		= 0x01,
		Leaf2		= ShinyLeaves.Leaf1 | 0x02,
		Leaf3		= ShinyLeaves.Leaf2 | 0x04,
		Leaf4		= ShinyLeaves.Leaf3 | 0x08,
		Leaf5		= ShinyLeaves.Leaf4 | 0x10,
		Crown		= ShinyLeaves.Leaf5 | 0x20
	}

	[Flags]
	public enum Markings : byte
	{
		None			= 0x00,
		Circle		= 0x01,
		Triangle		= 0x02,
		Square		= 0x04,
		Heart		= 0x08,
		Star			= 0x10,
		Diamond		= 0x20
	}

	public abstract class PokemonData
	{
		private bool m_NotSupported;

		private byte[] m_Data;

		private ushort m_PID;
		private ushort m_CheckSum;
		private ushort m_NatID;
		private ushort m_HeldItem;
		private ushort m_TID;
		private ushort m_SID;
		private uint m_Exp;
		private byte[] m_EVs;
		private byte[] m_ContestStats;
		private ushort m_Move1;
		private ushort m_Move2;
		private ushort m_Move3;
		private ushort m_Move4;
		private uint m_IVsCom;
		private bool m_Nicknamed;
		private bool m_IsEgg;
		private ushort m_PlatLocation;
		private ushort m_PlatEggLocation;
		private PokeString10 m_Name;
		private PokeString7 m_OTName;
		private DateTime m_EggDate;
		private DateTime m_CapDate;
		private ushort m_EggLocation;
		private ushort m_Location;
		private bool m_OTFemale;
		private byte m_MetAtLevel;
		private byte m_GenderThresh;
		private Gender m_Gender;

		public bool NotSupported{ get{ return m_NotSupported; } set{ m_NotSupported = value; } }
		public byte[] Data{ get{ return m_Data; } }

		public uint PID{ get{ return m_PID; } }
		public byte Nature{ get{ return (byte)(m_PID%25); } }

		public bool HasValidByte04{ get{ return m_Data[0x04] == 0; } }
		public bool HasValidByte05{ get{ return m_Data[0x05] == 0; } }

		public ushort NatID{ get{ return m_NatID; } }
		public ushort TID{ get{ return m_TID; } }
		public ushort SID{ get{ return m_SID; } }

		public byte Happiness{ get{ return m_Data[0x14]; } }
		public Ability Ability{ get{ return (Ability)m_Data[0x15]; } }
		public Markings Markings{ get{ return (Markings)m_Data[0x16]; } }
		public byte Language{ get{ return m_Data[0x17]; } }

		#region Sinnoh Ribbons-1
		public bool HasSinnohChampRibbon{ get{ return (m_Data[0x24] & 0x01) != 0; } }
		public bool HasAbilityRibbon{ get{ return (m_Data[0x24] & 0x02) != 0; } }
		public bool HasGreatAbilityRibbon{ get{ return (m_Data[0x24] & 0x04) != 0; } }
		public bool HasDoubleAbilityRibbon{ get{ return (m_Data[0x24] & 0x08) != 0; } }
		public bool HasMultiAbilityRibbon{ get{ return (m_Data[0x24] & 0x10) != 0; } }
		public bool HasPairAbilityRibbon{ get{ return (m_Data[0x24] & 0x20) != 0; } }
		public bool HasWorldAbilityRibbon{ get{ return (m_Data[0x24] & 0x40) != 0; } }
		public bool HasAlertRibbon{ get{ return (m_Data[0x24] & 0x80) != 0; } }

		public bool HasShockRibbon{ get{ return (m_Data[0x25] & 0x01) != 0; } }
		public bool HasDowncastRibbon{ get{ return (m_Data[0x25] & 0x02) != 0; } }
		public bool HasCarelessRibbon{ get{ return (m_Data[0x25] & 0x04) != 0; } }
		public bool HasRelaxRibbon{ get{ return (m_Data[0x25] & 0x08) != 0; } }
		public bool HasSnoozeRibbon{ get{ return (m_Data[0x25] & 0x10) != 0; } }
		public bool HasSmileRibbon{ get{ return (m_Data[0x25] & 0x20) != 0; } }
		public bool HasGorgeousRibbon{ get{ return (m_Data[0x25] & 0x40) != 0; } }
		public bool HasRoyalRibbon{ get{ return (m_Data[0x25] & 0x80) != 0; } }

		public bool HasGorgeousRoyalRibbon{ get{ return (m_Data[0x26] & 0x01) != 0; } }
		public bool HasFootprintRibbon{ get{ return (m_Data[0x26] & 0x02) != 0; } }
		public bool HasRecordRibbon{ get{ return (m_Data[0x26] & 0x04) != 0; } }
		public bool HasHistoryRibbon{ get{ return (m_Data[0x26] & 0x08) != 0; } }
		public bool HasLegendRibbon{ get{ return (m_Data[0x26] & 0x10) != 0; } }
		public bool HasRedRibbon{ get{ return (m_Data[0x26] & 0x20) != 0; } }
		public bool HasGreenRibbon{ get{ return (m_Data[0x26] & 0x40) != 0; } }
		public bool HasBlueRibbon{ get{ return (m_Data[0x26] & 0x80) != 0; } }

		public bool HasFestivalRibbon{ get{ return (m_Data[0x27] & 0x01) != 0; } }
		public bool HasCarnivalRibbon{ get{ return (m_Data[0x27] & 0x02) != 0; } }
		public bool HasClassicRibbon{ get{ return (m_Data[0x27] & 0x04) != 0; } }
		public bool HasPremierRibbon{ get{ return (m_Data[0x27] & 0x08) != 0; } }

		public bool HasInvalidRibbon1{ get{ return (m_Data[0x27] & 0xF0) != 0; } }
		#endregion

		public byte Move1PPLeft{ get{ return m_Data[0x30]; } }
		public byte Move2PPLeft{ get{ return m_Data[0x31]; } }
		public byte Move3PPLeft{ get{ return m_Data[0x32]; } }
		public byte Move4PPLeft{ get{ return m_Data[0x33]; } }

		public byte Move1PPUp{ get{ return m_Data[0x34]; } }
		public byte Move2PPUp{ get{ return m_Data[0x35]; } }
		public byte Move3PPUp{ get{ return m_Data[0x36]; } }
		public byte Move4PPUp{ get{ return m_Data[0x37]; } }

		public uint IV1{ get{ return (uint)( m_IVsCom & 0x7FFF ); } }
		public uint IV2{ get{ return (uint)( m_IVsCom >> 15 ) & 0x7FFF; } }
		public bool IsEgg{ get{ return m_IsEgg; } }
		public bool Nicknamed{ get{ return m_Nicknamed; } }

		#region Hoenn Ribbons-1
		public bool HasCoolRibbon{ get{ return (m_Data[0x3C] & 0x01) != 0; } }
		public bool HasCoolSuperRibbon{ get{ return (m_Data[0x3C] & 0x02) != 0; } }
		public bool HasCoolHyperRibbon{ get{ return (m_Data[0x3C] & 0x04) != 0; } }
		public bool HasCoolMasterRibbon{ get{ return (m_Data[0x3C] & 0x08) != 0; } }
		public bool HasBeautyRibbon{ get{ return (m_Data[0x3C] & 0x10) != 0; } }
		public bool HasBeautySuperRibbon{ get{ return (m_Data[0x3C] & 0x20) != 0; } }
		public bool HasBeautyHyperRibbon{ get{ return (m_Data[0x3C] & 0x40) != 0; } }
		public bool HasBeautyMasterRibbon{ get{ return (m_Data[0x3C] & 0x80) != 0; } }

		public bool HasCuteRibbon{ get{ return (m_Data[0x3D] & 0x01) != 0; } }
		public bool HasCuteSuperRibbon{ get{ return (m_Data[0x3D] & 0x02) != 0; } }
		public bool HasCuteHyperRibbon{ get{ return (m_Data[0x3D] & 0x04) != 0; } }
		public bool HasCuteMasterRibbon{ get{ return (m_Data[0x3D] & 0x08) != 0; } }
		public bool HasSmartRibbon{ get{ return (m_Data[0x3D] & 0x10) != 0; } }
		public bool HasSmartSuperRibbon{ get{ return (m_Data[0x3D] & 0x20) != 0; } }
		public bool HasSmartHyperRibbon{ get{ return (m_Data[0x3D] & 0x40) != 0; } }
		public bool HasSmartMasterRibbon{ get{ return (m_Data[0x3D] & 0x80) != 0; } }

		public bool HasToughRibbon{ get{ return (m_Data[0x3E] & 0x01) != 0; } }
		public bool HasToughSuperRibbon{ get{ return (m_Data[0x3E] & 0x02) != 0; } }
		public bool HasToughHyperRibbon{ get{ return (m_Data[0x3E] & 0x04) != 0; } }
		public bool HasToughMasterRibbon{ get{ return (m_Data[0x3E] & 0x08) != 0; } }
		public bool HasChampionRibbon{ get{ return (m_Data[0x3E] & 0x10) != 0; } }
		public bool HasWinningRibbon{ get{ return (m_Data[0x3E] & 0x20) != 0; } }
		public bool HasVictoryRibbon{ get{ return (m_Data[0x3E] & 0x40) != 0; } }
		public bool HasArtistRibbon{ get{ return (m_Data[0x3E] & 0x80) != 0; } }

		public bool HasEffortRibbon{ get{ return (m_Data[0x3F] & 0x01) != 0; } }
		public bool HasMarineRibbon{ get{ return (m_Data[0x3F] & 0x02) != 0; } }
		public bool HasLandRibbon{ get{ return (m_Data[0x3F] & 0x04) != 0; } }
		public bool HasSkyRibbon{ get{ return (m_Data[0x3F] & 0x08) != 0; } }
		public bool HasCountryRibbon{ get{ return (m_Data[0x3F] & 0x10) != 0; } }
		public bool HasNationalRibbon{ get{ return (m_Data[0x3F] & 0x20) != 0; } }
		public bool HasEarthRibbon{ get{ return (m_Data[0x3F] & 0x40) != 0; } }
		public bool HasWorldRibbon{ get{ return (m_Data[0x3F] & 0x80) != 0; } }
		#endregion

		public ushort Flags{ get{ return m_Data[0x40]; } }
		public bool IsFateful{ get{ return (Flags & 0x1) != 0; } }
		public bool HasMaleFlag{ get{ return (Flags & 0x6) != 0; } }
		public bool HasFemaleFlag{ get{ return ((Flags & 0x2) != 0) && ((m_Flags & 0x4) == 0); } }
		public bool HasGenderlessFlag{ get{ return ((Flags & 0x4) != 0) && ((m_Flags & 0x2) == 0); } }
		public ushort FormeByte{ get{ return (Flags & ~0x7); } }

		public ShinyLeaves ShinyLeaves{ get{ return (ShinyLeaves)m_Data[0x41]; } }

		public bool NoLeaves{ get{ return ShinyLeaves == ShinyLeaves.None; } }
		public bool HasLeaf1{ get{ return ShinyLeaves == ShinyLeaves.Leaf1; } }
		public bool HasLeaf2{ get{ return ShinyLeaves == ShinyLeaves.Leaf2; } }
		public bool HasLeaf3{ get{ return ShinyLeaves == ShinyLeaves.Leaf3; } }
		public bool HasLeaf4{ get{ return ShinyLeaves == ShinyLeaves.Leaf4; } }
		public bool HasLeaf5{ get{ return ShinyLeaves == ShinyLeaves.Leaf5; } }
		public bool Crown{ get{ return ShinyLeaves == ShinyLeaves.Crown; } }

		public bool HasValidByte42{ get{ return m_Data[0x42] == 00; } }
		public bool HasValidByte43{ get{ return m_Data[0x43] == 00; } }

		public ushort PlatLocation{ get{ return m_PlatLocation; } }
		public ushort PlatEggLocation{ get{ return m_PlatEggLocation; } }
		public bool IsHatched{ get{ return EggLocation > 0; } }

		public PokeString10 Name{ get{ return m_Name; } }
		public PokeString7 OTName{ get{ return m_OTName; } }

		public bool HasValidByte5E{ get{ return m_Data[0x5E] == 00; } }

		public byte HomeTown{ get{ return m_Data[0x5F]; } }

		#region Sinnoh Ribbons-2
		public bool HasSinnohCoolRibbon{ get{ return (m_Data[0x60] & 0x01) != 0; } }
		public bool HasSinnohCoolGreatRibbon{ get{ return (m_Data[0x60] & 0x02) != 0; } }
		public bool HasSinnohCoolUltraRibbon{ get{ return (m_Data[0x60] & 0x04) != 0; } }
		public bool HasSinnohCoolMasterRibbon{ get{ return (m_Data[0x60] & 0x08) != 0; } }
		public bool HasSinnohBeautyRibbon{ get{ return (m_Data[0x60] & 0x10) != 0; } }
		public bool HasSinnohBeautyGreatRibbon{ get{ return (m_Data[0x60] & 0x20) != 0; } }
		public bool HasSinnohBeautyUltraRibbon{ get{ return (m_Data[0x60] & 0x40) != 0; } }
		public bool HasSinnohBeautyMasterRibbon{ get{ return (m_Data[0x60] & 0x80) != 0; } }

		public bool HasSinnohCuteRibbon{ get{ return (m_Data[0x61] & 0x01) != 0; } }
		public bool HasSinnohCuteGreatRibbon{ get{ return (m_Data[0x61] & 0x02) != 0; } }
		public bool HasSinnohCuteUltraRibbon{ get{ return (m_Data[0x61] & 0x04) != 0; } }
		public bool HasSinnohCuteMasterRibbon{ get{ return (m_Data[0x61] & 0x08) != 0; } }
		public bool HasSinnohSmartRibbon{ get{ return (m_Data[0x61] & 0x10) != 0; } }
		public bool HasSinnohSmartGreatRibbon{ get{ return (m_Data[0x61] & 0x20) != 0; } }
		public bool HasSinnohSmartUltraRibbon{ get{ return (m_Data[0x61] & 0x40) != 0; } }
		public bool HasSinnohSmartMasterRibbon{ get{ return (m_Data[0x61] & 0x80) != 0; } }

		public bool HasSinnohToughRibbon{ get{ return (m_Data[0x62] & 0x01) != 0; } }
		public bool HasSinnohToughGreatRibbon{ get{ return (m_Data[0x62] & 0x02) != 0; } }
		public bool HasSinnohToughUltraRibbon{ get{ return (m_Data[0x62] & 0x04) != 0; } }
		public bool HasSinnohToughMasterRibbon{ get{ return (m_Data[0x62] & 0x08) != 0; } }

		public bool HasInvalidRibbon2{ get{ return (m_Data[0x62] & 0xF0) != 0; } }
		#endregion

		public bool HasValidByte64{ get{ return m_Data[0x64] == 0; } }
		public bool HasValidByte65{ get{ return m_Data[0x65] == 0; } }
		public bool HasValidByte66{ get{ return m_Data[0x66] == 0; } }
		public bool HasValidByte67{ get{ return m_Data[0x67] == 0; } }

		public ushort Location{ get{ return m_Location; } }
		public ushort EggLocation{ get{ return m_EggLocation; } }

		public byte Pokerus{ get{ return m_Data[0x82]; } }
		public PokeBall PokeBall{ get{ return (PokeBall)m_Data[0x83]; } }
		public byte EncounterType{ get{ return m_Data[0x85]; } }
		public PokeBall HGSSPokeBall{ get{ return (PokeBall)m_Data[0x86]; } }

		public bool HasValidByte87{ get{ return m_Data[0x87] == 0; } }

		public ushort EVsCom{ get{ ushort m_EvsCom = 0; for ( int i = 0; i < m_EVs.Length; i++ ) m_EvsCom+= m_EVs[i]; return m_EVsCom; } }
		public Gender Gender{ get{ return m_Gender; } }
		public byte GenderThresh{ get{ return m_GenderThresh; } }
		public uint Exp{ get{ return m_Exp; } }
		public byte Level{ get{ return m_Level; } }

		private byte[] ParseStats()
		{
			int iv1 = IV1;
			int iv2 = IV2;

			int[] stats = new int[6];

			stats[0] = (byte)(iv1 & 0x1F); //HP
			stats[1] = (byte)((iv1 & 0x3E0) >> 0x5); //Attack
			stats[2] = (byte)((iv1 & 0x7C00) >> 0xA); //Defense

			stats[3] = (byte)(iv2 & 0x1F); //Speed
			stats[4] = (byte)((iv2 & 0x3E0) >> 0x5); //Sp. Attack
			stats[5] = (byte)((iv2 & 0x7C00) >> 0xA); //Sp. Defense

			return stats;
		}

		public static PokemonData GetPokemon( byte[] data )
		{
			//Todo: Check for proper species for that era.  No 3rd gen bidoofs.
			switch ( data[0x5F] )
			{
				case 1: //Sapphire
					return SPokemonData.GetSPokemon( data );
				case 2: //Ruby
					return RPokemonData.GetRPokemon( data );
				case 3: //Emerald
					return EPokemonData.GetEPokemon( data );
				case 4: //Fire Red
					return FrPokemonData.GetFrPokemon( data );
				case 5: //Leaf Green
					return LgPokemonData.GetLgPokemon( data );
				case 7: case 8: //HG/SS
					return HGSSPokemonData.GetHGSSPokemon( data );
				case 10: case 11: //Diamond/Pearl
					return 4GPokemonData.GetDPPokemon( data );
				case 12: //Platinum
					return PlatPokemonData.GetPtPokemon( data );
				case 13: case 14: //Black/White
					return new BWPokemonData( data );
				case 16: //Collo/XD
					return new XDPokemonData( data );
			}

			return null;
		}

		public PokemonData( byte[] data )
		{
			m_Data = data;
			ValidateArray();

			if ( m_Validated )
				Initialize();
		}

		public uint ToUInt32( int index )
		{
			return BitConverter.ToUInt32( m_Data, index );
		}

		public ushort ToUInt16( int index )
		{
			return BitConverter.ToUInt16( m_Data, index );
		}

		public ushort[] ToUInt16Array( int index, int size )
		{
			ushort[] array = new ushort[size];
			for ( int i = 0;i < size; i++ )
				array[i] = BitConverter.ToUInt16( m_Data, index + i );
			
			return array;
		}

		public virtual void Checks() //All 4th gen basic checks
		{
			//We do not check for obvious things!!!
			BasicChecks();
		}

		public virtual void BasicChecks()
		{
			ushort realsum;
			bool validsum = ValidCheckSum( out realsum );
			Utility.Output( "Real Checksum: 0x{0:X2} - ", realsum ); // Checksum: 0x1234
			Utility.OutputValidityLine( validsum, "Valid", "Invalid" ); //Value, "True Text", "False Text"

			Utility.OutputLine( "Pokemon ID: {0} - {1}, {2}, {3}, {4}", m_PID, m_Natures[Nature], OutputGender(), OutputAbility(), IsShiny() ? "Shiny" : "Not Shiny" );

			if ( m_IsEgg || m_IsHatched )
			{
				Utility.Output( "Egg Received: " );
				Utility.Output( ConsoleColor.DarkCyan, m_EggDate.ToString( "MMMM dd, yyyy" ) );

				if ( m_IsHatched )
				{
					Utility.Output( " - Hatched: " );
					Utility.OutputLine( ConsoleColor.DarkCyan, m_CapDate.ToString( "MMMM dd, yyyy" ) );
				}
				else
					Utility.Output( "\n" ); //End the line
			}
			else
			{
				Utility.Output( "Date Captured: " );
				Utility.OutputLine( ConsoleColor.DarkCyan, m_CapDate.ToString( "MMMM dd, yyyy" ) );
			}

			byte[] ivs = ParseStats();

			Utility.OutputLine( "IVs: {0}, {1}, {2}, {3}, {4}, {5}", ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5] );

			Utility.OutputLine( "Trainer ID: {0}", m_TID );
			Utility.OutputLine( "Secret ID: {0}", m_SID );
			Utility.OutputLine( "Trainer Gender: {0}",  m_OTFemale ? "Female" : "Male" );
			Utility.OutputLine( "Hidden Power: {0} - {1}", m_AttackTypes[GetHPType()], GetHPPower() );

			Utility.Output( "Gender Check: " );
			Utility.OutputValidityLine( IsValidGender(), "Valid", "Invalid" );

			Utility.Output( "Effort Values: " );
			Utility.OutputValidityLine( IsValidEffortValues(), "Valid", "Invalid" );

			Utility.Output( "Nicknamed: {0}", m_Nicknamed ? "Yes" : "No" );
			Utility.Output( "Home Town: {0}", OutputHomeTown() );
			Utility.Output( "Game Language: {0}" OutputLanguage() );

			Utility.Output( "Poke Ball: " );
			Utility.OutputValidityLine( IsValidPokeball(), "Valid", "Invalid" );

			Utility.Output( "Shiny Leaves: " );
			Utility.OutputValidityLine( IsValidLeaves(), "Valid", "Invalid" );

			Utility.Output( "Location: " );
			Utility.OutputValidityLine( IsValidLocation(), "Valid", "Invalid" );
		}

		public virtual void Initialize()
		{
			m_PID = ToUInt32( 0x00 );
			m_CheckSum = ToUInt16( 0x6 );
			m_NatID = ToUInt16( 0x08 );

			m_GenderThresh = Utility.GenderThresholds[m_NatID];

			if ( m_GenderThresh != 255 )
				m_Gender = ( m_PID % 256 ) < m_GenderThresh ? Gender.Female : Gender.Male;
			else
				m_Gender = Gender.Genderless;

			m_TID = ToUInt16( 0x0C );
			m_SID = ToUInt16( 0x0E );

			m_Exp = ToUInt32( 0x10 );

			m_Language = m_Data[0x17];

			m_EVs = new byte[6];
			for ( int i = 0; i < m_EVs.Length; i++ )
				m_EVs[i] = m_Data[i+0x18];

			m_IVsCom = ToUInt32( 0x38 );

			m_IsEgg = ( m_IVsCom & 0x40000000u ) != 0;
			m_Nicknamed = ( m_IVsCom & 0x80000000u ) != 0;

			m_PlatEggLocation = ToUInt16( 0x44 );
			m_PlatLocation = ToUInt16( 0x46 );

			m_Name = new PokeString10( ToUInt16Array( 0x48, 11 ) );
			m_OTName = new PokeString7( ToUInt16Array( 0x68, 8 ) );

			try
			{
				m_EggDate = new DateTime( m_Data[0x78] + 2000, m_Data[0x79], m_Data[0x7A] );
			}
			catch ()
			{
				m_EggDate = DateTime.MinValue;
			}

			try
			{
				m_CapDate = new DateTime( m_Data[0x7B] + 2000, m_Data[0x7C], m_Data[0x7D] );
			}
			catch ()
			{
				m_CapDate = DateTime.MinValue;
			}

			m_EggLocation = ToUInt16( 0x7E );
			m_Location = ToUInt16( 0x80 );

			m_OTFemale = (m_Data[0x84] & 0x80) != 0;
			m_MetAtLevel = (m_Data[0x84] &~ 0x80);

			m_Level = IsEgg ? 0 : Utility.GetLevel( m_NatID, m_Exp );
		}

		internal virtual void ValidateArray()
		{
			if ( data.Length == 236 ) //D/P/Pt/HG/SS - Party
			{
				byte[] temp = new byte[136];
				Array.Copy( data, 0, temp, 0, 136 );
				m_Data = temp;
			}
			else if ( data.Length == 220 )
			{
				byte[] temp = new byte[136];
				Array.Copy( data, 0, temp, 0, 136 );
				m_Data = temp;
			}
			else if ( data.Length == 136 )
				m_Data = data;
		}

		public bool IsShiny()
		{
			uint low = m_PID & 0xFFFF;
			uint high = (uint)(m_PID & ~0xFFFF);

			return ( ( m_TID ^ m_SID ) ^ ( (high >> 0x10) ^ low ) ) < 8;
		}

		public bool ValidCheckSum( out ushort realsum )
		{
			long checksum = 0;

			for ( int i = 8; i < 136; i += 2 )
				checksum += ToUInt16( i );

			realsum = (ushort)checksum;

			return realsum == m_CheckSum;
		}

		public bool IsValidGender()
		{
			if ( m_GenderThresh == 255 && HasGenderlessFlag )
				return true;

			if ( ( m_Gender == Gender.Female || m_GenderThresh == 254 ) && HasFemaleFlag )
				return true;

			if ( ( m_Gender == Gender.Male || m_GenderThresh == 0 ) && HasMaleFlag )
				return true;

			return false;
		}

		public bool IsValidEffortValues()
		{
			return EVsCom <= 510;
		}

		public virtual bool IsValidLocation()
		{
			//Add database information
			return true;
		}

		public virtual bool IsValidPokeBall()
		{
			return (byte)m_PokeBall < (byte)HGSSPokeBall.FastBall && m_PokeBall > PokeBall.None;
		}

		public virtual bool IsValidLeaves()
		{
			return NoLeaves || Leaf1 || Leaf2 || Leaf3 || Leaf4 || Leaf5 || Crown;
		}

		public virtual bool IsValidAlgorithm( out string message )
		{
			message = "Valid";
			return true; //No algorithm set
		}

		public virtual bool IsValidRibbons()
		{
			return true; //Split by era, check template for event required ribbons.
		}

		public virtual bool IsValidFatefulEncounter()
		{
			return true; //4G and some 3G events only? In-game legends?
		}

		public string OutputGender()
		{
			switch ( m_Gender )
			{
				default: case Gender.Genderless: return "Genderless";
				case Gender.Female: return "Female";
				case Gender.Male: return "Male";
			}
		}

		public virtual string OutputAbility()
		{
			return ( (m_PID%2) == 0 ) ? "Ability 1" : "Ability 2"
		}

		public int GetHPType()
		{
			byte[] ivs = ParseStats();

			int total = 0;
			for ( int i = 0; i < ivs.Length; i++ )
				if ( ( ivs[i] % 2 ) == 1 )
					total += (int)Math.Pow( 2, i );
			return ( total * 15 ) / 63;
		}

		public int GetHPPower()
		{
			byte[] ivs = ParseStats();

			int total = 0;
			for ( int i = 0; i < ivs.Length; i++ )
				if ( ( ivs[i] % 4 ) > 1 )
					total += (int)Math.Pow( 2, i );
			return ( ( total * 40 ) / 63 ) + 30;
		}

		public ushort[] GetEvoChildren()
		{
			List<ushort> evolist = new List<ushort>();

			evolist.Add( m_NatID );

			bool notfin = true;

			ushort currentid = m_NatID;

			do
			{
				ushort checkid = Utility.EvoChains[currentid];
				if ( checkid != currentid )
				{
					evolist.Add( checkid );
					currentid = checkid;
				}
				else
					notfin = false;
			}
			while ( notfin );

			return evolist.ToArray();
		}

		public string OutputHomeTown()
		{
			return HomeTown > m_HomeTowns.Length ? "Invalid" : m_HomeTowns[HomeTown];
		}

		public string OutputLanguage()
		{
			switch ( m_Language )
			{
				default: return "(none)";
				case Language.Japanese: return "Japanese";
				case Language.English: return "English";
				case Language.French: return "French";
				case Language.Italian: return "Italian";
				case Language.German: return "German";
				case Language.Spanish: return "Spanish";
				case Language.Korean: return "Korean";
			}
		}

		private static readonly string[] m_HomeTowns = new string[]
		{
			"(none)", "Hoenn [Sapphire]", "Hoenn [Ruby]", "Hoenn [Emerald]",
			"Kanto [Fire Red]", "Kanto [Leaf Green]", "Invalid", "Johto [Heart Gold]",
			"Johto [Soul Silver]", "Invalid", "Sinnoh [Diamond]", "Sinnoh [Pearl]",
			"Sinnoh [Platinum]", "Unova [Black]", "Unova [White]", "Colloseum/XD"
		};

		private static readonly string[] m_AttackTypes = new string[]
		{
			"Fighting",
			"Flying",
			"Poison",
			"Ground",
			"Rock",
			"Bug",
			"Ghost",
			"Steel",
			"Fire",
			"Water",
			"Grass",
			"Electric",
			"Psychic",
			"Ice",
			"Dragon",
			"Dark"
		};

		private static readonly string[] m_Natures = new string[]
		{
			"Hardy",
			"Lonely",
			"Brave",
			"Adamant",
			"Naughty",
			"Bold",
			"Docile",
			"Relaxed",
			"Impish",
			"Lax",
			"Timid",
			"Hasty",
			"Serious",
			"Jolly",
			"Naive",
			"Modest",
			"Mild",
			"Quiet",
			"Bashful",
			"Rash",
			"Calm",
			"Gentle",
			"Sassy",
			"Careful",
			"Quirky",
		};
	}
}