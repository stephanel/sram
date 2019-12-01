using System;
namespace Sram.Configuration
{
	public class SpaceSuitConfig
	{
		/****
		 * Energy consumption is calculated (on each loop) by applying following formulas :
		 * quantity loast = EnergyLostPerSecond * RatioLostPerSecond
		 * RatioLostPerSecond = LOG(ABS(Ts-Tp)+1)
		 * Ts: space suit tempature
		 * Tp: planet temperature
		 ***/
		public float EnergyLostPerSecond { get; private set; }
		public float EnergyQuantity { get; private set; }
		public float RatioLostPerSecond { get; private set; }
		public float TemperatureLevel { get; private set; }
		public float TorchligthActiveCoeff { get; private set; }
		public bool TorchligthIsActive  { get; private set; }

		public SpaceSuitConfig(float gameTimeMultiplicator){
			//this.EnergyConsumptionRatioFactor = 5.0f; // 5.0f seems to be a good value : to consume all energy during 2.5 hours (in-game) at 1 degree as difference
			this.RatioLostPerSecond = 0.0f;	// default is 0.0f, see above for calculation
			this.EnergyQuantity = 100f;
			this.TemperatureLevel = 21.0f;
			this.TorchligthActiveCoeff = 0.3f;
			this.TorchligthIsActive = false;
			this.EnergyLostPerSecond = 0.3f;

			// calculating consumption
			// levelmax / ( duration * gameTimeMultiplicator )
			// duration in seconds in real-time
			this.EnergyLostPerSecond = this.EnergyQuantity / (5f * 60 * 60 / gameTimeMultiplicator); // 0.1f

		}	// constructor

		public static SpaceSuitConfig GetConfig (float gameTimeMultiplicator) {
			return new SpaceSuitConfig (gameTimeMultiplicator);
		}
	}
}

