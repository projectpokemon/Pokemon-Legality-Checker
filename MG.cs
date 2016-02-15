using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public enum MGiftType : byte
	{
		None,
		Static,
		StaticHacked,
		Dynamic,
		DynamicHacked,
		DynamicShiny,
		DynamicShinyHacked
	}

	[Flags]
	public enum DistRegions : byte
	{
		NonKorean	    = 0x01,
		Korean			= 0x02,
		Global			= 0x03
	}

	public class DMG : MG
	{
		public DMG( ushort natid, ushort tid, ushort sid, bool female, DateTime date ) : base( natid, tid, sid, 1, female, date, DistRegions.NonKorean )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, DateTime date, DistRegions regions ) : base( natid, tid, sid, 1, female, HT4.DPPtHGSS, date, regions )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, DateTime date ) : base( natid, tid, sid, 1, female, lang, date, DistRegions.NonKorean )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, DateTime date, DistRegions regions ) : base( natid, tid, sid, 1, female, lang, 3060, date, regions )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, ushort loc, DateTime date ) : base( natid, tid, sid, 1, female, lang, loc, date, DistRegions.NonKorean )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, ushort loc, DateTime date, DistRegions regions ) : base( natid, tid, sid, 1, female, lang, loc, HT4.D, date, regions )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, HT4 ht, DateTime date ) : base( natid, tid, sid, 1, female, lang, ht, date, DistRegions.NonKorean )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, HT4 ht, DateTime date, DistRegions regions ) : base( natid, tid, sid, 1, female, lang, 3060, ht, date, regions )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, ushort loc, HT4 ht, DateTime date ) : base( natid, tid, sid, 1, female, lang, loc, ht, date, DistRegions.NonKorean )
		{
		}

		public DMG( ushort natid, ushort tid, ushort sid, bool female, HT4 lang, ushort loc, HT4 ht, DateTime date, DistRegions regions ) : base( natid, tid, sid, 1, female, lang, loc, ht, date, regions )
		{
		}
	}

	public class MG
	{
		private ushort m_NatID;
		private ushort m_TID;
		private ushort m_SID;
		private uint m_PID;
		private HT4 m_Language;
		private DateTime m_Date;
		private DistRegions m_Regions;
		private bool m_Female;
		private ushort m_Location;
		private HT4 m_HomeTown;

		public ushort NatID{ get{ return m_NatID; } }
		public ushort TID{ get{ return m_TID; } }
		public ushort SID{ get{ return m_SID; } }
		public uint PID{ get{ return m_PID; } }
		public HT4 Language{ get{ return m_Language; } }
		public DateTime Date{ get{ return m_Date; } }
		public DistRegions Regions{ get{ return m_Regions; } }
		public bool Female{ get{ return m_Female; } }
		public ushort Location{ get{ return m_Location; } }
		public HT4 HomeTown{ get{ return m_HomeTown; } }

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, DateTime date ) : this( natid, tid, sid, pid, female, date, DistRegions.NonKorean )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, DateTime date, DistRegions regions ) : this( natid, tid, sid, pid, female, HT4.DPPtHGSS, date, regions )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, DateTime date ) : this( natid, tid, sid, pid, female, lang, date, DistRegions.NonKorean )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, DateTime date, DistRegions regions ) : this( natid, tid, sid, pid, female, lang, 3060, date, regions )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, ushort loc, DateTime date ) : this( natid, tid, sid, pid, female, lang, loc, date, DistRegions.NonKorean )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, ushort loc, DateTime date, DistRegions regions ) : this( natid, tid, sid, pid, female, lang, loc, HT4.D, date, regions )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, HT4 ht, DateTime date ) : this( natid, tid, sid, pid, female, lang, ht, date, DistRegions.NonKorean )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, HT4 ht, DateTime date, DistRegions regions ) : this( natid, tid, sid, pid, female, lang, 3060, ht, date, regions )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, ushort loc, HT4 ht, DateTime date ) : this( natid, tid, sid, pid, female, lang, loc, ht, date, DistRegions.NonKorean )
		{
		}

		public MG( ushort natid, ushort tid, ushort sid, uint pid, bool female, HT4 lang, ushort loc, HT4 ht, DateTime date, DistRegions regions )
		{
			m_NatID = natid;
			m_TID = tid;
			m_SID = sid;
			m_PID = pid;
			m_Language = lang;
			m_Date = date;
			m_Regions = regions;
			m_Female = female;
			m_Location = loc;
			m_HomeTown = ht;
		}

		public bool ValidAcquire( DateTime date, byte town, byte language, ushort natid, bool female, ushort loc )
		{
			/*
			if ( !ValidEvo( natid ) )
				return false;
			*/
			//Console.WriteLine( "Evo - {0} {1}", language, (int)m_Regions );

			if ( female != m_Female )
				return false;

			if ( language == 8 )
			{
				if ( ( m_Regions & DistRegions.Korean ) == 0 )
					return false;
			}
			else if ( ( m_Regions & DistRegions.NonKorean ) == 0 )
				return false;

			if ( loc != m_Location )
				return false;

			if ( ( ( ( m_HomeTown & HT4.HGSS ) != 0 ) && town != 7 ) || ( ( ( m_HomeTown & HT4.DP ) != 0 ) && town != 10 ) ) //Gold or Diamond
				return false;

			DateTime[] releasedates = null;

			switch ( town )
			{
				default:
					return false;
				case 10:
				{
					if ( ( m_HomeTown & HT4.DP ) != 0 )
						releasedates = LegitCheck.m_DPDates;
					else if ( ( m_HomeTown & HT4.Pt ) != 0 )
						releasedates = LegitCheck.m_PtDates;
					else
						return false;
					break;
				}
				case 7:
				{
					if ( ( m_HomeTown & HT4.HGSS ) != 0 )
						releasedates = LegitCheck.m_HGSSDates;
					else
						return false;
					break;
				}
			}

			//Console.WriteLine( "Date Arrays" );

			DateTime releasedate = DateTime.Now;

			switch ( language )
			{
				default:
					return false;
				case 1: //Japan
				{
					releasedate = releasedates[0];
					break;
				}
				case 2: //USA (UK is English, so we cannot evaluate)
				{
					releasedate = releasedates[1];
					break;
				}
				case 3: case 4: case 5: case 7: //EU (Non-English)
				{
					releasedate = releasedates[2];
					break;
				}
				case 8: //Korean
				{
					releasedate = releasedates[3];
					break;
				}
			}

			//Console.WriteLine( "Release Dates" );

			return date >=releasedate;
		}

		public bool ValidEvo( ushort natid )
		{
			return m_NatID == natid || ( natid != LegitCheck.m_EvoChains[natid] && ValidEvo( LegitCheck.m_EvoChains[natid] ) );
		}

		public static readonly MG[] m_StaticGifts = new MG[]
		{
			new MG( 125, 11256, 25152, 93432704, false, HT4.DP, 3029, new DateTime( 2006, 11, 25 ) ),
			new MG( 126, 11256, 18298, 12610642, false, HT4.DP, 3029, new DateTime( 2006, 11, 25 ) ),
			new MG( 441, 10286, 57436, 19014063, false, HT4.DP, 3000, new DateTime( 2006, 10, 28 ) ),
			new MG( 25, 12146, 56585, 433683475, false, HT4.DP, new DateTime( 2006, 12, 14 ) ),
			new MG( 25, 8107, 20846, 611193775, false, HT4.DP, new DateTime( 2007, 8, 10 ) ),
			new MG( 4, 7207, 12105, 170598041, false, HT4.DP, 3053, new DateTime( 2007, 7, 20 ) ),
			new MG( 466, 12077, 31297, 225658278, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 466, 12077, 41731, 225658278, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 466, 12077, 23335, 225658278, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 466, 6257, 47618, 225658278, false, HT4.DP, 3000, new DateTime( 2007, 6, 25 ) ),
			new MG( 224, 10147, 31254, 98612, true, HT4.DP, new DateTime( 2007, 10, 14 ) ),
			new MG( 448, 7157, 56541, 611193990, false, HT4.DP, 3062, new DateTime( 2007, 7, 15 ) ),
			new MG( 467, 12077, 12473, 225558290, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 467, 12077, 20088, 225558290, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 467, 12077, 57092, 225558290, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 467, 12077, 43212, 225558290, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 467, 6257, 2565, 225558290, false, HT4.DP, 3000, new DateTime( 2007, 6, 25 ) ),
			new MG( 490, 7157, 41056, 611193975, true, HT4.DP, 3062, new DateTime( 2007, 7, 15 ) ),
			new MG( 151, 7157, 33606, 611193974, false, HT4.DP, 3062, new DateTime( 2007, 7, 15 ) ),
			new MG( 25, 12077, 2384, 433683475, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 25, 12077, 33009, 433683475, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 25, 12077, 31209, 433683475, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 25, 12077, 59606, 433683475, false, HT4.DP, 3000, new DateTime( 2007, 12, 7 ) ),
			new MG( 25, 6257, 44870, 433683475, false, HT4.DP, 3000, new DateTime( 2007, 6, 25 ) ),
			new MG( 466, 11157, 31946, 522625512, false, HT4.DP, new DateTime( 2007, 11, 15 ) ),
			new MG( 467, 12017, 17582, 522625525, false, HT4.DP, new DateTime( 2007, 12, 1 ) ),
			new MG( 350, 12157, 37705, 237949230, true, HT4.DP, new DateTime( 2007, 12, 15 ) ),
			new MG( 357, 2027, 62861, 611193963, true, HT4.DP, new DateTime( 2007, 3, 2 ) ),
			new MG( 340, 3217, 51799, 801742146, false, HT4.DP, new DateTime( 2007, 3, 21 ) ),
			new MG( 25, 5308, 7501, 564976218, false, HT4.DP, new DateTime( 2008, 5, 30 ) ),
			new MG( 25, 10278, 59322, 564976218, false, HT4.DP, new DateTime( 2008, 10, 27 ) ),
			new MG( 448, 8178, 6406, 10848703, false, HT4.DP, new DateTime( 2008, 8, 17 ) ),
			new MG( 4, 7208, 27372, 170598039, false, HT4.DP, 3053, new DateTime( 2008, 7, 20 ) ),
			new MG( 447, 3208, 17806, 111595912, false, HT4.DP, 3001, new DateTime( 2008, 3, 20 ) ),
			new MG( 149, 11088, 47337, 237829116, false, HT4.DP, new DateTime( 2008, 11, 8 ) ),
			new MG( 25, 12268, 7242, 84717515, false, HT4.DPPt, 3058, new DateTime( 2008, 12, 26 ) ),
			new MG( 133, 12068, 21977, 79481876, true, HT4.DPPt, 3052, new DateTime( 2008, 12, 6 ) ),
			new MG( 447, 3208, 45450, 111595912, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new MG( 447, 3208, 16165, 111595912, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new MG( 447, 3208, 23382, 111595912, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new MG( 447, 3208, 33861, 111595912, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new MG( 447, 3208, 64847, 111595912, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new MG( 465, 10038, 61139, 198025102, false, HT4.DP, new DateTime( 2008, 10, 3 ), DistRegions.Korean ),
			new MG( 466, 6298, 21589, 522625512, false, HT4.DP, new DateTime( 2008, 6, 29 ), DistRegions.Korean ),
			new MG( 467, 6298, 6457, 522625525, false, HT4.DP, new DateTime( 2008, 6, 29 ), DistRegions.Korean ),
			new MG( 149, 1158, 11342, 237829116, false, HT4.DP, new DateTime( 2008, 1, 15 ) ),
			new MG( 373, 2158, 12900, 237829104, false, HT4.DP, new DateTime( 2008, 2, 15 ) ),
			new MG( 350, 10128, 7252, 3160311710, false, HT4.Pt, new DateTime( 2008, 10, 12 ) ),
			new MG( 25, 10108, 57139, 84717515, false, HT4.DPPt, 3058, new DateTime( 2008, 10, 10 ) ),
			new MG( 25, 2079, 55619, 764998357, false, HT4.DPPt, 3052, new DateTime( 2009, 2, 7 ) ),
			new MG( 52, 3209, 52529, 557566638, false, HT4.DPPt, 3052, new DateTime( 2009, 3, 20 ) ),
			new MG( 350, 5099, 14128, 705629910, false, HT4.Pt, new DateTime( 2009, 5, 9 ) ),
			new MG( 172, 6199, 13193, 1062147313, true, HT4.DPPt, 3007, new DateTime( 2009, 6, 19 ) ),
			new MG( 461, 8159, 46299, 347683463, false, HT4.DPPt, new DateTime( 2009, 8, 15 ), DistRegions.Global ),
			new MG( 350, 5309, 48136, 624987635, false, HT4.DPPt, new DateTime( 2009, 5, 30 ) ),
			new MG( 350, 5309, 32473, 1152855760, false, HT4.DPPt, new DateTime( 2009, 5, 30 ) ),
			new MG( 350, 5309, 35642, 2050352560, false, HT4.DPPt, new DateTime( 2009, 5, 30 ) ),
			new MG( 4, 7209, 34107, 1665328979, false, HT4.DPPt, 3053, new DateTime( 2009, 7, 20 ) ),
			new MG( 25, 7249, 36178, 771667902, false, HT4.DPPt, new DateTime( 2009, 7, 24 ), DistRegions.Korean ),
			new MG( 446, 7049, 38410, 355626007, false, HT4.DPPt, new DateTime( 2009, 7, 4 ), DistRegions.Korean ),
			new MG( 349, 7049, 4277, 755624545, false, HT4.DPPt, new DateTime( 2009, 7, 4 ), DistRegions.Korean ),
			new MG( 25, 10039, 46553, 1457899204, false, HT4.DPPtHGSS, new DateTime( 2009, 10, 3 ) ),
			new MG( 390, 9129, 63029, 128912416, false, HT4.DPPtHGSS, 3056, new DateTime( 2009, 9, 12 ) ),
			new MG( 25, 11219, 28540, 356458068, false, HT4.DPPtHGSS, HT4.HG, new DateTime( 2009, 11, 21 ) ),
			new MG( 172, 1300, 52721, 1158581738, true, HT4.DPPt, new DateTime( 2010, 01, 30 ) ),
            new MG( 133, 1110, 51199, 843837925, false, HT4.HGSS, new DateTime( 2010, 01, 11 ) ), //wcs eevee
            new MG( 172, 3050, 17366, 2010660838, true, HT4.HGSS, new DateTime( 2010, 03, 5 ) ), //spanish spring 2010 pichu
            new MG( 172, 12179, 20300, 1611137238, false, HT4.HGSS, new DateTime( 2009, 12, 17 ), DistRegions.Korean ), //korean spring 2010 pichu
            new MG( 172, 3050, 28194, 251882188, true, HT4.HGSS, new DateTime( 2010, 03, 5 ) ), //italian spring 2010 pichu
            new MG( 172, 3050, 57103, 1132042138, true, HT4.HGSS, new DateTime( 2010, 03, 5 ) ), //german spring 2010 pichu
            new MG( 172, 3050, 12961, 1707039988, true, HT4.HGSS, new DateTime( 2010, 03, 5 ) ), //french spring 2010 pichu
            new MG( 172, 3050, 56955, 672333188, true, HT4.HGSS, new DateTime( 2010, 03, 5 ) ), //english spring 2010 pichu
		    new MG( 133, 5080, 49497, 1609534825, false, HT4.HGSS, new DateTime( 2010, 05, 8 ) ), //vgc eevee english
            new MG( 133, 5080, 21368, 2116763275, false, HT4.HGSS, new DateTime( 2010, 06, 5 ) ), //vgc eevee german
            new MG( 244, 6180, 27198, 664819128, false, HT4.DPPtHGSS, new DateTime( 2010, 6, 18 ) ), //crown entei
            new MG( 243, 6180, 62989, 1177004044, false, HT4.DPPtHGSS, new DateTime( 2010, 6, 18 ) ), //crown raikou
            new MG( 245, 6180, 58746, 99023032, false, HT4.DPPtHGSS, new DateTime( 2010, 6, 18 ) ), //crown suicune
            new MG( 4, 7200, 58364, 253336400, false, HT4.DPPtHGSS, new DateTime( 2010, 7, 20 ) ), //japan birthday charmander
            new MG( 25, 9120, 11976, 1832799900, false, HT4.DPPtHGSS, new DateTime( 2010, 9, 12 ) ), //japan birthday chimchar
            new MG( 350, 7210, 7655, 878785960, false, HT4.HGSS, new DateTime( 2010, 7, 21 ), DistRegions.Korean ), //char fair milotic
            new MG( 490, 8110, 64718, 81105308, true, HT4.DPPtHGSS, new DateTime( 2010, 8, 14 ) ), //NZONE Manaphy
            new MG( 169, 8150, 15269, 1699284360, false, HT4.DPPtHGSS, new DateTime( 2010, 8, 15 ) ), //worlds 2010 crobat
            new MG( 243, 9180, 28031, 439112844, false, HT4.DPPtHGSS, new DateTime( 2010, 9, 18 ), DistRegions.Korean ), //korean crown raikou
            new MG( 485, 3060, 4100, 1065989092, false, HT4.DPPtHGSS, new DateTime( 2010, 10, 14 ) ),//english ranger 3 heatran
            new MG( 485, 3060, 6817, 1410402292, false, HT4.DPPtHGSS, new DateTime( 2010, 10, 14 ) ),//french ranger 3 heatran
            new MG( 485, 3060, 41818, 1676573392, false, HT4.DPPtHGSS, new DateTime( 2010, 10, 14 ) ),//german ranger 3 heatran
            new MG( 485, 3060, 14167, 107219692, false, HT4.DPPtHGSS, new DateTime( 2010, 10, 14 ) ),//italian ranger 3 heatran
            new MG( 485, 3060, 17615, 169776792, false, HT4.DPPtHGSS, new DateTime( 2010, 10, 14 ) ),//spanish ranger 3 heatran
            new MG( 485, 3060, 63645, 1348338592, false, HT4.DPPtHGSS, new DateTime( 2010, 10, 14 ) ),//japanese ranger 3 heatran
        };

		public static readonly DateTime[] m_PtDates = new DateTime[]
		{
			new DateTime( 2008, 09, 13 ), //Japan
			new DateTime( 2009, 03, 22 ), //North Am
			new DateTime( 2009, 05, 22 ), //Europe
			new DateTime( 2009, 07, 02 ), //Korea
		};

		public static readonly DateTime[] m_HGSSDates = new DateTime[]
		{
			new DateTime( 2009, 09, 12 ), //Japan
			new DateTime( 2010, 03, 14 ), //North Am
			new DateTime( 2010, 06, 24 ), //Europe
			new DateTime( 2010, 08, 05 ) //Korea
		};

		public static readonly MG[] m_DynamicGifts = new MG[]
		{
			new DMG( 490, 12226, 7700, true, HT4.DP, 3000, new DateTime( 2006, 12, 22 ) ),
			new DMG( 385, 7077, 41928, false, HT4.DP, 3074, new DateTime( 2007, 7, 7 ) ),
			new DMG( 385, 7077, 28320, false, HT4.DP, new DateTime( 2007, 7, 7 ) ),
			new DMG( 490, 7157, 6367, true, HT4.DP, new DateTime( 2007, 7, 15 ) ),
			new DMG( 490, 9297, 60303, true, HT4.DP, new DateTime( 2007, 9, 29 ) ),
			new DMG( 151, 7157, 49168, false, HT4.DP, new DateTime( 2007, 7, 15 ) ),
			new DMG( 151, 7157, 13805, false, HT4.DP, new DateTime( 2007, 7, 15 ) ),
			new DMG( 151, 7157, 61127, false, HT4.DP, new DateTime( 2007, 7, 15 ) ),
			new DMG( 151, 7157, 43910, false, HT4.DP, new DateTime( 2007, 7, 15 ) ),
			new DMG( 151, 7157, 45830, false, HT4.DP, new DateTime( 2007, 7, 15 ) ),
			new DMG( 386, 7147, 47015, false, HT4.DP, 3005, new DateTime( 2007, 7, 14 ) ),
			new DMG( 491, 7147, 29045, true, HT4.DP, 3005, new DateTime( 2007, 7, 14 ) ),
			new DMG( 490, 10187, 36666, true, HT4.DP, new DateTime( 2007, 10, 18 ) ),
			new DMG( 490, 11077, 40919, true, HT4.DP, new DateTime( 2007, 11, 7 ) ),
			new DMG( 490, 10017, 16757, true, HT4.DP, new DateTime( 2007, 10, 1 ) ),
			new DMG( 491, 11088, 59899, false, HT4.DP, new DateTime( 2008, 11, 8 ), DistRegions.Korean ),
			new DMG( 386, 8308, 13183, false, HT4.DP, new DateTime( 2008, 8, 30 ), DistRegions.Korean ),
			new DMG( 490, 3298, 52652, false, HT4.DP, new DateTime( 2008, 3, 29 ), DistRegions.Korean ),
			new DMG( 384, 5318, 2526, false, HT4.DP, new DateTime( 2008, 5, 31 ), DistRegions.Korean ),
			new DMG( 491, 3208, 28204, false, HT4.DP, 3001, new DateTime( 2008, 3, 20 ) ),
			new DMG( 491, 10308, 61209, true, HT4.DP, new DateTime( 2008, 10, 30 ) ),
			new DMG( 491, 7038, 22261, true, HT4.DP, new DateTime( 2008, 7, 3 ) ),
			new DMG( 491, 7038, 44409, true, HT4.DP, new DateTime( 2008, 7, 3 ) ),
			new DMG( 491, 7038, 30791, true, HT4.DP, new DateTime( 2008, 7, 3 ) ),
			new DMG( 491, 7038, 22857, true, HT4.DP, new DateTime( 2008, 7, 3 ) ),
			new DMG( 491, 7038, 20313, true, HT4.DP, new DateTime( 2008, 7, 3 ) ),
			new DMG( 491, 7038, 45156, true, HT4.DP, new DateTime( 2008, 7, 3 ) ),
			new DMG( 491, 5318, 10413, true, HT4.DP, new DateTime( 2008, 5, 31 ) ),
			new DMG( 491, 3208, 21867, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new DMG( 491, 3208, 11046, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new DMG( 491, 3208, 26945, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new DMG( 491, 3208, 22806, false, HT4.DP, new DateTime( 2008, 3, 20 ) ),
			new DMG( 491, 3208, 46999, false, HT4.DP, 3001, new DateTime( 2008, 3, 20 ) ),
			new DMG( 386, 6218, 31630, false, HT4.DP, new DateTime( 2008, 6, 21 ) ),
			new DMG( 492, 7198, 50784, true, HT4.DP, 3006, new DateTime( 2008, 7, 19 ) ),
			new DMG( 486, 3089, 29347, false, HT4.DP, new DateTime( 2008, 3, 8 ) ),
			new DMG( 492, 2089, 14103, false, HT4.DP, new DateTime( 2008, 2, 20 ) ),
			new DMG( 385, 8188, 41454, true, HT4.DP, 3074, new DateTime( 2008, 8, 18 ) ),
			new DMG( 486, 7198, 29382, false, HT4.DP, 3006, new DateTime( 2008, 7, 19 ) ),
			new DMG( 492, 4019, 34011, false, HT4.DP, new DateTime( 2009, 4, 1 ) ),
			new DMG( 492, 4019, 49952, false, HT4.DP, new DateTime( 2009, 4, 1 ) ),
			new DMG( 151, 3219, 36366, false, HT4.DP, new DateTime( 2009, 3, 21 ), DistRegions.Korean ),
			new DMG( 492, 4019, 45671, false, HT4.DP, new DateTime( 2009, 4, 1 ) ),
			new DMG( 385, 6199, 35776, true, HT4.DPPt, 3073, new DateTime( 2009, 6, 19 ) ),
			new DMG( 493, 7189, 2831, false, HT4.DPPt, 3007, new DateTime( 2009, 7, 18 ) ),
			new DMG( 486, 7189, 38437, false, HT4.DPPt, new DateTime( 2009, 7, 18 ) ),
			new DMG( 486, 7189, 26575, false, HT4.DPPt, new DateTime( 2009, 7, 18 ) ),
			new DMG( 486, 7189, 21084, false, HT4.DPPt, new DateTime( 2009, 7, 18 ) ),
			new DMG( 486, 7189, 50753, false, HT4.DPPt, new DateTime( 2009, 7, 18 ) ),
			new DMG( 492, 7249, 58953, false, HT4.DPPt, new DateTime( 2009, 7, 24 ), DistRegions.Korean ),
			new DMG( 486, 6209, 15181, false, HT4.DP, new DateTime( 2009, 6, 20 ), DistRegions.Korean ),
			new DMG( 59, 6069, 55303, false, HT4.DP, new DateTime( 2009, 6, 6 ), DistRegions.Korean ),
			new DMG( 493, 11059, 29950, false, HT4.DPPt, new DateTime( 2009, 11, 5 ) ),
			new DMG( 493, 11079, 62276, false, HT4.DPPt, new DateTime( 2009, 11, 7 ) ),
			new DMG( 151, 11219, 60403, false, HT4.HGSS, 3073, HT4.HG, new DateTime( 2009, 11, 11 ) ),
			new DMG( 151, 11219, 50545, false, HT4.DPPtHGSS, 3073, HT4.HG, new DateTime( 2010, 01, 29 ) ),
			new DMG( 385, 2270, 54369, true, HT4.DPPt, new DateTime( 2010, 02, 27 ) ),
			new DMG( 385, 1300, 44886, true, HT4.DPPt, new DateTime( 2010, 01, 30 ), DistRegions.Korean ),
            new DMG( 493, 2010, 58750, false, HT4.DPPt, new DateTime( 2010, 02, 17 ) ), //michina arceus - german
            new DMG( 493, 2010, 25598, false, HT4.DPPt, new DateTime( 2010, 02, 17 ) ), //michina arceus - italian
            new DMG( 493, 2010, 22297, false, HT4.DPPt, new DateTime( 2010, 02, 22 ) ), //michina arceus - france
            new DMG( 493, 2010, 56468, false, HT4.DPPt, new DateTime( 2010, 02, 26 ) ), //michina arceus - spain
            new DMG( 493, 12249, 28562, false, HT4.DPPt, new DateTime( 2009, 12, 24 ), DistRegions.Korean ), //michina(cinema) arceus - korea
		    new DMG( 385, 3010, 32238, true, HT4.DPPt, new DateTime( 2010, 2, 27 ) ), //pklatam jirachi(latin america)
            new DMG( 492, 5010, 58953, false, HT4.DPPtHGSS, new DateTime( 2010, 5, 1 ), DistRegions.Korean ),//time square shaymin, nok
            new DMG( 385, 6030, 36733, true, HT4.DPPtHGSS, new DateTime( 2010, 6, 6 ) ), //smr2010 aus night sky's edge jirachi
            new DMG( 385, 6260, 48628, true, HT4.DPPtHGSS, new DateTime( 2010, 6, 26 ) ), //smr2010 UK night sky's edge jirachi
            new DMG( 385, 6260, 49062, true, HT4.DPPtHGSS, new DateTime( 2010, 6, 26 ) ), //smr2010 french night sky's edge jirachi
            new DMG( 385, 6260, 55068, true, HT4.DPPtHGSS, new DateTime( 2010, 6, 26 ) ), //smr2010 german night sky's edge jirachi
            new DMG( 385, 6260, 43570, true, HT4.DPPtHGSS, new DateTime( 2010, 6, 26 ) ), //smr2010 italian night sky's edge jirachi
            new DMG( 385, 6260, 30810, true, HT4.DPPtHGSS, new DateTime( 2010, 6, 26 ) ), //smr2010 spanish night sky's edge jirachi
            new DMG( 212, 6180, 12346, false, HT4.DPPtHGSS, new DateTime( 2010, 6, 18 ) ), //japan goon's scizor
            new DMG( 251, 7100, 38491, true, HT4.DPPtHGSS, new DateTime( 2010, 7, 10 ) ), //japan eigakan celebi
            new DMG( 25, 7150, 50165, false, HT4.DPPtHGSS, new DateTime( 2010, 7, 15 ) ), //japan satoshi's pika
            new DMG( 386, 3060, 1859, false, HT4.DPPtHGSS, new DateTime( 2010, 10, 4 ) ),//english ranger 3 deoxys(attack)
            new DMG( 386, 3060, 28479, false, HT4.DPPtHGSS, new DateTime( 2010, 11, 5 ) ),//french ranger 3 deoxys(attack)
            new DMG( 386, 3060, 21463, false, HT4.DPPtHGSS, new DateTime( 2010, 11, 5 ) ),//german ranger 3 deoxys(attack)
            new DMG( 386, 3060, 22392, false, HT4.DPPtHGSS, new DateTime( 2010, 11, 5 ) ),//italian ranger 3 deoxys(attack)
            new DMG( 386, 3060, 14447, false, HT4.DPPtHGSS, new DateTime( 2010, 11, 5 ) ),//spanish ranger 3 deoxys(attack)
            new DMG( 386, 3060, 20191, false, HT4.DPPtHGSS, new DateTime( 2010, 3, 6 ) ),//japanese ranger 3 deoxys(attack)
        };

		public static readonly MG[] m_DynamicShinyGifts = new MG[]
		{
		};

		public static MG FindStaticGift( ushort natid, uint tid, uint sid )
		{
			for (int i = 0;i < m_StaticGifts.Length; i++ )
			{
				MG entry = m_StaticGifts[i];
				if ( entry.ValidEvo( natid ) && entry.TID == tid && entry.SID == sid )
				{
					//Console.WriteLine( "Entry: {0}", i );
					return entry;
				}
			}

			return null;
		}

		public static MG FindDynamicGift( ushort natid, uint tid, uint sid )
		{
			for (int i = 0;i < m_DynamicGifts.Length; i++ )
			{
				MG entry = m_DynamicGifts[i];
				if ( entry.ValidEvo( natid ) && entry.TID == tid && entry.SID == sid )
					return entry;
			}

			return null;
		}

		public static MG FindDynamicShinyGift( ushort natid, uint tid, uint sid )
		{
			for (int i = 0;i < m_DynamicShinyGifts.Length; i++ )
			{
				MG entry = m_DynamicShinyGifts[i];
				if ( entry.ValidEvo( natid ) && entry.TID == tid && entry.SID == sid )
					return entry;
			}

			return null;
		}
	}
}