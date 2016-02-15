using System;

namespace LegalityChecker
{
	public enum HGSSPokeBall : byte
	{
		None		= 0,
		MasterBall,
		UltraBall,
		GreatBall,
		PokeBall, //Set this when its HG/SS specific
		SafariBall,
		NetBall,
		DiveBall,
		NestBall,
		RepeatBall,
		TimerBall,
		LuxuryBall,
		PremierBall,
		DuskBall,
		HealBall,
		QuickBall,
		CherishBall,
		//HG/SS Only
		FastBall,
		LevelBall,
		LureBall,
		HeavyBall,
		LovelyBall,
		FriendBall,
		MoonBall,
		CompBall
	}

	public class HGSSPokemonData : PlatPokemonData
	{
		private HGSSPokeBall m_HGSSPokeBall;
		public HGSSPokeBall HGSSPokeBall{ get{ return m_HGSSPokeBall; } }

		public override void Initialize()
		{
			base.Initialize();

			m_HGSSPokeBall = m_Data[0x86];
		}

		public override bool IsValidPokeBall()
		{
			return ( (byte)PokeBall < (byte)HGSSPokeBall.FastBall && (byte)m_HGSSPokeBall == (byte)PokeBall ) || PokeBall == PokeBall.PokeBall && m_HGSSPokeBall >= HGSSPokeBall.FastBall;
		}

		public HGSSPokemonData( byte[] data ) : base( data )
		{
		}

		public static HGSSPokemonData GetHGSSPokemon( byte[] data )
		{
			if ( BitConverter.ToUInt16( data, 0x46 ) == 0xE9 ) //Pokewalker!
				return new WalkerPokemonData( data );

			return new HGSSPokemonData( data );
		}
	}
}