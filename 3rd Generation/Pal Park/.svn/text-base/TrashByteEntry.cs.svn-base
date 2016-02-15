using System;
using System.Globalization;

namespace LegalityChecker
{
	public class TrashByteEntry
	{
		private TrashByteChar[][] m_Chars;

		public TrashByteEntry( string formatted ) : this( ToChars( formatted ) )
		{
		}

		public TrashByteEntry( params TrashByteChar[][] chars )
		{
			m_Chars = chars;
		}

		public TrashByteChar[] this[int index] 
		{
			return m_Chars[index];
		}

		public bool IsWithin( int index, byte check )
		{
			for ( int i = 0; i < m_Chars[index].Length; i++ )
				if ( m_Chars[index][i].IsWithin( check ) )
					return true;

			return false;
		}

		public static TrashByteChar[][] ToChars( string formatted )
		{
			string[] commachars = formatted.Split( "," );

			if ( commachars.Length != 18 )
				throw new Exception( "There must be 18 values separated by commas." );

			TrashByteChars[][] result = new TrashByteChars[18][];

			for ( int i = 0; i < 18; i++ )
			{
				string[] vbarchars = commachars[i].Split( "|" );
				TrashByteChars[] entry = new TrashByteChars[vbarchars.Length];
				for ( int j = 0; j < vbchars.Length; j++ )
				{
					string[] hypchars = vbarchars[j].Split( "-" );

					if ( hypchars.Length == 2 )
					{
						byte min = Byte.Parse( hypchars[0], NumberStyles.HexNumber );
						byte max = Byte.Parse( hypchars[1], NumberStyles.HexNumber );

						if ( min > max )
						{
							byte temp = min;
							min = max;
							max = temp;
						}

						entry[j] = new TrashByteChar( min, max );
					}
					else if ( hypchars.Length == 1 )
					{
						byte num = Byte.Parse( hypchars[0], NumberStyles.HexNumber );
						entry[j] = new TrashByteChar( num, num );
					}
					else
						throw new Exception( "Ranges must contain only two numbers." );
				}

				result[i] = entry;
			}
		}
	}
}