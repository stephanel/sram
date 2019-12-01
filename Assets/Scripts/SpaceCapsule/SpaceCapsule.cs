using Sram.Configuration;
using Sram.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace Sram
{
	public class SpaceCapsule : ItemContainer, IGameObjectInteractable, IRandomizableObject {

		GameController GameController;
		SpaceCapsuleManager SpaceCapsuleManager; 

		//public List<Item> Items { get; private set; }

		int lifeDuration;
		System.Guid UID;
		public string Name { get; private set; }

		public string InternalName {
			get {
				if(this.SpaceCapsulePrefabInstance==null)
					return null;
				return this.SpaceCapsulePrefabInstance.name;
			}
		}

		public GameObject SpaceCapsulePrefabInstance { get; private set; }
		Vector3 SpawnPosition;
		public Vector3 LandingPosition { get; private set; }
		bool IsCrached;

		public SpaceCapsule(SpaceCapsuleManager manager, SpaceCapsuleConfig config){
			this.GameController = manager.GameController;
			this.SpaceCapsuleManager = manager;
			//this.Name = Sram.Generators.SpaceCapsuleName.RandomName (manager);
			this.Name = new Sram.Generators.SpaceCapsuleNames().RandomName( (IRandomizableObjectManager<IRandomizableObject>)manager);
			this.UID = System.Guid.NewGuid ();
			this.lifeDuration = config.LifeDurationSeconds;
			this.IsCrached = false;

			// random position where capsule spawns and where capsule crashes
			int x = Random.Range (this.GameController.TerrainManager.Xmin, this.GameController.TerrainManager.Xmax);
			int z = Random.Range (this.GameController.TerrainManager.Zmin, this.GameController.TerrainManager.Zmax);
			float y = this.GameController.TerrainManager.GetHeight (x, z); //Terrain.activeTerrain.SampleHeight (position);

			this.SpawnPosition = new Vector3 (x, 1000, z); 
			this.LandingPosition = new Vector3 (x, y, z); 

			// get random items
			System.Random rnd = new System.Random ();
			// TODO : make items as perishable object
			Items = this.GameController.GameDataRepository.FromRandom(this, rnd);

			// add caps to scene
			this.SpaceCapsulePrefabInstance = (GameObject)GameObject.Instantiate(this.GameController.AssetManager.CapsulePrefab.gameObject);
			this.SpaceCapsulePrefabInstance.name = "SpaceCapsule" + UID.ToString ();
			this.SpaceCapsulePrefabInstance.SetActive(true);
			this.SpaceCapsulePrefabInstance.transform.position = this.SpawnPosition;
			this.SpaceCapsulePrefabInstance.GetComponent<ParticleSystem>().Stop ();
			this.SpaceCapsulePrefabInstance.GetComponent<ParticleSystem>().Clear ();
		}	// constructor

		public void RepositionForDebug(Vector3 newPosition){
			this.SpaceCapsulePrefabInstance.transform.position = newPosition;
		}	// RepositionForDebug

		public void Destruct(){
			for(int i=0; i<this.Items.Count; i++){
				this.Items[i] = null;
			}
			this.Items = null;
			this.SpaceCapsuleManager=null;
			this.GameController = null;
			GameObject.Destroy(this.SpaceCapsulePrefabInstance);
			this.SpaceCapsulePrefabInstance=null;
			System.GC.Collect ();	// force garbage collector
		}	// Destruct

		public void Update()
		{
			if (!IsCrached) {
//				float y = this.GameController.TerrainManager.GetHeight(this.Cube.transform.position);
//				Debug.Log ("SpaceCapsule::Update - currentPosition="+this.Cube.transform.position+" ; ground height="+y);

				if(this.SpaceCapsulePrefabInstance.transform.position.y<=this.LandingPosition.y){
					this.SpaceCapsulePrefabInstance.GetComponent<ParticleSystem>().Play ();
					this.IsCrached=true;
					this.SpaceCapsuleManager.OnCapsuleLanded(this);

					// start alive timing
				}
			}
		}	// Update

		public void Open(){
			foreach (Item item in Items)
				this.GameController.GUIController.Log (item.ItemBase.Name + " x " + item.Quantity);
		}	// Open

	}
}