using System;

namespace LegalityChecker
{
	public class WalkerPokemonData : HGSSPokemonData
	{
		public WalkerPokemonData( byte[] data ) : base( data )
		{
		}

		public override bool IsValidPokeBall()
		{
			return PokeBall == PokeBall.PokeBall && HGSSPokeBall == HGSSPokeBall.PokeBall;
		}

		public override bool IsValidAlgorithm( out string message )
		{
			if ( IsValidWalker() )
			{
				message = "Valid PokeWalker";
				return true;
			}
			else
			{
				message = "Hacked PokeWalker";
				return false;
			}
		}

		private bool IsValidWalker()
		{
			byte nature = Nature;

			if ( nature == 24 ) //No Quirkies
				return false;

			PokeWalkerEntry[] entries = null;

			List<uint> validPIDs = new List<uint>();

			if ( entries = GetEntriesFor() )
			{
				if ( entries.Length == 0 )
					return false;

				uint pid_seed = (uint)(((TID ^ SID) >> 8) ^ 0xFF) << 0x18;

				pid_seed += nature - ( pid_seed % 25 );

				if ( GenderThresh > 0 && GenderThresh < 254 ) //Gender Forcing!
				{
					bool female = ( pid_seed % 256 ) < GenderThresh;

					for ( int i = 0; i < entries.Length; i++ )
					{
						PokeWalkerEntry entry = entries[i];

						uint pidcheck = pid_seed;

						if ( !entry.Female && female )
						{
							pidcheck += ((((GenderThresh - (pidcheck & 0xFF)) / 25) + 1) * 25);
							if ( ( nature & 1 ) != ( pidcheck & 1 ) )
								pidcheck += 25;
						}
						else if ( entry.Female && !female )
						{
							pidcheck += (((((pidcheck & 0xFF) - GenderThresh) / 25) + 1) * 25);
							if ( ( nature & 1 ) != ( pidcheck & 1 ) )
								pidcheck -= 25;
						}

						validPIDs.Add( pidcheck );
					}
				}

				for ( int i = 0;i < validPIDs.Count; i++ )
					if ( PID == validPIDs[i] )
						return true;
			}

			return false;
		}

		public PokeWalkerEntry[] GetEntriesFor()
		{
			ushort[] evochildren = GetEvoChildren();

			List<PokeWalkerEntry> entries = new List<PokeWalkerEntry>();

			for ( int i = 0; i < PokeWalkerCourse.PokeWalkerCourses.Length; i++ )
			{
				PokeWalkerCourse course = PokeWalkerCourse.PokeWalkerCourses[i];

				if ( ( course.Language & (LangFlag)Math.Pow( 2, (byte)Language ) ) != 0 )
					for ( int j = 0; j < 6; j++ )
					{
						PokeWalkerEntry entry = course[j];

						for ( int k = 0; k < evochildren.Length; k++ )
							if ( k == entry.NatID && entry.Level <= Level && entry.Female == ( Gender == Gender.Female || Gender == Gender.Genderless ) )
								entries.Add( entry );
					}
			}

			return entries.ToArray();
		}
	}
}