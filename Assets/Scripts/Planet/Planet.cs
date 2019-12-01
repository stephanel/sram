using Sram.Configuration;
using UnityEngine;
namespace Sram
{
	public class Planet: IGameComponent
	{
		GameController GameController;

		public float TemperatureLevel { get; private set; }

		public static Planet GetInstance (GameController gameController, PlanetConfig config)
		{
			return new Planet (){
				GameController=gameController,
				TemperatureLevel=config.TemperatureLevel,
			};
		}

		public void Update(){
		}
	}
}

