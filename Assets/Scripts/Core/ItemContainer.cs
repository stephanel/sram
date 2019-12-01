using System.Collections.Generic;

namespace Sram
{
	public class ItemContainer : IGameObjectContainer<Item>
	{
		public List<Item> Items { get; protected set; }

		public ItemContainer ()
		{
			this.Items = new List<Item> ();
		}
	}
}

