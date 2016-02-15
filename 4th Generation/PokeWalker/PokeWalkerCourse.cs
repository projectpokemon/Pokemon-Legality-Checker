using System;

namespace LegalityChecker
{
	public class PokeWalkerCourse
	{
		private PokeWalkerEntry m_Entries[];
		private LangFlag m_Language;
		
		public LangFlag Language{ get{ return m_Language; } }

		public PokeWalkerCourse( LangFlag lang, params PokeWalkerEntry[] entries )
		{
			m_Language lang;
			m_Entries = entries;
		}

		public PokeWalkerCourse( LangFlag lang, ushort n1, bool f1, byte l1, ushort n2, bool f2, byte l2, ushort n3, bool f3, byte l3, ushort n4, bool f4, byte l4,
			ushort n5, bool f5, byte l5, ushort n6, bool n6, byte l6 )
			: this ( lang, new PokeWalkerEntry( n1, f1, l1 ), new PokeWalkerEntry( n2, f2, l2 ), new PokeWalkerEntry( n3, f3, l3 ), new PokeWalkerEntry( n4, f4, l4 ),
			new PokeWalkerEntry( n5, f5, l5 ), new PokeWalkerEntry( n6, f6, l6 ) )
		{
		}

		public PokeWalkerEntry this[int index] 
		{
			return m_Entries[index];
		}

		public static readonly PokeWalkerCourses = new PokeWalkerCourses[]
			{
				new PokeWalkerCourse( LangFlag.All, 84, true, 8, 115, true, 8, 29, true, 5, 32, false, 5, 16, false, 5, 161, true, 5 ), //Refreshing Field
				new PokeWalkerCourse( LangFlag.All, 69, true, 8, 202, true, 15, 46, true, 5, 48, false, 5, 21, false, 5, 43, false, 5 ), //Noisy Forest
				new PokeWalkerCourse( LangFlag.All, 95, false, 9, 240, false, 9, 66, true, 7, 77, true, 7, 74, true, 8, 163, true, 6 ), //Rugged Road
				new PokeWalkerCourse( LangFlag.All, 54, true, 10, 120, true, 10, 60, false, 7, 70, false, 7, 191, true, 8, 194, false, 6 ), //Beautiful Beach
				new PokeWalkerCourse( LangFlag.All, 81, true, 11, 239, false, 11, 81, true, 8, 198, false, 11, 19, false, 7, 163, false, 7 ), //Suburban Area
				new PokeWalkerCourse( LangFlag.All, 92, true, 15, 238, true, 12, 92, true, 10, 95, false, 10, 41, false, 8, 66, false, 8 ), //Dim Cave
				new PokeWalkerCourse( LangFlag.All, 60, true, 15, 147, true, 10, 90, true, 12, 98, false, 12, 72, true, 9, 118, true, 9 ), //Blue Lake
				new PokeWalkerCourse( LangFlag.All, 63, true, 15, 100, true, 15, 88, false, 13, 109, true, 13, 19, true, 16, 162, false, 15 ), //Town Outskirts
				new PokeWalkerCourse( LangFlag.All, 264, true, 30, 300, true, 30, 313, false, 25, 314, true, 25, 263, true, 17, 265, true, 15 ), //Hoenn Field
				new PokeWalkerCourse( LangFlag.All, 298, true, 20, 320, true, 31, 116, true, 20, 318, true, 26, 118, true, 22, 129, true, 15 ), //Warm Beach
				new PokeWalkerCourse( LangFlag.All, 218, true, 31, 307, false, 32, 111, false, 20, 228, false, 26, 74, false, 29, 77, true, 19 ), //Volcano Path
				new PokeWalkerCourse( LangFlag.All, 351, true, 30, 352, false, 30, 203, true, 28, 234, true, 28, 44, true, 14, 70, false, 13 ), //Treehouse
				new PokeWalkerCourse( LangFlag.All, 105, true, 30, 128, false, 30, 42, false, 33, 177, true, 24, 66, false, 13, 92, true, 15 ), //Scary Cave
				new PokeWalkerCourse( LangFlag.All, 415, false, 30, 439, true, 29, 403, true, 33, 406, false, 30, 399, true, 13, 401, false, 15 ), //Sinnoh Field
				new PokeWalkerCourse( LangFlag.All, 361, true, 28, 459, false, 31, 215, false, 28, 436, true, 20, 179, true, 15, 220, true, 16 ), //Icy Mountain Road
				new PokeWalkerCourse( LangFlag.All, 377, true, 35, 438, false, 30, 114, true, 30, 400, true, 30, 102, true, 17, 179, false, 19 ), //Big Forest
				new PokeWalkerCourse( LangFlag.All, 200, true, 32, 433, true, 31, 93, false, 25, 418, false, 28, 170, true, 17, 223, true, 19 ), //White Lake
				new PokeWalkerCourse( LangFlag.All, 422, true, 30, 456, true, 456, 86, true, 27, 129, true, 30, 54, true, 22, 90, false, 30 ), //Stormy Beach
				new PokeWalkerCourse( LangFlag.All, 25, true, 30, 417, true, 33, 35, true, 31, 39, true, 30, 183, true, 25, 187, true, 25 ), //Resort
				new PokeWalkerCourse( LangFlag.All, 422, false, 31, 446, false, 33, 349, false, 30, 433, true, 26, 42, false, 33, 164, true, 30 ), //Quiet Cave
				new PokeWalkerCourse( LangFlag.All, 120, true, 14, 224, true, 19, 116, false, 15, 222, true, 16, 170, true, 12, 223, false, 14 ), //Beyond the Sea
				new PokeWalkerCourse( LangFlag.All, 35, false, 8, 39, false, 10, 41, false, 9, 163, true, 6, 74, false, 5, 95, true, 5 ), //Night Sky's Edge
				new PokeWalkerCourse( LangFlag.All, 25, false, 15, 25, true, 14, 25 false, 13, 25, true, 12, 25, false, 10, 25, true, 10 ), //Yellow Forest
				new PokeWalkerCourse( LangFlag.Japanese, 441, false, 15, 302, true, 15, 25, true, 10, 453, false, 10, 427, true, 5, 417, false, 5 ), //Rally (EVENT?)
				new PokeWalkerCourse( LangFlag.JapKor, 133, false, 10, 255, false, 10, 61, true, 15, 279, false, 15, 25, true, 8, 52, false, 10 ), //Sightseeing
				new PokeWalkerCourse( LangFlag.All, 374, true, 5, 446, false, 5, 116, false, 5, 355, false, 5, 129, false, 5, 436, true, 5 ), //Winner's Path
				new PokeWalkerCourse( LangFlag.Japanese, 239, false, 5, 240, false, 5, 238, true, 5, 440, true, 5, 173, true, 5, 174, false, 5 ) //Amity Meadow
			};
	}
}