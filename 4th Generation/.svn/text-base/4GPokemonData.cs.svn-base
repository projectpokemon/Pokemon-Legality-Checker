using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class 4GPokemonData : PokemonData
	{
		private bool m_RequiresSyncCheck;
		private bool m_ValidSync;
		public bool RequiresSyncCheck{ get{ return m_RequiresSyncCheck; } set{ m_RequiresSyncCheck = value; } }

		public 4GPokemonData( byte[] data ) : base( data )
		{
			m_RequiresSyncCheck = true;
		}

		public override bool IsValidPokeball() //Depends if its a starter/hatched/gift/event...
		{
			return PokeBall == PokeBall.PokeBall;
		}

		public override bool IsValidLocation()
		{
			return base.IsValidLocation() && Location < 2000; //Cannot be more than 2000, thats for special stuff.
		}

		public virtual bool IsValidEggLocation()
		{
			return EggLocation == 2000; //We reserve this for the database, where we have specific eggs.
		}

		public virtual bool IsValidHatchLocation()
		{
			return Location
		}

		public override void Checks()
		{
			base.Checks();

			Utility.Output( "Shiny Leaves: " );
			Utility.OutputValidityLine( IsValidLeaves(), "Valid", "Invalid" );

			Utility.Output( "Location: " ); //If its an egg, check for a valid egg location.
			Utility.OutputValidityLine( IsValidLocation(), "Valid", "Invalid" );

			Utility.Output( "Algorithm: " );
			string message;
			if ( IsValidAlgorithm( out message ) )
			{
				if ( m_RequiresSyncCheck )
				{
					Utility.OutputValid( message );
					Utility.Output( " - Sync: " );
					Utility.OutputValidityLine( m_ValidSync, "Valid", "Invalid" )
				}
				else
					Utility.OutputValidLine( message );
			}
			else
				Utility.OutputInvalidLine( message );
		}

		public override bool IsValidAlgorithm( out string message )
		{
			if ( IsEgg && IsValidEggLocation() )
			{
				message = "Valid Egg";
				return true;
			}
			
			if ( IsHatched && 

			uint plow = ( PID & 0xFFFF ) << 0x10;
			uint phigh = (uint)(PID & ~0xFFFF) >> 0x10;

			bool valid = false;

			PokePRNG rand = new PokePRNG();
			for ( uint j = 0;j < 0x10000; j++ )
			{
				rand.Seed = plow + j;
				if ( ( rand.NextNum() >> 0x10 ) == phigh ) // A-B
				{
					int C = (int)rand.SetNext().HighBits & 0x7FFF;
					int D = (int)rand.SetNext().HighBits & 0x7FFF;

					if ( Utility.IsIVMatch( iv1, iv2, C, D ) )
					{
						valid = true;
						message = "NDS (A-B-C-D)";

						if ( RequiresSyncCheck )
						{
							m_ValidSync = IsValidNature( plow + j, PID % 25 ) );
							if ( m_ValidSync )
								break;
						}
					}
				}
			}

			if ( !valid && (HomeTown == 10 || HomeTown == 11) && IsShiny() && IsGrassWild() && Location != 48 ) //Check for chaining! ~ Not in Eterna Forest
			{
				PokePRNG rand = new PokePRNG();
				uint iv1 = IV1;
				uint iv2 = IV2;

				uint highbits = iv1 << 0x10;
				for ( uint k = 0;k < 2; k++ )
				{
					for( uint i = 0; i < 0x10000; i++ )
					{
						uint seed = ( k << 31 ) + highbits + i;
						rand.Seed = seed;
						if ( ( ( rand.Next() >> 0x10 ) & 0x7FFF ) == iv2 )
						{
							for( int j = 0; j < 16; j++ )
								rand.PrevNum();

							uint natseed = rand.Seed;

							uint pid = ShinyPID( rand );

							if ( realpid == pid )
							{
								valid = true;
								message = "Chain Shiny";
								m_RequiresSyncCheck = false;
							}
						}
					}
				}
			}

			if ( !valid )
				message = "Invalid";

			return valid;
		}

		private uint ShinyPID( PokePRNG rand )
		{
			uint subID = ( TID ^ SID ) >> 0x3;
			uint low6 = rand.SetNext().HighBits & 0x7;
			uint high5 = rand.SetNext().HighBits & 0x7;

			for( int i = 0; i < 13; i++ )
			{
				bool subidcheck = (subID & (1<<i)) != 0;
				bool randcheck = (rand.SetNext().HighBits & 0x1) != 0;

				if ( randcheck )
					low6 |= ( 1u << ( i + 3 ) );

				if ( randcheck != subidcheck )
					high5 |= ( 1u << ( i + 3 ) );
			}

			return ( high5 << 0x10 ) | low6;
		}

		public static 4GPokemonData GetDPPokemon( byte[] data )
		{
			ushort natid = BitConverter.ToUInt16( data, 0x08 );
			ushort eggloc = BitConverter.ToUInt16( data, 0x7E );

			if ( (natid == 446 || natid == 143) && eggloc == 0 ) //Munchlax/Snorlax ~ not hatched/egg
				return DPMunchlaxPokemon( data );

			return new 4GPokemonData( data );
		}

		public static bool IsValidNature( uint pid1seed, uint nature )
		{
			PokePRNG rand = new PokePRNG( pid1seed ); //PID_LOW (FULL SEED)

			bool cont = true;

			do
			{
				uint newseed = rand.PrevNum(); //POSSIBLE SEED POINTER

				uint newhigh = rand.SetPrev().HighBits; //PID2
				uint newlow = rand.SetPrev().HighBits; //PID1

				uint newpid = ( newhigh << 0x10 ) | newlow;

				if ( (newseed / 0xA3E) == nature || (newseed >> 15 == 0) )
					return true;
				else if ( (newpid % 25) == nature )
					cont = false;
			}
			while ( cont );

			return false;
		}
	}
}