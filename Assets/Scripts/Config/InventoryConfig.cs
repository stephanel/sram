using System;
namespace Sram.Configuration
{
	public class InventoryConfig
	{
		public int SlotCount { get; private set; }
		public int Width { get; private set; }

		public static InventoryConfig GetConfig ()
		{
			return new InventoryConfig (){
				SlotCount=32,
				Width=300,
			};
		}
	}
}

