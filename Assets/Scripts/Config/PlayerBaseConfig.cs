using System;
namespace Sram.Configuration
{
	public class PlayerBaseConfig
	{
		public float EnergyQuantity { get; private set; }
		public float FoodQuantity { get; private set; }
		public float OxygenQuantity { get; private set; }
		public float WaterQuantity { get; private set; }

		public static PlayerBaseConfig GetPlayerBaseConfig ()
		{
			return new PlayerBaseConfig (){
				EnergyQuantity=5000F,
				FoodQuantity= 5000F,
				WaterQuantity = 5000F,
				OxygenQuantity = 5000F,
			};
		}
	}
}

