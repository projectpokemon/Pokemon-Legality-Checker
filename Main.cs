using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class LegitCheck
	{
		public static readonly string m_Version = "vB54-24";

		public static void Main( string[] args )
		{
			PushColor( ConsoleColor.White );
			Console.WriteLine( "Pokemon Legality Checker - by: Sabresite - {0}", m_Version );
			PopColor();

			if ( args.Length > 0 )
			{
				List<string> filelist = new List<string>();

				for ( int i = 0; i < args.Length; i++ )
				{
					if ( File.Exists( args[i] ) && args[i].ToLower().EndsWith( ".pkm" ) )
						filelist.Add( args[i] );
					else if ( Directory.Exists( args[i] ) )
						ProcDir( args[i], ref filelist );
				}

				for ( int j = 0; j < filelist.Count; j++ )
				{
					ProcFile( filelist[j] );
					Console.WriteLine( "--------------------" );
				}
			}
			else
				Console.WriteLine( "Legit.exe <filename|folderpath> <filename|folderpath>..." );
			Console.WriteLine( "\n\r\n\rPress the ENTER key to exit..." );
			Console.Read();
		}

		public static void ProcDir( string target, ref List<string> filelist )
		{
			// Process the list of files found in the directory.
			string [] files = Directory.GetFiles(target);
			for ( int i = 0; i < files.Length; i++ )
			{
				string filename = files[i];
				if ( filename.ToLower().EndsWith( ".pkm" ) )
					filelist.Add( filename );
			}

			// Recurse into subdirectories of this directory.
			string[] subdirs = Directory.GetDirectories(target);
			for ( int i = 0; i < subdirs.Length; i++ )
				ProcDir( subdirs[i], ref filelist );
		}

		public static void ProcFile( string filename )
		{
			byte[] data = null;
			try
			{
				FileStream fs = File.OpenRead(filename);
				data = new byte[fs.Length];
				if ( data.Length != 136 && data.Length != 236 )
				{
					Console.WriteLine( "Invalid File: {0}", filename );
					return;
				}
				ReadBytes( fs, data );
				fs.Close();
			}
			catch ( Exception e )
			{
				Console.WriteLine( "Error Opening File: {0}", e.ToString() );
			}

			Console.WriteLine( "File: {0}", filename );

			ushort ochecksum = BitConverter.ToUInt16( data, 0x6 );

			long checksum = 0;

			for ( int i = 8; i < 136; i+=2 )
				checksum += BitConverter.ToUInt16( data, i );

			ushort newchecksum = (ushort)checksum;

			Console.Write( "Checksum: 0x{0:X2} - ", newchecksum );
			if ( newchecksum == ochecksum )
			{
				PushColor( ConsoleColor.DarkGreen );
				Console.WriteLine( "Valid" );
			}
			else
			{
				PushColor( ConsoleColor.Red );
				Console.WriteLine( "Invalid" );
			}
			PopColor();

			ushort natid = BitConverter.ToUInt16( data, 0x08 );
			DateTime date = new DateTime( data[0x7B] + 2000, Math.Max( data[0x7C], (byte)1 ), Math.Max( data[0x7D],(byte)1 ) );
			uint ivscom = BitConverter.ToUInt32( data, 0x38 );
			int iv1 = (int)( ivscom & 0x7FFF );
			int iv2 = (int)( ivscom >> 15 ) & 0x7FFF;

			uint pid = BitConverter.ToUInt32( data, 0x00 );
			int[] ivset = ParseStats( iv1, iv2 );
			byte gendthresh = m_GenderThresholds[natid];
			string gender = "Genderless";
			if ( gendthresh != 255 )
			{
				bool female = (pid%256) < gendthresh;
				if ( gendthresh == 0 || !female )
					gender = "Male";
				else if ( gendthresh == 254 || female )
					gender = "Female";
			}

			uint tid = (uint)BitConverter.ToUInt16( data, 0x0C );
			uint sid = (uint)BitConverter.ToUInt16( data, 0x0E );

			bool egg = ( ivscom & 0x40000000u ) != 0;
			bool nickname = ( ivscom & 0x80000000u ) != 0;
			bool hatched = !egg && BitConverter.ToUInt16( data, 0x7E ) > 0;

			bool shiny = IsShiny( pid, tid, sid );

			Console.WriteLine( "Pokemon ID: {0} - {1}, {2}, {3}, {4}", pid, m_Natures[pid%25], gender, ( (pid%2) == 0 ) ? "Ability 1" : "Ability 2", shiny ? "Shiny" : "Not Shiny" );

			if ( egg || hatched )
			{
				DateTime eggdate = new DateTime( 2000 + data[0x78], data[0x79], data[0x7A] );
				Console.Write( "Egg Received: " );
				PushColor( ConsoleColor.DarkCyan );
				if ( hatched )
				{
					Console.Write( eggdate.ToString( "MMMM dd, yyyy" ) );
					PopColor();
					Console.Write( " - Hatched: " );
					PushColor( ConsoleColor.DarkCyan );
					Console.WriteLine( date.ToString( "MMMM dd, yyyy" ) );
				}
				else
					Console.WriteLine( eggdate.ToString( "MMMM dd, yyyy" ) );
				PopColor();
			}
			else
			{
				Console.Write( "Date: " );
				PushColor( ConsoleColor.DarkCyan );
				Console.WriteLine( date.ToString( "MMMM dd, yyyy" ) );
				PopColor();
			}

			Console.WriteLine( "IVs: {0},{1},{2},{3},{4},{5}", ivset[0], ivset[1], ivset[2], ivset[3], ivset[4], ivset[5] );

			int evs = data[0x18] + data[0x19] + data[0x1A] + data[0x1B] + data[0x1C] + data[0x1D];
			bool otg = ( ( data[0x84] & 0x80 ) != 0 );

			Console.WriteLine( "Trainer ID: {0}\r\nSecret ID: {1}", tid, sid );
			Console.WriteLine( "Trainer Gender: {0}",  otg ? "Female" : "Male" );
			Console.WriteLine( "Hidden Power: {0} - {1}", m_AttackTypes[GetHPType( ivset )], GetHPPower( ivset ) );

			bool fateful = ( data[0x40] & 0x1 ) != 0;
			Console.WriteLine( "Fateful Encounter: {0}", fateful ? "Yes" : "No" );
			Console.Write( "Gender Check: " );
			if ( ValidGender( pid, data[0x40], gendthresh ) )
			{
				PushColor( ConsoleColor.DarkGreen );
				Console.WriteLine( "Valid" );
			}
			else
			{
				PushColor( ConsoleColor.Red );
				Console.WriteLine( "Invalid" );
			}
			PopColor();
			Console.Write( "Effort Values: " );

			if ( evs <= 510 )
			{
				PushColor( ConsoleColor.DarkGreen );
				Console.WriteLine( "Valid" );
			}
			else
			{
				PushColor( ConsoleColor.Red );
				Console.WriteLine( "Invalid" );
			}
			PopColor();

			string message = String.Empty;

			byte country = data[0x17];
			byte hometown = data[0x5F];
			bool colored = false;

			ushort location = BitConverter.ToUInt16( data, 0x80 );
			ushort eggloc = BitConverter.ToUInt16( data, 0x7E );
			ushort byte46 = BitConverter.ToUInt16( data, 0x46 );
			ushort byte85 = data[0x85];

			if ( hometown == 12 || hometown == 7 || hometown == 8 ) //Platinum
			{
				location = byte46;
				eggloc = BitConverter.ToUInt16( data, 0x44 );
			}

			bool manaphy = natid == 490 && fateful && eggloc == 3001;

			bool hatchedman = hatched && manaphy;
			bool eggman = egg && manaphy;
			bool eggmanevent = eggman && IsABCD( pid, iv1, iv2 );
			bool manevent = false;

			if ( hatchedman ) //Manaphy Event
			{
				if ( IsABCD( pid, iv1, iv2 ) )
					manevent = true;
				else // if ( !shiny ) //Lets see if its antishiny!
				{
					uint newpid = pid;
					uint baseshinyxor = 0;
					bool nobasexor = true;
					bool cont = false;
					do
					{
						cont = false;

						newpid--;
						newpid *= 0x9638806D;

						if ( IsShiny( newpid, tid, sid ) )
						{
							uint low = newpid & 0xFFFF;
							uint high = (uint)(newpid & ~0xFFFF);

							uint shinyxor = high ^ low;

							if ( nobasexor )
							{
								baseshinyxor = shinyxor;
								nobasexor = false;
								cont = true;
							}
							else if ( ( baseshinyxor ^ shinyxor ) < 8 )
								cont = true;

							if ( cont && IsABCD( newpid, iv1, iv2 ) )
							{
								manevent = true;
								break;
							}
						}
					}
					while ( cont );
				}
			}

			bool palparked = location == 55;
			bool mysterygift = location >= 3000 && fateful;
			MGiftType gifttype = MGiftType.None;

			if ( mysterygift )
			{
				MG valid = MG.FindStaticGift( natid, tid, sid );
				if ( valid != null )
				{
					if ( valid.PID == pid && valid.ValidAcquire( date, hometown, country, natid, otg, location ) )
						gifttype = MGiftType.Static;
					else
					{
						colored = true;
						gifttype = MGiftType.StaticHacked;
					}
				}
				else
				{
					valid = MG.FindDynamicGift( natid, tid, sid );
					if ( valid != null )
					{
						if ( valid.ValidAcquire( date, hometown, country, natid, otg, location ) )
							gifttype = MGiftType.Dynamic;
						else
						{
							colored = true;
							gifttype = MGiftType.DynamicHacked;
						}
					}
					/*
					else
					{
						valid = MG.FindDynamicShinyGift( shortnat, tid, sid );
						if ( valid != null )
						{
							if ( valid.ValidAcquire( date, hometown, country, natid, otg, location ) )
								gifttype = MGiftType.DynamicShiny;
							else
								gifttype = MGiftType.DynamicHacked;
						}
					}
					*/
				}
			}

			bool munchlax = ( natid == 446 || natid == 143 ) && !palparked && !hatched && ( hometown == 10 || hometown == 11 || hometown == 12 );
			bool munchlaxlegal = munchlax && IsMunchTree( location, tid, sid );

			string trashmessage = String.Empty;

			Console.WriteLine( "Nicknamed: {0}", nickname ? "Yes" : "No" );
			Console.WriteLine( "Home Town: {0}", ( hometown >= 0 && hometown < m_HomeTowns.Length ) ? m_HomeTowns[hometown] : "(none)" );
			Console.Write( "Country Originated: " );

			if ( country < 1 || country > 8 || country == 6 )
			{
				PushColor( ConsoleColor.Red );
				Console.WriteLine( "Invalid" );
				PopColor();
			}
			else
				Console.WriteLine( m_CountryNames[country] );

			if ( palparked )
				trashmessage = PalParkTrashName( data, location == byte46, nickname ) ? "Valid" : "Invalid";
			else if ( mysterygift )
				trashmessage = MysteryGiftTrashNames( data ) ? "Valid" : "Invalid";

			if ( !String.IsNullOrEmpty( trashmessage ) )
			{
				Console.Write( "Trash Bytes: " );

				if ( trashmessage == "Valid" )
				{
					PushColor( ConsoleColor.DarkGreen );
					Console.WriteLine( "Valid" );
				}
				else
				{
					PushColor( ConsoleColor.Red );
					Console.WriteLine( "Invalid" );
				}
				PopColor();
			}

			bool shinyglitchzig = ( natid == 263 || natid == 264 ) && location == 55 && shiny && hometown == 1 && data[0x15] == 53 && sid == 0 && ( tid == 21121 || tid == 30317 ); //zigzagoon or linoone, pal parked, shiny, hometown sapphire, and ability is pickup, and event tid/sid
			bool checknature = !fateful && !egg && !hatched && eggloc == 0 && ( hometown == 10 || hometown == 11 || hometown == 12 ) && natid != 488 && natid != 481 && natid != 144 && natid != 145 && natid != 146 && location < 2000 && location != 52 && location != 55 && ( byte85 == 2 || byte85 == 9 || byte85 == 5 || byte85 == 7 || byte85 == 4 || byte85 == 0x17 );

			if ( byte85 == 0 )
			{
				if ( ( natid == 425 || natid == 426 ) && location == 47 )
					checknature = true;
				else if ( natid == 442 && location == 24 )
					checknature = true;
			}

			bool specialegg = false;
			if ( ( hometown == 8 || hometown == 7 ) && BitConverter.ToUInt16( data, 0x44 ) == 0x7DE )
			{
				if ( natid == 179 || natid == 180 || natid == 194 || natid == 195 || natid == 218 || natid == 219 ) //special mareep/wooper egg
					specialegg = true;
			}

			if ( egg )
			{
				if ( eggman )
				{
					if ( eggmanevent )
					{
						uint newpid = pid;
						while ( IsShiny( newpid, tid, sid ) )
						{
							newpid *= 0x6C078965;
							newpid++;
						}
						if ( pid != newpid )
							Console.WriteLine( "Untraded Hatched Pokemon ID: {0} - {1}, {2}, {3}, {4}", newpid, m_Natures[newpid%25], "Genderless", ( (newpid%2) == 0 ) ? "Ability 1" : "Ability 2", IsShiny( newpid, tid, sid ) ? "Shiny" : "Not Shiny" );
						message = "Egg from Manaphy Event (Pokemon Ranger)";
					}
					else
					{
						colored = true;
						message = "Hacked Manaphy Event Egg";
					}
				}
				else if ( specialegg && IsABCD( pid, iv1, iv2 ) )
					message = "Big Brother's Egg";
				else
					message = "Egg";
			}
			else if ( gifttype != MGiftType.None )
			{
				switch ( gifttype )
				{
					case MGiftType.Static: message = "Mystery Gift (Static)"; break;
					case MGiftType.StaticHacked: message = "Mystery Gift (Hacked)"; break;
					case MGiftType.Dynamic: message = "Mystery Gift (Dynamic)"; break;
					case MGiftType.DynamicHacked: message = "Mystery Gift (Hacked)"; break;
					//case MGiftType.DynamicShiny: message = "Mystery Gift (Dynamic)"; break;
					//case MGiftType.DynamicShinyHacked: message = "Mystery Gift (Hacked)"; break;
				}
			}
			else if ( hatched )
			{
				if ( hatchedman )
				{
					if ( manevent )
						message = "Hatched from Manaphy Event (Pokemon Ranger)";
					else
					{
						colored = true;
						message = "Hacked Manaphy Event";
					}
				}
				else if ( specialegg && IsABCD( pid, iv1, iv2 ) )
					message = "Hatched Big Brother's Egg";
				else
					message = "Hatched";
			}
			else if ( munchlax )
			{
				uint intseed;

				if ( munchlaxlegal && IsABCD( pid, iv1, iv2, out intseed ) )
				{
					Console.Write( "Type: Honey Tree Munchlax - Sync: " );
					if ( ValidNature( intseed, pid % 25 ) )
					{
						PushColor( ConsoleColor.DarkGreen );
						Console.WriteLine( "Valid" );
					}
					else
					{
						PushColor( ConsoleColor.Red );
						Console.WriteLine( "Invalid" );
					}

					PopColor();
					//message = "Honey Tree Munchlax";
				}
				else
				{
					colored = true;
					message = "Hacked Pokemon";
				}
			}
			else if ( shinyglitchzig )
			{
				if ( ValidGlitchZig( pid, tid, iv1, iv2 ) )
					message = String.Format( "Valid {0} Berry Glitch Zigzagoon ({1})", tid == 30317 ? "USA" : "Japan",  ( ( data[0x84] & 0x80 ) != 0 ) ? "RUBY" : "SAPHIRE" );
				else
				{
					colored = true;
					message = "Hacked Berry Glitch Zigzagoon";
				}
			}
			else if ( palparked && SP.Valid3rdGen( natid, tid, sid, pid, ivscom, otg, hometown ) )
			{
				message = "Static (usually in-game traded)";
			}
			else if ( palparked && SP.ValidJeremy( natid, tid, sid, pid, ivscom, otg, Math.Min( (byte)(data[0x84] & 0x7F), (byte)100 ), hometown ) )
			{
				message = "JEREMY (Static)";
			}
			else if ( !palparked && SP.Valid4thGen( natid, tid, sid, pid, ivscom, otg, hometown, location ) )
			{
				message = "Static (usually in-game traded)";
			}
			else if ( !GenerateIVs( natid, pid, iv1, iv2, hometown, checknature, palparked ) )
			{
				if ( shiny && ChainedPID( (uint)iv1, (uint)iv2, tid, sid, pid ) )
					message = "Chain Shiny";
				else if ( palparked )
					message = "Unknown GBA Type.";
				else
				{
					colored = true;
					message = "Hacked or Unknown Pokemon";
				}
			}

			if ( !String.IsNullOrEmpty( message ) )
			{
				Console.Write( "Type: " );
				if ( colored )
					PushColor( ConsoleColor.Red );
				else
					PushColor( ConsoleColor.Yellow );
				Console.WriteLine( message );
				PopColor();
			}
		}

		public static bool ValidGlitchZig( uint pid, uint tid, int oivs1, int oivs2 )
		{
			PokePRNG rand = new PokePRNG();
			for ( uint i = 0; i < 2; ++i )
			{
				for ( uint j = 0; j < 0x10000; j++ )
				{
					rand.Seed = (i << 31) + ((uint)oivs2 << 16) + j;
					uint iv1 = rand.Seed;
					uint second = ( rand.SetPrev().Seed >> 0x10 ) & 0x7FFF;
					uint iv2 = rand.Seed;
					if ( second == (uint)oivs1 )
					{
						uint afull = rand.PrevNum(); //Variance
						uint a = rand.HighBits;
						uint bfull = rand.PrevNum(); //Unknown (Gender Check?)
						uint b = rand.HighBits;
						uint cfull = rand.PrevNum(); //High PID
						uint c = rand.HighBits;
						uint dfull = rand.PrevNum(); //Seed
						uint d = rand.HighBits;

						if ( d == 0 && (dfull & 0xFFFF) <= 0xFF )
						{
							uint calcshiny = c ^ tid ^ 0;
							uint var = ( ( calcshiny ^ a ) & 0x7 );
							uint calclowpid = calcshiny ^ var;
							if ( ( ( c << 0x10 ) | calclowpid ) == pid )
								return true;
						}
					}
				}
			}
			return false;
		}

		public static bool IsShiny( uint pid, uint tid, uint sid )
		{
			uint low = pid & 0xFFFF;
			uint high = (uint)(pid & ~0xFFFF);

			return ( ( tid ^ sid ) ^ ( (high >> 0x10) ^ low ) ) < 8;
		}

		public static bool ValidGender( uint pid, byte genderflag, byte genderthresh )
		{
			bool female = (pid % 256) < genderthresh;

			if ( genderthresh == 255 && ( ( genderflag & 0x4 ) != 0 ) )
				return true;

			if ( ( female || genderthresh == 254 ) && ( ( genderflag & 0x2 ) != 0 ) )
				return true;

			if ( ( !female || genderthresh == 0 ) && ( ( genderflag & 0x6 ) == 0 ) )
				return true;

			return false;
		}

		public static int GetHPType( int[] ivs )
		{
			int total = 0;
			for ( int i = 0; i < ivs.Length; i++ )
				if ( ( ivs[i] % 2 ) == 1 )
					total += (int)Math.Pow( 2, i );
			return ( total * 15 ) / 63;
		}

		public static int GetHPPower( int[] ivs )
		{
			int total = 0;
			for ( int i = 0; i < ivs.Length; i++ )
				if ( ( ivs[i] % 4 ) > 1 )
					total += (int)Math.Pow( 2, i );
			return ( ( total * 40 ) / 63 ) + 30;
		}

		public static bool MysteryGiftTrashNames( byte[] data )
		{
			bool trash1 = true;
			bool trash2 = true;

			int termindex = -1;

			for ( int i = 0x48; termindex == -1 && i < 0x5D; i+=2 ) //not using <= because we are checking 2 bytes at a time
				if ( data[i] == 0xFF && data[i+1] == 0xFF )
					termindex = i;

			if ( termindex > -1 )
				for ( termindex += 2;trash1 && termindex <= 0x5D; termindex++ )
					if ( data[termindex] != 0xFF )
						trash1 = false;

			termindex = -1;

			for ( int i = 0x68; termindex == -1 && i < 0x77; i+=2 ) //not using <= because we are checking 2 bytes at a time
				if ( data[i] == 0xFF && data[i+1] == 0xFF )
					termindex = i;

			if ( termindex > -1 )
				for ( termindex += 2; trash2 && termindex <= 0x77; termindex++ )
					if ( data[termindex] != 0xFF )
						trash2 = false;

			return trash1 && trash2;
		}

		public static readonly byte[][][] m_PtTrashNames = new byte[][][]
		{
			new byte[][]{}, //No Country
			new byte[][] //Japanese
				{
					new byte[]{ 0x00, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x58, 0x11, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x28, 0x02, 0xFF, 0xFF, 0x27, 0x02, 0x25, 0x6E, 0x07, 0x02, 0x00, 0x00 }
				},
			new byte[][] //English
				{
					new byte[]{ 0x00, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC8, 0x19, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0xFF, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x28, 0x02, 0xFF, 0xFF, 0x27, 0x02, 0x4D, 0x75, 0x07, 0x02, 0x00, 0x00 }
				},
			new byte[][] //French
				{
				},
			new byte[][] //Italian
				{
				},
			new byte[][] //German
				{
				},
			new byte[][]{}, //No Country
			new byte[][] //Spanish
				{
				},
			new byte[][] //Korean
				{
				},
		};

		public static readonly byte[][][] m_DPTrashNames = new byte[][][]
		{
			new byte[][]{}, //No Country
			new byte[][] //Japanese
				{
					new byte[]{ 0x00, 0x00, 0x00, 0x00, 0xB4, 0xC5, 0x0C, 0x02, 0xE0, 0xFF, 0x7F, 0x02, 0x42, 0x00, 0x00, 0xFF, 0x00, 0x00 },
					new byte[]{ 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }
				},
			new byte[][] //English
				{
					new byte[]{ 0x18, 0x20, 0x0D, 0x02, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0xA1, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0x05, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x27, 0x02, 0xFF, 0xFF, 0x26, 0x02, 0xE9, 0x9A, 0x06, 0x02, 0x00, 0x00 }
				},
			new byte[][] //French
				{
					new byte[]{ 0x74, 0x20, 0x0D, 0x02, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA4, 0xA1, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0x05, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x27, 0x02, 0xFF, 0xD5, 0x26, 0x02, 0x45, 0x9B, 0x06, 0x02, 0x00, 0x00 }
				},
			new byte[][] //Italian
				{
					new byte[]{ 0x54, 0x20, 0x0D, 0x02, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x84, 0xA1, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0x05, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x27, 0x02, 0xFF, 0xFF, 0x26, 0x02, 0x25, 0x9B, 0x06, 0x02, 0x00, 0x00 }
				},
			new byte[][] //German
				{
					new byte[]{ 0x74, 0x20, 0x0D, 0x02, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA4, 0xA1, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0x05, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x27, 0x02, 0xFF, 0xD4, 0x26, 0x02, 0x45, 0x9B, 0x06, 0x02, 0x00, 0x00 }
				},
			new byte[][]{}, //No Country
			new byte[][] //Spanish
				{
					new byte[]{ 0x74, 0x20, 0x0D, 0x02, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA4, 0xA1, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0x05, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x27, 0x02, 0xFF, 0xFF, 0x26, 0x02, 0x45, 0x9B, 0x06, 0x02, 0x00, 0x00 }
				},
			new byte[][] //Korean
				{
					new byte[]{ 0x00, 0x00, 0x00, 0x00, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x18, 0x77, 0x0C, 0x02, 0xE0, 0xFF },
					new byte[]{ 0x05, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x28, 0x02, 0xFF, 0x4B, 0x27, 0x02, 0xB1, 0x9F, 0x06, 0x02, 0x00, 0x00 }
				},
		};

		public static readonly string[] m_CountryNames = new string[]{ "(None)", "Japan", "UK/US/AU", "France", "Italy", "Germany", "Invalid", "Spain/South America", "Korea" };

		public static bool PalParkTrashName( byte[] data, bool platinum, bool nicknamed )
		{
			int country = data[0x17];
			if ( country == 0 || country == 6 || country > 7 )
				return false;

			int termindex = -1;

			for ( int i = 0x48; termindex == -1 && i < 0x5D; i+=2 ) //not using <= because we are checking 2 bytes at a time
				if ( data[i] == 0xFF && data[i+1] == 0xFF )
					termindex = i;

			if ( termindex == -1 )
				return false;
			else if ( termindex == 0x5C )
			{
				Console.WriteLine( "PalPark Country: Indeterminate" );
				return true;
			}

			termindex += 2;

			int termin = termindex;

			byte[][][] trashnames = m_DPTrashNames;
			if ( platinum )
				trashnames = m_PtTrashNames;

			bool[][] trashbool = new bool[trashnames.Length][];

			for ( int boolindex = 1; boolindex < trashbool.Length; boolindex++ )
			{
				trashbool[boolindex] = new bool[trashnames[boolindex].Length];

				if ( boolindex == 6 ) //Not a valid country
					continue;

				for ( int j = 0; j < trashnames[boolindex].Length; j++ )
				{
					termindex = termin;
					byte[] trashseq =  trashnames[boolindex][j];

					bool trash = true;

					for ( int index = termindex - 0x4C; trash && index < trashseq.Length; index++, termindex++ )
					{
						if ( trashseq[index] == 0xFF )
						{
							if ( j == 0 ) //First Slot
							{
								if ( !platinum )
								{
									if ( boolindex == 1 && index == 15 ) //JAPAN
									{
										if ( !(data[termindex] == 0x08 || data[termindex] == 0x00) )
											trash = false;
									}
									else if ( data[termindex] != trashseq[index] )
										trash = false;
								}
								else if ( data[termindex] != trashseq[index] )
									trash = false;
							}
							else if ( boolindex > 0 ) //NOT AN INVALID COUNTRY???
							{
								if ( platinum )
								{
									if ( index == 0 || index == 4 || index == 8 )
										continue;
									else if ( index == 5 )
									{
										switch ( boolindex )
										{
											case 1: //Japan
											{
												if ( data[termindex] != 0x00 && ( data[termindex] < 0x9C || data[termindex] > 0xA0 ) )
													trash = false;
												break;
											}
											case 2: //English
											{
												if ( data[termindex] != 0x00 && ( data[termindex] < 0xA8 || data[termindex] > 0xAD ) )
													trash = false;
												break;
											}
										}
									}
									else if ( index == 9 )
									{
										switch ( boolindex )
										{
											case 1: //Japan
											{
												if ( data[termindex] < 0xD4 || data[termindex] > 0xD5 )
													trash = false;
												break;
											}
											case 2: //English
											{
												if ( data[termindex] < 0xE0 || data[termindex] > 0xE1 )
													trash = false;
												break;
											}
										}
									}
									else if ( data[termindex] != trashseq[index] )
										trash = false;
								}
								else //JAPANESE NOT AFFECTED BY SLOT2
								{
									if ( index == 4 || index == 8 )
										continue;
									else if ( index == 5 )
									{
										if ( boolindex == 8 ) //Korean
										{
											if ( data[termindex] < 0x07 || data[termindex] > 0x0B )
												trash = false;
										}
										else if ( data[termindex] < 0x8E || data[termindex] > 0x96 ) //English -> Spanish
											trash = false;
									}
									else if ( index == 9 )
									{
										switch ( boolindex )
										{
											case 2: //English
											{
												if ( data[termindex] < 0xD1 || data[termindex] > 0xD2 )
													trash = false;
												break;
											}
											case 4: //Italian
											{
												if ( data[termindex] < 0xD3 || data[termindex] > 0xD4 )
													trash = false;
												break;
											}
											case 7: //Spanish
											{
												if ( data[termindex] < 0xD5 || data[termindex] > 0xD6 )
													trash = false;
												break;
											}
										}
									}
									else if ( data[termindex] != trashseq[index] )
										trash = false;
								}
							}
							else if ( data[termindex] != trashseq[index] )
								trash = false;
						}
						else if ( data[termindex] != trashseq[index] )
							trash = false;
					}

					trashbool[boolindex][j] = trash;
				}
			}

			bool ottrash = true;

			termindex = -1;

			for ( int i = 0x68; termindex == -1 && i < 0x77; i+=2 ) //not using <= because we are checking 2 bytes at a time
				if ( data[i] == 0xFF && data[i+1] == 0xFF )
					termindex = i;

			if ( termindex == -1 ) //No terminator
				ottrash = false;
			else
			{
				termindex += 2;

				for (; ottrash && termindex <= 0x77; termindex++ )
					if ( data[termindex] != data[termindex - 0x20] )
						ottrash = false;
			}

			if ( ottrash )
			{
				string cmessage = String.Empty;
				for ( int i = 1; i < trashbool.Length; i++ )
				{
					if ( i != 0 && i != 6 && ( country == i || nicknamed || i == 8 ) )
					{
						for ( int j = 0; j < trashbool[i].Length; j++ )
						{
							if ( trashbool[i][j] )
							{
								cmessage += String.Format( "{0}{1}", String.IsNullOrEmpty( cmessage ) ? String.Empty : ", ", m_CountryNames[i] );
								break;
							}
						}
					}
				}

				if ( !String.IsNullOrEmpty( cmessage ) )
				{
					Console.WriteLine( "PalPark Country: {0}", cmessage );
					return true;
				}
			}

			return false;
		}

		public static void ReadBytes(Stream stream, byte[] data )
		{
			ReadBytes( stream, 0, data );
		}

		public static void ReadBytes(Stream stream, int offset, byte[] data )
		{
			int remaining = data.Length;
			while (remaining > 0)
			{
				int read = stream.Read(data, offset, remaining);
				if (read <= 0)
					throw new EndOfStreamException
						(String.Format("End of stream reached with {0} bytes left to read", remaining));
				remaining -= read;
				offset += read;
			}
		}

		public static bool IsMunchTree( ushort loc, uint tid, uint sid )
		{
			byte[] indices = new byte[4];
			indices[0] = (byte)(((sid>>0x8)&0xFF)%0x15);
			indices[1] = (byte)((sid&0xFF)%0x15);
			indices[2] = (byte)(((tid>>0x8)&0xFF)%0x15);
			indices[3] = (byte)((tid&0xFF)%0x15);

			for ( int i = 1;i < 4; i++ )
			{
				for ( int j = 0;j < i; j++ )
				{
					byte t1 = indices[j];
					byte t2 = indices[i];
					if( t1 == t2 )
						indices[i] = (byte)( ++t1 % 0x15 );
				}
			}

			for ( int i = 0;i < 4; i++ )
			{
				Console.WriteLine( "Location: {0}", m_HoneyLocations[indices[i]] );
				if ( m_HoneyLocations[indices[i]] == loc )
					return true;
			}

			return false;
		}

		public static bool ChainedPID( uint iv1, uint iv2, uint tid, uint sid, uint realpid )
		{
			PokePRNG rand = new PokePRNG(0);
			uint highbits = iv1 << 0x10;
			for ( uint k = 0;k < 2; k++ )
			{
				for( uint i = 0; i < 0x10000; i++ )
				{
					uint seed = ( k << 31 ) + highbits + i;
					rand.Seed = seed;
					if ( ( ( rand.Next() >> 0x10 ) & 0x7FFF ) == iv2 )
					{
						//Console.WriteLine( "Seed Found: {0:X}", seed );
						for( int j = 0; j < 16; j++ )
							rand.PrevNum();

						uint natseed = rand.Seed;

						//Console.WriteLine( "Modified Seed: {0:X}", rand.Seed );
						uint pid = ShinyPID( tid, sid, rand );
						//Console.WriteLine( "Found PID: {0:X}", pid );
						if ( realpid == pid )
						{
							bool validnature = ValidNature( natseed, realpid % 25 );
							Console.Write( "Chained Sync: " );
							if ( validnature )
							{
								PushColor( ConsoleColor.DarkGreen );
								Console.WriteLine( "Valid" );
							}
							else
							{
								PushColor( ConsoleColor.Red );
								Console.WriteLine( "Invalid" );
							}
							PopColor();
							return true;
						}
					}
				}
			}
			return false;
		}

		public static uint ShinyPID( uint tid, uint sid, PokePRNG rand )
		{
			uint subID = ( tid ^ sid ) >> 0x3;
			uint low6 = rand.SetNext().HighBits & 0x7;
			uint high5 = rand.SetNext().HighBits & 0x7;

			//Console.WriteLine( "Low6: {0:X}\nHigh5: {1:X}", low6, high5 );

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

		public static bool IsABCD( uint pid, int iv1, int iv2 )
		{
			uint intseed;
			return IsABCD( pid, iv1, iv2, out intseed );
		}

		public static bool IsABCD( uint pid, int iv1, int iv2, out uint intseed )
		{
			intseed = 0;
			uint slow = ( pid & 0xFFFF ) << 0x10;
			uint shigh = (uint)(pid & ~0xFFFF) >> 0x10;

			PokePRNG rand = new PokePRNG();
			for ( uint j = 0; j < 0x10000; j++ )
			{
				uint A = slow + j;
				rand.Seed = A;
				intseed = slow + j;
				if ( ( rand.NextNum() >> 0x10 ) == shigh )
				{
					int C = (int)rand.SetNext().HighBits & 0x7FFF;
					int D = (int)rand.SetNext().HighBits & 0x7FFF;

					if ( IsLegal( iv1, iv2, C, D ) )
						return true;
				}
			}

			return false;
		}

		public static bool ValidNature( uint pid1seed, uint nature )
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

		public static void WriteType( string message )
		{
			Console.Write( "Type: " );
			PushColor( ConsoleColor.Yellow );
			Console.WriteLine( message );
			PopColor();
		}

		public static void WriteEventType( string message, bool rest )
		{
			Console.Write( "Type: " );
			PushColor( ConsoleColor.Yellow );
			Console.WriteLine( String.Format( "{0} [{1}]", message, rest ? "Restricted" : "Unrestricted" ) );
			PopColor();
		}

		public static void WriteSyncType( string message, bool valid )
		{
			Console.Write( "Type: " );
			PushColor( ConsoleColor.Yellow );
			Console.Write( message );
			PopColor();

			Console.Write( " - Sync: " );
			if ( valid )
			{
				PushColor( ConsoleColor.DarkGreen );
				Console.WriteLine( "Valid" );
			}
			else
			{
				PushColor( ConsoleColor.Red );
				Console.WriteLine( "Invalid" );
			}
			PopColor();
		}

		public static bool GenerateIVs( ushort natid, uint pid, int iv1, int iv2, int hometown, bool checknature, bool palpark )
		{
			bool matches = false;

			uint slow = ( pid & 0xFFFF ) << 0x10;
			uint shigh = (uint)(pid & ~0xFFFF) >> 0x10;

			uint elow = ( pid & 0xFFFF );
			uint ehigh = (uint)(pid & ~0xFFFF);

			PokePRNG rand = new PokePRNG();
			for ( uint j = 0; j < 0x10000; j++ )
			{
				bool validnature = false;

				rand.Seed = slow + j;
				if ( ( rand.NextNum() >> 0x10 ) == shigh ) // A-B
				{
					//Console.WriteLine( "Seed: {0:X}", A );
					int C = (int)rand.SetNext().HighBits & 0x7FFF;
					int D = (int)rand.SetNext().HighBits & 0x7FFF;
					int E = (int)rand.SetNext().HighBits & 0x7FFF;
					int F = (int)rand.SetNext().HighBits & 0x7FFF;

					//Console.WriteLine( "AB-C: {0:X}\nAB-D: {1:X}\nAB-E: {2:X}\nAB-F: {3:X}", C,D,E,F );

					validnature = checknature && IsLegal( iv1, iv2, C, D ) && ValidNature( slow + j, pid % 25 );

					if ( palpark )
					{
						if ( IsLegal( iv1, iv2, C, D ) ) //Normal Pokemon
						{
							WriteType( GetABGBAIVType( 3, 4 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, C, E ) ) //Normal GBA
						{
							WriteType( GetABGBAIVType( 3, 5 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, C, F ) )
						{
							WriteType( GetABGBAIVType( 3, 6 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, D, E ) ) //Normal
						{
							Console.WriteLine( "Seed Found: {0:X8}", new PokePRNG( slow + j ).Prev() );
							WriteType( GetABGBAIVType( 4, 5 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, D, F ) ) //Rare GBA
						{
							WriteType( GetABGBAIVType( 4, 6 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, E, F ) ) //Ultra Rare GBA
						{
							WriteType( GetABGBAIVType( 5, 6 ) );
							matches = true;
						}

						if ( ( hometown == 1 || hometown == 2 || hometown == 4 || hometown == 5 ) && ( IsRoaming( C, iv1, iv2 ) || IsRoaming( D, iv1, iv2 ) || IsRoaming( E, iv1, iv2 ) ) )
						{
							WriteType( "Roaming GBA" );
							matches = true;
						}
					}
					else
					{
						if ( IsLegal( iv1, iv2, C, D ) ) //Normal Pokemon
						{
							string message = GetABNDSIVType( 3, 4 );
							if ( checknature )
								WriteSyncType( message, validnature );
							else
								WriteType( message );
							matches = true;
						}
					}
				}
				else if ( palpark && ( rand.NextNum() >> 0x10 ) == shigh ) //A-C
				{
					int D = (int)rand.SetNext().HighBits & 0x7FFF;
					int E = (int)rand.SetNext().HighBits & 0x7FFF;
					int F = (int)rand.SetNext().HighBits & 0x7FFF;

					if ( IsLegal( iv1, iv2, D, E ) )
					{
						WriteType( GetACIVType( 4, 5 ) );
						matches = true;
					}

					if ( IsLegal( iv1, iv2, D, F ) )
					{
						WriteType( GetACIVType( 4, 6 ) );
						matches = true;
					}

					if ( IsLegal( iv1, iv2, E, F ) )
					{
						WriteType( GetACIVType( 5, 6 ) );
						matches = true;
					}

					if ( ( hometown == 1 || hometown == 2 || hometown == 4 || hometown == 5 ) && ( IsRoaming( D, iv1, iv2 ) || IsRoaming( E, iv1, iv2 ) ) )
					{
						WriteType( "Roaming GBA [Theoretical]" );
						matches = true;
					}
				}
				else if ( palpark && ( rand.NextNum() >> 0x10 ) == shigh ) //A-D
				{
					int E = (int)rand.SetNext().HighBits & 0x7FFF;
					int F = (int)rand.SetNext().HighBits & 0x7FFF;

					validnature = checknature && IsLegal( iv1, iv2, E, F ) && ValidNature( slow + j, pid % 25 );

					if ( IsLegal( iv1, iv2, E, F ) )
					{
						WriteType( GetADIVType( 5, 6 ) );
						matches = true;
					}

					if ( ( hometown == 1 || hometown == 2 || hometown == 4 || hometown == 5 ) && IsRoaming( E, iv1, iv2 ) )
					{
						WriteType( "Roaming GBA [Theoretical]" );
						matches = true;
					}
				}

				rand.Seed = ehigh + j;
				bool rest = rand.Prev() <= 0xFFFF;
				if ( ( rand.NextNum() >> 0x10 ) == elow ) // B-A (Events)
				{
					int C = (int)rand.SetNext().HighBits & 0x7FFF;
					int D = (int)rand.SetNext().HighBits & 0x7FFF;
					int E = (int)rand.SetNext().HighBits & 0x7FFF;
					int F = (int)rand.SetNext().HighBits & 0x7FFF;

					if ( palpark && natid == 201 && ( hometown == 4 || hometown == 5 ) ) //unown
					{
						if ( IsLegal( iv1, iv2, C, D ) )
						{
							WriteType( GetUnownIVType( 3, 4 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, C, E ) )
						{
							WriteType( GetUnownIVType( 3, 5 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, C, F ) )
						{
							WriteType( GetUnownIVType( 3, 6 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, D, E ) )
						{
							WriteType( GetUnownIVType( 4, 5 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, D, F ) )
						{
							WriteType( GetUnownIVType( 4, 6 ) );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, E, F ) )
						{
							WriteType( GetUnownIVType( 5, 6 ) );
							matches = true;
						}
					}
					else
					{
						if ( IsLegal( iv1, iv2, C, D ) )
						{
							WriteEventType( GetBAIVType( 3, 4 ), rest );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, C, E ) )
						{
							WriteEventType( GetBAIVType( 3, 5 ), rest );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, C, F ) )
						{
							WriteEventType( GetBAIVType( 3, 6 ), rest );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, D, E ) )
						{
							WriteEventType( GetBAIVType( 4, 5 ), rest );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, D, F ) )
						{
							WriteEventType( GetBAIVType( 4, 6 ), rest );
							matches = true;
						}

						if ( IsLegal( iv1, iv2, E, F ) )
						{
							WriteEventType( GetBAIVType( 5, 6 ), rest );
							matches = true;
						}
					}

				}
			}

			return matches;
		}

		public static bool IsRoaming( int c, int iv1, int iv2 )
		{
			return iv2 == 0 && iv1 == ( c & 0xFF );
		}

		public static bool IsLegal( int iv1o, int iv2o, int iv1f, int iv2f )
		{
			return ( iv1o == iv1f ) && ( iv2o == iv2f );
		}

		public static string GetABNDSIVType( int pos1, int pos2 )
		{
			string message = String.Empty;
			if ( pos1 == 3 && pos2 == 4 ) // C-D
				return "Common NDS (A-B-C-D)";

			return String.Format( "Never seen this. A-B-{0}-{1}", (char)( pos1 + 64 ), (char)( pos2 + 64 ) );
		}

		public static string GetACIVType( int pos1, int pos2 )
		{
			string message = String.Empty;
			if ( pos1 == 4 && pos2 == 5 ) //D-E
				return "Uncommon GBA (A-C-D-E)";

			return String.Format( "Never seen this. A-C-{0}-{1}", (char)( pos1 + 64 ), (char)( pos2 + 64 ) );
		}

		public static string GetADIVType( int pos1, int pos2 )
		{
			return String.Format( "Never seen this. A-D-{0}-{1}", (char)( pos1 + 64 ), (char)( pos2 + 64 ) );
		}

		public static string GetABGBAIVType( int pos1, int pos2 )
		{
			string message = String.Empty;
			if ( pos1 == 3 && pos2 == 4 ) // D-E
				return "Common GBA (A-B-C-D)";
			if ( pos1 == 3 && pos2 == 5 ) //C-E
				return "Uncommon GBA (A-B-C-E)";
			if ( pos1 == 4 && pos2 == 5 ) //D-E
				return "Uncommon GBA (A-B-D-E)";
			if ( pos1 == 4 && pos2 == 6 ) //D-F
				return "Rare GBA (A-B-D-F)";
			if ( pos1 == 5 && pos2 == 6 ) //E-F
				return "Very Rare GBA (A-B-E-F)";
			if ( pos1 == 3 && pos2 == 6 ) //C-F
				return "Ultra Rare GBA (A-B-C-F)";

			return String.Format( "Never seen this. A-B-{0}-{1}", (char)( pos1 + 64 ), (char)( pos2 + 64 ) );
		}

		public static string GetUnownIVType( int pos1, int pos2 )
		{
			string message = String.Empty;
			if ( pos1 == 3 && pos2 == 4 ) // C-D
				return "Common GBA Unown (B-A-C-D)";

			if ( pos1 == 4 && pos2 == 5 ) //D-E
				return "Uncommon GBA Unown (B-A-D-E)";
			if ( pos1 == 3 && pos2 == 5 ) // C-E
				return "Uncommon GBA Unown (B-A-C-E)";

			/*
			if ( pos1 == 4 && pos2 == 6 ) // D-F
				return "Very Rare GBA Unown ({0})"; //Theoretical
			if ( pos1 == 5 && pos2 == 6 ) //E-F
				return "Ultra Rare GBA Unown ({0})"; //Theoretical
			*/

			return String.Format( "Never seen this. B-A-{0}-{1}", (char)( pos1 + 64 ), (char)( pos2 + 64 ) );
		}

		public static string GetBAIVType( int pos1, int pos2 )
		{
			string message = String.Empty;
			if ( pos1 == 3 && pos2 == 4 ) // C-D
				return "Common GBA Event";
/*
			if ( pos1 == 4 && pos2 == 5 ) //D-E
				return "Uncommon GBA Event"; //Theoretical
			if ( pos1 == 3 && pos2 == 5 ) // C-E
				return "Rare GBA Event"; //Theoretical
			if ( pos1 == 4 && pos2 == 6 ) // D-F
				return "Rare GBA Event"; //Theoretical
			if ( pos1 == 5 && pos2 == 6 ) //E-F
				return "Ultra Rare GBA Event"; //Theoretical
*/
			return String.Format( "Never seen this. B-A-{0}-{1}", (char)( pos1 + 64 ), (char)( pos2 + 64 ) );
		}

		public static int[] ParseStats( int first, int second )
		{
			int[] stats = new int[6];

			stats[0] = first & 0x1F; //HP
			stats[1] = ( first & 0x3E0 ) >> 0x5; //Attack
			stats[2] = ( first & 0x7C00 ) >> 0xA; //Defense

			stats[3] = second & 0x1F; //Speed
			stats[4] = ( second & 0x3E0 ) >> 0x5; //Sp. Attack
			stats[5] = ( second & 0x7C00 ) >> 0xA; //Sp. Defense

			return stats;
		}
	}
}