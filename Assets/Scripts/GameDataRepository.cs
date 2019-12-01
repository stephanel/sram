using Sram.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace Sram
{
	public class GameDataRepository
	{
		Dictionary<int, ItemBase> Items;

		static GameDataRepository _instance;
		public static GameDataRepository GetInstance  (GameTimeConfig gameTimeConfig, string filename)
		{
			if (_instance == null) {

				_instance = new GameDataRepository (){
					Items = LoadItemBaseData(gameTimeConfig),//filename),
				};
			}
			return _instance;
		}

		public List<Item> FromRandom(ItemContainer container, System.Random rnd){
			
			ItemBase[] selectableItems = this.GetCapsElligibleItems ((float)rnd.NextDouble ());
			
			List<Item> items = selectableItems.Select(p => p.GetAsDropItem(container, rnd)).ToList();
			
			return items;
		}	// FromRandom

		public ItemBase[] GetCapsElligibleItems(float dropRate){
			return this.Items.Where (p => {
				return p.Value.PopIntoCaps==true && p.Value.DropRate >= dropRate;
			}).Select (p => p.Value).ToArray ();
		}	// PickItem


		private static Dictionary<int, ItemBase> LoadItemBaseData(GameTimeConfig gameTimeConfig){
			Dictionary<int, ItemBase> dic = new Dictionary<int, ItemBase>();

			List<ItemBase> items = ItemBase.GetItemsAsGameData(gameTimeConfig);
			for(int i = 0; i < items.Count; i++)
			{
				dic.Add(items[i].ID, items[i]);
			}

			Debug.Log ("Items loaded: "+dic.Count+" items found.");

			return dic; 
		}	// LoadGameData
		private static Dictionary<int, ItemBase> LoadItemBaseData(GameTimeConfig gameTimeConfig, string filename){
			if (!System.IO.File.Exists (filename)) {
				GameDataRepository.CreateGameData (gameTimeConfig, filename);
			}
			
			Core.GameData.Reader.ItemBaseReader reader = new Core.GameData.Reader.ItemBaseReader (filename);
			reader.Open();
			Dictionary<int, ItemBase> dic = new Dictionary<int, ItemBase>();
			
			while(reader.Read()) 
			{
				dic.Add(reader.Data.ID, reader.Data);
			};			
			reader.Close();
			
			Debug.Log ("Items loaded: "+dic.Count+" items found.");
			
			return dic; 
		}	// LoadGameData

		#region "Game data creation"
		public static void CreateGameData(GameTimeConfig gameTimeConfig, string filename){
			// create game data if not exists
			if (!File.Exists (filename)) {
				Core.GameData.Writer.GameDataWriter writer = new Core.GameData.Writer.GameDataWriter(filename);
				List<ItemBase> items = ItemBase.GetItemsAsGameData(gameTimeConfig);
				for(int i = 0; i < items.Count; i++)
				{
					writer.Write(items[i]);
				}
				writer.Close();
			}
			
		}	// CreateGameData
		#endregion
	}
}

