using Sram.Configuration;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Sram
{
	public interface IInventory
	{
		List<Item> Items { get; }
		int Size { get; }

		void AddItem(ItemBase item);
		void RemoveItem (ItemBase item);
		void Open();
		void Close();
	}
	public class Inventory
	{
		Player Player;
		public List<Item> Items { get; private set; }
		public int SlotCount { get; private set;}

		bool IsOpened;
		int Width;
		int Height;
		int Left;
		int Top;
		const int SlotSize = 48;
		const int SpaceBetweenSlots = 5;
		const int RightLeftMargin = 10;
		const int BottomMargin = 10;
		const int TopMargin = 30;

		public static Inventory GetInventory (Player player, InventoryConfig config)
		{
			int width = config.Width;
			int columnCount = (int)Math.Round((double)((width-RightLeftMargin*2) / (SlotSize)),0);
			int rowCount = (int)Math.Round ((double)(config.SlotCount / columnCount), 0, MidpointRounding.AwayFromZero);

			return new Inventory (){
				Player=player,
				Items = new List<Item>(),
				SlotCount=config.SlotCount,
				IsOpened = false,
				Width=RightLeftMargin*2 + (columnCount*SlotSize)+((columnCount-1)*SpaceBetweenSlots),
				Height=TopMargin+BottomMargin+(rowCount*SlotSize)+((rowCount-1)*SpaceBetweenSlots), // title height + rows height 
				Left=Screen.width / 2 - 290, // 290=280+10
				Top=10,
			};
		}

		public void AddItem(Item item)
		{
			if (this.Items.Count >= this.SlotCount)
				 new FullInventoryException ();

			this.Items.Add (item);
		}

		public void RemoveItem (Item item)
		{
			this.Items.Remove (item);
		}

		public void SwithchOpenState(){
			IsOpened=!IsOpened;
			Console.WriteLine ("IsOpened=" + IsOpened);
		}

		public void ToGUI(){
			if (this.IsOpened) {
				GUI.Window(0, new Rect(Left, Top, Width, Height), DoMyWindow, "Inventory");  
			}
		}

		void DoMyWindow(int windowID){

			int columnCount = (int)Math.Round((double)(Width / (SlotSize)),0);
			int rowCount = (int)Math.Round ((double)(SlotCount / columnCount), 0);

			int count = 0;
			for (int i=0; i<rowCount; i++) {
				if(count>=this.SlotCount)
					break;
				for (int j=0; j<columnCount; j++) {
					if(count>=this.SlotCount)
						break;
					GUI.Box (new Rect (RightLeftMargin + (SlotSize+SpaceBetweenSlots)*j, TopMargin + (SlotSize+SpaceBetweenSlots)*i, SlotSize, SlotSize), "");
					count++;
				}
			}
		}
	}

	public class FullInventoryException : Exception
	{
		public FullInventoryException() : base("Inventory is full!")
		{ } 
	}

}
