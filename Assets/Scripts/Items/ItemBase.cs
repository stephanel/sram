using Sram.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
namespace Sram
{
    [DebuggerDisplay("{Name}")]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public class ItemBase : IEquatable<ItemBase>, Core.GameData.Writer.IWritableGameData
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public float DropRate { get; set; }
		public bool PopIntoCaps { get; set; }
		
		public int MaxDropQuantity { get; set; }
		public int MinDropQuantity { get; set; }

		public bool IsPerishable { get; set; }
		public float Lifetime { get; set; }
		
		public static ItemBase GetInstance (int id, string name)
		{
			return new ItemBase(){
				ID=id,
				Name=name,
			};
		}
		
		public Item GetAsDropItem(ItemContainer container, Random rnd){
			return new Item (this, container, rnd);
		}	// GetAsDropItem
		
		
//		public static List<Item> FromRandom(GameDataRepository repository, IGameObjectInteractable container, Random rnd){
//			
//			ItemBase[] selectableItems = repository.GetCapsElligibleItems ((float)rnd.NextDouble ());
//			
//			List<Item> items = selectableItems.Select(p => p.GetAsDropItem(container, rnd)).ToList();
//			
//			return items;
//		}	// FromRandom
		
		
		
		
		
		#region "IComparable"
		public bool Equals(ItemBase other)
		{
			//Return the difference in power.
			return ID == other.ID;
		}	// Equals
		#endregion
		
		#region "Data"
		internal sealed class ItemBaseSize
		{
			public static int _size;
			
			static ItemBaseSize()
			{
				_size = Marshal.SizeOf(typeof(ItemBase));
			}
			
			public static int Size
			{
				get
				{
					return _size;
				}
			}
		}
		public byte[] ToByteArray()
		{
			byte[] buff = new byte[ItemBaseSize.Size];//faster than [Marshal.SizeOf(typeof(TestStruct))];
			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			Marshal.StructureToPtr(this, handle.AddrOfPinnedObject(), false);
			handle.Free();
			return buff;
		}
		
		public static ItemBase ReadBlock(BinaryReader br)
		{
			byte[] buff = br.ReadBytes(ItemBaseSize.Size);//faster than (Marshal.SizeOf(typeof(ItemBase)));
			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			ItemBase s = (ItemBase)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ItemBase));
			handle.Free();
			return s;
		}
		
		//		public static ItemBase ReadFields(BinaryReader br)
		//		{
		//			ItemBase _data = new ItemBase();
		//			_data.ID = br.ReadInt32();
		//			_data.Name = br.ReadString ();
		//			_data.DropRate = br.ReadSingle();
		//			_data.PopIntoCaps = br.ReadBoolean();
		//			_data.MaxDropQuantity = br.ReadInt32();
		//			_data.MinDropQuantity = br.ReadInt32 ();
		//			return _data;
		//		}
		
		public static List<ItemBase> GetItemsAsGameData(GameTimeConfig gameTimeConfig){
			List<ItemBase> items = new List<ItemBase> ();
			items.Add (new ItemBase (){ID=1, Name="Food", DropRate=0.95f, PopIntoCaps=true, MaxDropQuantity=800, MinDropQuantity=1,
				IsPerishable=true, Lifetime=86400 * 2  / gameTimeConfig.TimeMultiplicator});
			items.Add (new ItemBase (){ID=2, Name="Water", DropRate=0.95f, PopIntoCaps=true, MaxDropQuantity=1000, MinDropQuantity=1,
				IsPerishable=true, Lifetime=86400 * 6  / gameTimeConfig.TimeMultiplicator});
			items.Add (new ItemBase (){ID=3, Name="Oxygen", DropRate=0.85f, PopIntoCaps=true, MaxDropQuantity=600, MinDropQuantity=1,
				IsPerishable=false});
			items.Add (new ItemBase (){ID=4, Name="Energy", DropRate=0.75f, PopIntoCaps=true, MaxDropQuantity=400, MinDropQuantity=1,
				IsPerishable=true, Lifetime=86400 * 11  / gameTimeConfig.TimeMultiplicator});
			return items;
		}
		#endregion
	}
}

