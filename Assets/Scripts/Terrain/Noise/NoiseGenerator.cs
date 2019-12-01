using System;
namespace Sram.Noise
{
	public class NoiseGenerator
	{
		INoiser m_generator;

		public NoiseGenerator (NoiseGeneratorType generator, int seed)
		{
			switch(generator)
			{
				case NoiseGeneratorType.Gen1:
					m_generator = new PerlinNoiseImpl1(seed);
					break;

				default:
					throw new NotImplementedException(generator+" is not yet implemented!");
			}
		}

		public float FractalNoise1D(float x, int octNum, float frq, float amp)
		{
			return m_generator.FractalNoise1D (x, octNum, frq, amp);
		}

		public float FractalNoise2D(float x, float y, int octNum, float frq, float amp){
			return m_generator.FractalNoise2D (x, y, octNum, frq, amp);
		}

		public float FractalNoise3D(float x, float y, float z, int octNum, float frq, float amp){
			return m_generator.FractalNoise3D (x, y, z, octNum, frq, amp);
		}
	}

	public enum NoiseGeneratorType
	{
		Gen1,
		Gen2,
		Gen3,
	}

}