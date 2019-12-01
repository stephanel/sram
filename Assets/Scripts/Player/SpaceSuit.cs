using System;
using Sram.Configuration;
namespace Sram
{
	public class SpaceSuit
	{
		GameController GameController; 
		Player Player;

		public GameStat EnergyStat { get; private set; }

		public float TemperatureLevel { get; private set; }
		//float EnergyConsumptionRatioFactor;

		public bool TorchligthIsActive { get; private set; }
		public float TorchligthActiveCoeff { get; private set; }

		public static SpaceSuit GetSpaceSuit(GameController gameController, Player player, SpaceSuitConfig config)
		{
			return new SpaceSuit (){
				GameController=gameController,
				Player=player,
				EnergyStat = GameStat.GetStat(config.EnergyLostPerSecond, config.RatioLostPerSecond, config.EnergyQuantity), 
				TemperatureLevel = config.TemperatureLevel,
				//EnergyConsumptionRatioFactor = config.EnergyConsumptionRatioFactor,
				TorchligthActiveCoeff = config.TorchligthActiveCoeff,
				TorchligthIsActive = config.TorchligthIsActive,
			};
		}

		public void Update (float currentElapsed, float timeAccelerator) {
			if (currentElapsed > 1) {	// 1 for 1 seconds

				// the consumption of energy depends on the temperature difference
				float diff = this.GameController.Planet.TemperatureLevel - this.TemperatureLevel;
				// define coeff to maximize consumption due to the usage of the torchlight
				float coeff =  this.TorchligthIsActive ? TorchligthActiveCoeff : 0f;
				this.EnergyStat.RatioLostPerSecond = (float)(Math.Log(Math.Abs (diff)+ 1) + coeff);// * -1; // * -1 because Log function returns always a positive value !
				this.EnergyStat.Update(currentElapsed, timeAccelerator);

				if(this.EnergyStat.Quantity <= 0){
					this.TemperatureLevel = 0;
					this.Player.Die(ReasonsAboutDeath.TemperatureDownToZero);
				}
			}
			currentElapsed = currentElapsed % 1; // 1 for 1 seconds
		}

		public void SwitchTorchLightState()
		{
			this.TorchligthIsActive = !this.TorchligthIsActive;
		}	// SwitchTorchLightState
	}
}

