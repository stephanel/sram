using UnityEngine;
namespace Sram.Configuration
{
	public class PlanetConfig
	{
		public float TemperatureLevel { get; private set; }

		public static PlanetConfig GetPlanetConfig ()
		{
			return new PlanetConfig(){
				TemperatureLevel = 20.0F,
			};
		}

	}
}

