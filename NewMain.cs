using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class MainProgram
	{
		public static readonly string m_Version = "vB55";

		public static void Main( string[] args )
		{
			Utility.PushColor( ConsoleColor.White );
			Console.WriteLine( "Pokemon Legality Checker - by: Sabresite - {0}", m_Version );
			Utility.PopColor();

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

				if ( data.Length != 136 && data.Length != 236 && data.Length != 220 )
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

			PokemonData poke = PokemonData.GetPokemon( data );

			if ( poke.NotSupported )
			{
				Utility.PushColor( ConsoleColor.Red );
				Console.WriteLine( "Black & White Not Supported" );
				Utility.PopColor();
			}
			else
				LegitChecker.CheckLegality( poke );
		}
	}
}