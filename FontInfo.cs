using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LegalityChecker
{
	public class FontChar : IEntity
	{
		private ushort m_PokeChar;
		private ushort m_Unicode;

		public ushort PokeChar{ get{ return m_PokeChar; } set{ m_PokeChar = value; } }
		public ushort Unicode{ get{ return m_Unicode; } set{ m_Unicode = value; } }

		public bool IsTerminator{ get{ return m_PokeChar == 0xFFFF; } }

		ushort IEntity.ID{ get{ return m_PokeChar; } set{ m_PokeChar = value; } }

		public FontChar( ushort pokechar, ushort unicode )
		{
			m_PokeChar = pokechar;
			m_Unicode = unicode;
		}

		public FontChar()
		{
		}

		public void Deserialize( GenericReader reader )
		{
			m_PokeChar = reader.ReadUShort();
			m_Unicode = reader.ReadUShort();
		}

		public void Serialize( GenericWriter writer )
		{
			writer.Write( m_PokeChar );
			writer.Write( m_Unicode );
		}
	}

	public abstract class PokeString
	{
		private ushort[] m_PokeChars;

		public ushort[] ToArray(){ return m_PokeChars; }
		public bool IsTerminatedAt(int number){ return m_PokeChars[number] == 0xFFFF; }

		public abstract int Length{ get; }
		public abstract bool HasTrash{ get; }

		public PokeString( GenericReader reader )
		{
			m_PokeChars = new ushort[Length];
			for ( int i = 0; i < Length; i++ )
			{
				m_PokeChars[i] = reader.ReadUShort();
				if ( !HasTrash && IsTerminatedAt( i ) )
					break;
			}
		}

		public PokeString( ushort[] chars )
		{
			m_PokeChars = chars;
		}

		public PokeString( string chars, int size )
		{

			m_PokeChars = new ushort[size];

			for ( int i = 0; i < chars.Length && i < size; i++ )
				m_PokeChars[i] = PokeData.FindPokeChar( chars[i] );

			if ( chars.Length < size )
				m_PokeChars[chars.Length] = 0xFFFF; //ushort terminator
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			for ( int i = 0; i < m_PokeChars.Length; i++ )
			{
				if ( IsTerminatedAt( i ) )
					break;
				else
				{
					//Console.WriteLine( "Char: {0:X}", m_PokeChars[i] );
					if ( m_PokeChars[i] > 0 )
						builder.Append( (char)PokeData.FindUniChar( m_PokeChars[i] ) );
				}
			}

			return builder.ToString();
		}

		public virtual void Serialize( GenericWriter writer )
		{
			for ( int i = 0; i < Length; i++ )
			{
				writer.Write( (ushort)m_PokeChars[i] );
				if ( !HasTrash && IsTerminatedAt( i ) )
					break;
			}
		}
	}

	public class PokeString10Info : PokeString //Pokemon Names in database
	{
		public override int Length{ get{ return 11; } }
		public override bool HasTrash{ get{ return false; } }

		public PokeString10Info( GenericReader reader ) : base( reader )
		{
		}

		public PokeString10Info( ushort[] chars ) : base( chars )
		{
		}

		public PokeString10Info( string chars, int size ) : base( chars, size )
		{
		}
	}

	public class PokeString10 : PokeString //Pokemon Names
	{
		public override int Length{ get{ return 11; } }
		public override bool HasTrash{ get{ return true; } }

		public PokeString10( GenericReader reader ) : base( reader )
		{
		}

		public PokeString10( ushort[] chars ) : base( chars )
		{
		}

		public PokeString10( string chars, int size ) : base( chars, size )
		{
		}
	}

	public class PokeString7 : PokeString //Original Trainer Name
	{
		public override int Length{ get{ return 8; } }
		public override bool HasTrash{ get{ return true; } }

		public PokeString7( GenericReader reader ) : base( reader )
		{
		}

		public PokeString7( ushort[] chars ) : base( chars )
		{
		}

		public PokeString7( string chars, int size ) : base( chars, size )
		{
		}
	}

	public class PokeString8Info : PokeString //Box Names
	{
		public override int Length{ get{ return 9; } }
		public override bool HasTrash{ get{ return false; } }

		public PokeString8Info( GenericReader reader ) : base( reader )
		{
		}

		public PokeString8Info( ushort[] chars ) : base( chars )
		{
		}

		public PokeString8Info( string chars, int size ) : base( chars, size )
		{
		}
	}
}