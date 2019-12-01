namespace Sram.Noise
{
	public interface INoiser
	{
		float FractalNoise1D(float x, int octNum, float frq, float amp);
		float FractalNoise2D(float x, float y, int octNum, float frq, float amp);
		float FractalNoise3D(float x, float y, float z, int octNum, float frq, float amp);
	}
}

