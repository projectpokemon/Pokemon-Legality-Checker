using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LegalityChecker
{
	public class 3GPokemonData : PokemonData
	{
		private PPData m_PPData;

		public 3GPokemonData( PokemonData pkm ) : base( pkm )
		{
			//Determine PalParkData - Will the proper information be present in the PKM itself? (Check Nicknames with Korean pokes)
			m_PPData = PPData.GetPalParkData( this );
		}

		public override void Checks()
		{
			base.Checks();

			if ( IsEgg || IsHatched || m_PalParkData == null )
			{
				Utility.Output( "Pal-Park: " );
				Utility.OutputInvalidLine( "HACKED" );
				return;
			}

			switch ( m_PPData.Gen4Game )
			{
				case HT4.DP: Utility.OutputLine( "4G Game: Diamond/Pearl" ); break;
				case HT4.Pt: Utility.OutputLine( "4G Game: Platinum" ); break;
				case HT4.HGSS: Utility.OutputLine( "4G Game: Heart Gold/Soul Silver" ); break;
			}

			//Check proper trash byte stuff, and make sure its valid.
			Utility.Output( "Trash Bytes: " );
			if ( m_PPData.IsValid )
			{
				Utility.OutputValidLine( "Valid" );
				if ( m_PPData.IsAnySlot )
					Utility.Output( "Slot #: Indeterminate" );
				else
				{
					string slots = String.Empty;
					bool notfirst = false;

					if ( m_PPData.IsSlot1 )
						notfirst = Utility.Append( ref slots, "1", notfirst );
					if ( m_PPData.IsSlot2 )
						notfirst = Utility.Append( ref slots, "2", notfirst );
					if ( m_PPData.IsSlot3 )
						notfirst = Utility.Append( ref slots, "3", notfirst );
					if ( m_PPData.IsSlot4 )
						notfirst = Utility.Append( ref slots, "4", notfirst );
					if ( m_PPData.IsSlot5 )
						notfirst = Utility.Append( ref slots, "5", notfirst );
					if ( m_PPData.IsSlot6 )
						notfirst = Utility.Append( ref slots, "6", notfirst );

					Utility.Output( "\tSlot #: {0}", slots );
				}

				if ( !m_PPData.Slot1 && m_PPData.SetBytes )
					Utility.Output( "\tSet Bytes: {0:X4}", ToUInt16( 0x54 ) );
			}
			else
				Utility.OutputInvalidLine( "Invalid" );
		}
	}
}