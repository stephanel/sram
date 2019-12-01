using Sram.Generators;
using System;
namespace Sram
{
	public class Item : ICloneable, IRandomizableObject
	{

		public ItemBase ItemBase { get; private set; }
		public int Quantity { get; private set; }

		ItemContainer InteractableContainerObject;

		private Item(){ }
		public Item (ItemBase itemBase, ItemContainer container, Random rnd)
		{
			this.ItemBase = itemBase;
			this.Quantity = rnd.Next (itemBase.MinDropQuantity, itemBase.MaxDropQuantity);
			this.InteractableContainerObject = container;
		}

		public void Transfert(ItemContainer containerFrom, ItemContainer containerTo) {
			containerFrom.Items.Remove (this);
			containerTo.Items.Add (this);
			this.InteractableContainerObject = containerTo;
		}

		#region "ICloneable"
		public object Clone(){
			return new Item(){
				ItemBase = this.ItemBase,
				Quantity = this.Quantity,
			};
		}
		#endregion

		#region "IRandomizableObject"
		public string Name { get { return this.ItemBase.Name; } }
		public string InternalName {
			get {
				return this.Name;
			}
		}
		public void Update(){

		}	// Update
		#endregion

	}
}

