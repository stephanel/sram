namespace Sram.Configuration
{
	public class SpaceCapsuleManagerConfig
	{
		public SpaceCapsuleConfig SpaceCapsuleConfig { get; private set; }

		public int PopIntervalSeconds { get; private set; }	// interval in seconds
		public float PopRate { get; private set; }

		public static SpaceCapsuleManagerConfig GetConfig()
		{
			return new SpaceCapsuleManagerConfig(){
				SpaceCapsuleConfig = SpaceCapsuleConfig.GetConfig (),
				PopIntervalSeconds = 10 * 60,	// 10 minutes
				PopRate = 0.097f,	// 0.015f
			};
		}
	}
}

