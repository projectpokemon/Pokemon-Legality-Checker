using System;

namespace LegalityChecker
{
	public class PtMunchlaxPokemonData : PtPokemonData
	{
		public PtMunchlaxPokemonData( byte[] data ) : base( data )
		{
		}
/*
		public bool IsValidMunchlax()
		{
			//Location must be IsValidTree
			//Not hatched/egg
			//ABCD
		}
*/
		public override bool IsValidLocation()
		{
			return IsValidTree();
		}

		public override bool IsValidAlgorithm( out string message )
		{
			if ( IsValidMunchlax() )
			{
				message = "Honey Tree Munchlax";
				return true;
			}
			else
			{
				message = String.Format( "Hacked {0}", GetSpecies() );
				return false;
			}
		}

		public bool IsValidTree()
		{
			byte[] indices = new byte[4];
			indices[0] = (byte)(((SID>>0x8)&0xFF)%0x15);
			indices[1] = (byte)((SID&0xFF)%0x15);
			indices[2] = (byte)(((TID>>0x8)&0xFF)%0x15);
			indices[3] = (byte)((TID&0xFF)%0x15);

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
				if ( m_HoneyLocations[indices[i]] == Location )
					return true;

			return false;
		}

		private static readonly ushort[] m_HoneyLocations = new ushort[]
		{
			20, //Route 205 South
			20, //Route 205 North
			21, //Route 206
			22, //Route 207
			23, //Route 208
			24, //Route 209
			25, //Route 210 South
			25, //Route 210 North
			26, //Route 211
			27, //Route 212 East
			27, //Route 212 West
			28, //Route 213
			29, //Route 214
			30, //Route 215
			33, //Route 218
			36, //Route 221
			37, //Route 222
			47, //WindWorks
			48, //Eterna
			49, //Feugo
			58, //Floaroma
		};
	}
}