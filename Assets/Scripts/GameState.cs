using UnityEngine;
namespace Sram.Core
{
	public class GameState
	{
		public static GameState _instance;
		public static GameState Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = new GameState();
				}
				
				return _instance;
			}
		}	

		public void OnApplicationQuit()
		{
			_instance = null;
		}

		private GameState ()
		{
		}

	}
}

