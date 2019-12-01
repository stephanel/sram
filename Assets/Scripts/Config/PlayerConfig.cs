using System;
namespace Sram.Configuration
{
	public class PlayerConfig
	{
		public float HungerLevelLostPerSecond { get; private set; }
		public float HungerLevel { get; private set; }

		public float OxygenLevelLostPerSecond { get; private set; }
		public float OxygenLevel { get; private set; }

		public float ThirstLevelLostPerSecond { get; private set; }
		public float ThirstLevel { get; private set; }

		private PlayerConfig(float gameTimeMultiplicator){
			this.HungerLevel = 100f;
			this.ThirstLevel = 100f;
			this.OxygenLevel = 100f;

			// calculating consumption
			// levelmax / ( duration * gameTimeMultiplicator )
			// duration in seconds in real-time
			this.HungerLevelLostPerSecond = this.HungerLevel / (8 * 60 * 60 / gameTimeMultiplicator); // 0.0069f
			this.ThirstLevelLostPerSecond =this.ThirstLevel / (4 * 60 * 60 / gameTimeMultiplicator);	// 0.0139f
			this.OxygenLevelLostPerSecond = this.OxygenLevel / (2 * 60 * 60 / gameTimeMultiplicator); // 0.0278f

		}	// constructor

		public static PlayerConfig GetConfig(float gameTimeMultiplicator)
		{
			return new PlayerConfig (gameTimeMultiplicator);
		}
	}
}

