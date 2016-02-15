using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class SP
	{
		private ushort m_NatID;
		private ushort m_TID;
		private ushort m_SID;
		private uint m_PID;
		private uint m_IVs;
		private byte m_HomeTown;
		private bool m_Female;

		public ushort NatID{ get{ return m_NatID; } }
		public ushort TID{ get{ return m_TID; } }
		public ushort SID{ get{ return m_SID; } }
		public uint PID{ get{ return m_PID; } }
		public uint IVs{ get{ return m_IVs; } }
		public byte HomeTown{ get{ return m_HomeTown; } set{ m_HomeTown = value; } }
		public bool Female{ get{ return m_Female; } }

		public SP( ushort natid, ushort tid, ushort sid, uint pid, uint ivs, bool female, byte hometown )
		{
			m_NatID = natid;
			m_TID = tid;
			m_SID = sid;
			m_PID = pid;
			m_IVs = ivs;
			m_Female = female;
			m_HomeTown = hometown;
		}

		public static readonly SP3[] m_3GSP = new SP3[]
		{
			new SP3( 222, 50183, 0, 1284967295, 0x8A421484, true, HT3.RS ), //Corsola - LANE//COROSO
			new SP3( 296, 49562, 0, 40000, 0x884210A5, false, HT3.RS ), //Makuhita - ELYSSA//MAKIT
			new SP3( 300, 2259, 0, 1233792535, 0x8842905, true, HT3.RS ), //Skitty - DARRELL//SKITIT

			new SP3( 116, 46285, 0, 127, 0x88521085, false, HT3.E ), //Horsea - SKYLAR//SEASOR
			new SP3( 52, 25945, 0, 139, 0x884290A4, true, HT3.E ), //Meowth - ISIS//MEOWOW
			new SP3( 311, 8460, 1, 111, 0x88529084, false, HT3.E ), //Plusle - ROMAN//PLUSES
			new SP3( 273, 38726, 0, 132, 0x88421485, false, HT3.E ), //Seedot - KOBE//DOTS

			new SP3( 101, 50298, 0, 104075286, 0xA79CCA13, false, HT3.FrLg ), //Electrode - CLIFTON//ESPHERE
			new SP3( 83, 8810, 0, 353977303, 0xA8FC5734, false, HT3.FrLg ), //Farfetch'd - ELYSSA//CH’DING
			new SP3( 124, 36728, 0, 1233792541, 0xAB9B4A32, false, HT3.FrLg ), //Jynx - DONTAE//ZYNX
			new SP3( 108, 1239, 0, 1158875307, 0xAB77D678, false, HT3.FrLg ), //Lickitung - HADEN//MARC
			new SP3( 122, 1985, 0, 40110, 0xAD7C45F4, false, HT3.FrLg ), //Mr.Mime - REYLEY//MIMIEN
			new SP3( 86, 9853, 0, 1210887305, 0xAD7859F8, false, HT3.FrLg ), //Seel - GARETT//SEELOR
			new SP3( 114, 60042, 0, 1551363322, 0xA9786636, true, HT3.FrLg ), //Tangela - NORMA//TANGENY

			new SP3( 29, 63184, 0, 1284967305, 0xACF9E656, true, HT3.Fr ), //Nidoran-F - MS.NIDO//SAIGE
			new SP3( 30, 13637, 0, 15649301, 0x26FB4B39, false, HT3.Fr ), //Nidorina - TURNOR//NINA

			new SP3( 32, 63184, 0, 1284967326, 0x9F6B4B33, true, HT3.Lg ), //Nidoran-M - MR.NIDO//SAIGE
			new SP3( 33, 13637, 0, 15649305, 0xACFB6653, false, HT3.Lg ), //Nidorino - TURNER//NINO
		};

		public static readonly SP4[] m_4GSP = new SP4[]
		{
			new SP4( 63, 25643, 0, 142, 0xB39A3DEF, true, HT4.DPPt ), //Abra - Hilary//Kazza
			new SP4( 441, 44142, 0, 2151, 0x9F9CBE8F, false, HT4.DPPt ), //Chatot - Norton//Charap
			new SP4( 93, 19248, 0, 136, 0x9EFCBF34, true, HT4.DPPt ), //Haunter - Mindy//Gaspar
			new SP4( 129, 53277, 0, 1116, 0x9F9A3F2F, false, HT4.DPPt ), //Magikarp - Meister//Foppa (GERMAN)

			new SP4( 21, 1001, 0 , 27486, 0xA94A3E8F, false, 183, HT4.HGSS ), //Spearow - Route 35
			new SP4( 25, 33038, 0, 4741238, 0x9B9FCB34, false, HT4.HGSS ), //Pikachu - Lt.Surge
			new SP4( 85, 283, 0, 130058, 0x9EF7D294, true, HT4.HGSS ), //Dodrio
			new SP4( 66, 37460, 0, 8976, 0x9EFA532F, false, HT4.HGSS ), //Machop
			new SP4( 82, 50082, 0, 53558, 0xA94A3E8F, false, HT4.HGSS ), //Magneton
			new SP4( 95, 48926, 0, 9711, 0x9EF7E699, false, HT4.HGSS ), //Onix
			new SP4( 178, 15616, 0, 13540, 0xA94A3E8F, false, HT4.HGSS ), //Xatu
			new SP4( 374, 23478, 0, 1226452, 0xB38BE3BC, false, HT4.HGSS ), //Bedlum
		};

		public static readonly SPJ[] m_JSP = new SPJ[]
		{
			new SPJ( 23, 24680, 13330, 3923358231, 0x3CE59B9A, true, 14, HT3.Fr ), //Ekans
			new SPJ( 90, 24680, 13330, 1516074102, 0x1656CA65, true, 24, HT3.Fr ), //Shellder
			new SPJ( 43, 24680, 13330, 1212000899, 0x13585317, true, 26, HT3.Fr ), //Oddish
			new SPJ( 54, 24680, 13330, 3954095834, 0x3FD7321F, true, 27, HT3.Fr ), //Psyduck
			new SPJ( 58, 24680, 13330, 3601012542, 0x2811730B, true, 32, HT3.Fr ), //Growlithe

			new SPJ( 27, 13579, 26437, 559901056, 0x03FCA2E4, false, 12, HT3.Lg ), //Sandshrew
			new SPJ( 120, 13579, 26437, 3937382660, 0x0789586A, false, 18, HT3.Lg ), //Staryu
			new SPJ( 37, 13579, 26437, 1705041524, 0x1B9B0CCF, false, 18, HT3.Lg ), //Vulpix
			new SPJ( 94, 13579, 26437, 578476325, 0x22ED81D3, false, 23, HT3.Lg ), //Gengar
			new SPJ( 128, 13579, 26437, 2778102381, 0x0B1D326E, false, 25, HT3.Lg ), //Tauros
			new SPJ( 79, 13579, 26437, 1510360139, 0x0B454D71, false, 31, HT3.Lg ), //Slowpoke
			new SPJ( 68, 13579, 26437, 4102829892, 0x1F4566E9, false, 38, HT3.Lg ), //Machamp
		};

		public bool ValidHomeTown( byte hometown )
		{
				switch ( hometown )
				{
					default:
						return false;
					case 1:
					{
						if ( ( m_HomeTown & (byte)HT3.R ) != 0 )
							return true;
						break;
					}
					case 2:
					{
						if ( ( m_HomeTown & (byte)HT3.S ) != 0 )
							return true;
						break;
					}
					case 3:
					{
						if ( ( m_HomeTown & (byte)HT3.E ) != 0 )
							return true;
						break;
					}
					case 4:
					{
						if ( ( m_HomeTown & (byte)HT3.Fr ) != 0 )
							return true;
						break;
					}
					case 5:
					{
						if ( ( m_HomeTown & (byte)HT3.Lg ) != 0 )
							return true;
						break;
					}

					case 10:
					{
						if ( ( m_HomeTown & (byte)HT4.D ) != 0 )
							return true;
						break;
					}
					case 11:
					{
						if ( ( m_HomeTown & (byte)HT4.P ) != 0 )
							return true;
						break;
					}
					case 12:
					{
						if ( ( m_HomeTown & (byte)HT4.Pt ) != 0 )
							return true;
						break;
					}
					case 7:
					{
						if ( ( m_HomeTown & (byte)HT4.HG ) != 0 )
							return true;
						break;
					}
					case 8:
					{
						if ( ( m_HomeTown & (byte)HT4.SS ) != 0 )
							return true;
						break;
					}
				}
				return false;
		}

		public bool ValidEvo( ushort natid )
		{
			return m_NatID == natid || ( natid != LegitCheck.m_EvoChains[natid] && ValidEvo( LegitCheck.m_EvoChains[natid] ) );
		}

		public static bool ValidJeremy( ushort natid, uint tid, uint sid, uint pid, uint ivscom, bool female, byte level, byte hometown )
		{
			for (int i = 0;i < m_JSP.Length; i++ )
			{
				SPJ entry = m_JSP[i];
				if ( entry.ValidEvo( natid ) && entry.TID == tid && entry.SID == sid && entry.PID == pid && entry.IVs == ivscom && entry.Female == female && entry.Level <= level && entry.ValidHomeTown( hometown ) )
					return true;
			}

			return false;
		}

		public static bool Valid3rdGen( ushort natid, uint tid, uint sid, uint pid, uint ivscom, bool female, byte hometown )
		{
			for (int i = 0;i < m_3GSP.Length; i++ )
			{
				SP3 entry = m_3GSP[i];
				if ( entry.ValidEvo( natid ) && entry.TID == tid && entry.SID == sid && entry.PID == pid && entry.IVs == ivscom && entry.Female == female && entry.ValidHomeTown( hometown ) )
					return true;
			}

			return false;
		}

		public static bool Valid4thGen( ushort natid, uint tid, uint sid, uint pid, uint ivscom, bool female, byte hometown, ushort loc )
		{
			for (int i = 0;i < m_4GSP.Length; i++ )
			{
				SP4 entry = m_4GSP[i];
				if ( entry.ValidEvo( natid ) && entry.TID == tid && entry.SID == sid && entry.PID == pid && entry.IVs == ivscom && entry.Female == female && entry.ValidHomeTown( hometown ) && entry.Location == loc )
					return true;
			}

			return false;
		}

		public class SPJ : SP3 //3rd gen
		{
			private byte m_Level;
			
			public byte Level{ get{ return m_Level; } }

			public SPJ( ushort natid, ushort tid, ushort sid, uint pid, uint ivs, bool female, byte level, HT3 hometown ) : base( natid, tid, sid, pid, ivs, female, hometown )
			{
				m_Level = level;
			}
		}

		public class SP3 : SP //3rd gen
		{
			public SP3( ushort natid, ushort tid, ushort sid, uint pid, uint ivs, bool female, HT3 hometown ) : base( natid, tid, sid, pid, ivs, female, (byte)hometown )
			{
			}
		}

		public class SP4 : SP //4th gen
		{
			private ushort m_Location;
			public ushort Location{ get{ return m_Location; } }

			public SP4( ushort natid, ushort tid, ushort sid, uint pid, uint ivs, bool female, HT4 hometown ) : this( natid, tid, sid, pid, ivs, female, 2001, hometown )
			{
			}

			public SP4( ushort natid, ushort tid, ushort sid, uint pid, uint ivs, bool female, ushort location, HT4 hometown ) : base( natid, tid, sid, pid, ivs, female, (byte)hometown )
			{
				m_Location = location;
			}
		}
	}
}