using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class PlatPokemonData : 4GPokemonData
	{
		private ushort m_PlatLocation;
		private ushort m_PlatEggLocation;

		public override ushort Location{ get{ return m_PlatLocation; } }
		public override ushort EggLocation{ get{ return m_PlatEggLocation; } }

		public override void Initialize()
		{
			base.Initialize();

			m_PlatEggLocation = ToUInt16( 0x44 );
			m_PlatLocation = ToUInt16( 0x46 );
		}

		public PlatPokemonData( byte[] data ) : base( data )
		{
		}

		public static PlatPokemonData GetPtPokemon( byte[] data )
		{
			ushort natid = BitConverter.ToUInt16( data, 0x08 );
			ushort eggloc = BitConverter.ToUInt16( data, 0x44 );

			if ( (natid == 446 || natid == 143) && eggloc == 0 ) //Munchlax/Snorlax ~ not hatched/egg
				return PtMunchlaxPokemon( data );

			return new PlatPokemonData( data );
		}
	}
}