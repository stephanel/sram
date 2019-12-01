using Sram.Configuration;
using System;
namespace Sram
{
	public class PlayerStat : Sram.GameStat
	{
		Player Player;
		ReasonsAboutDeath ReasonsAboutDeath;

//		float QuantitylostPerSecond;
//		public float Quantity { get; private set; }

		private PlayerStat(float lostPerSecond, float ratioLostPerSecond, float defaultQuantity) 
			: base(lostPerSecond, ratioLostPerSecond, defaultQuantity) 
		{ }

		public static PlayerStat GetStat (Player player, ReasonsAboutDeath reasonsAboutDeath, float lostPerSecond, float ratioLostPerSecond, float defaultQuantity)
		{
			return new PlayerStat (lostPerSecond, ratioLostPerSecond, defaultQuantity){
				Player=player,
				ReasonsAboutDeath=reasonsAboutDeath,
			};
		}

		public override void Update (float currentElapsed, float timeAccelerator) {
			base.Update (currentElapsed, timeAccelerator);
			if (this.Quantity <= 0) {
				this.Player.Die (this.ReasonsAboutDeath);
			}
		}
	}
}

