namespace Sram
{
	public class SpaceCapsuleConfig
	{
		public int LifeDurationSeconds { get; private set; }

		public static SpaceCapsuleConfig GetConfig()
		{
			return new SpaceCapsuleConfig(){
				LifeDurationSeconds = 15 * 60, // 15 minutes
			};
		}
	}
}

