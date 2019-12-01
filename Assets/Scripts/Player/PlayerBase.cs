using Sram.Configuration;
using System;
namespace Sram
{
	public class PlayerBase
	{
		Player Player;

		public static PlayerBase GetPlayerBase(Player player, PlayerBaseConfig config)
		{
			return new PlayerBase (){
				Player=player,
			};
		}
	}
}

