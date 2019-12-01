using Sram.Noise;
namespace Sram.Configuration
{
	public class TerrainGeneratorConfig
	{
		public NoiseGeneratorType NoiseGeneratorType { get; private set; }
		public int GroundSeed { get; private set; } 
		public int GroundOctaves { get; private set; } 

		public float GroundAmplitude { get; private set; } 
		public float GroundFrequence { get; private set; } 
		public int MountainOctaves { get; private set; } 
		public float MountainAmplitude { get; private set; } 
		public float MountainFrequence { get; private set; } 

		public static TerrainGeneratorConfig GetConfig ()
		{
			return new TerrainGeneratorConfig(){
				NoiseGeneratorType = NoiseGeneratorType.Gen1,
				GroundOctaves=4,
				GroundAmplitude=0.1f,
				GroundFrequence=800.0f,
				MountainOctaves=6,
				MountainAmplitude=0.8f,
				MountainFrequence=1200.0f,
			};
		}
	}
}

