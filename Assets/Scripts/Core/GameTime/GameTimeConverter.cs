namespace Sram
{
	public class GameTimeConverter
	{
		private GameTimeConverter ()
		{
		}

		public static float ToBase1000(float currentTimeInSeconds){
			return currentTimeInSeconds / 86400 * 1000;	// base 1000 about 24 heures = 24 heures = 86400 seconds
		}
	}
}

