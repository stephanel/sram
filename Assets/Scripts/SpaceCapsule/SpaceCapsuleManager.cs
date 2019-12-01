using Sram.Configuration;
using Sram.Generators;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Sram
{
	public class SpaceCapsuleManager : IGameComponent, IRandomizableObjectManager<IRandomizableObject>
	{

		public GameController GameController { get; private set;}
		SpaceCapsuleManagerConfig Config;

		public List<IRandomizableObject> GeneratedObjects { get; private set; }

		float currentElapsed = 0;

		public static SpaceCapsuleManager GetInstance(GameController gameController, SpaceCapsuleManagerConfig config) {
			return new SpaceCapsuleManager (){
				GameController = gameController,
				Config = config,
				GeneratedObjects = new List<IRandomizableObject>(),
			};
		}

		public void Update () {
				// generate new capsule if 
			currentElapsed += Time.deltaTime;

			if (currentElapsed >= this.Config.PopIntervalSeconds) {

				Random.seed = System.Environment.TickCount;
				float rate = Random.value;

				if(rate<=this.Config.PopRate)
				{
					this.LaunchNewSpaceCapsule();
				}
			}

			for(int i=0; i<this.GeneratedObjects.Count; i++){
				this.GeneratedObjects[i].Update ();
			}

			currentElapsed = currentElapsed % this.Config.PopIntervalSeconds;
		}

		public void LaunchNewSpaceCapsule(){
			// generate a new capsule...
			SpaceCapsule caps = new SpaceCapsule(this, this.Config.SpaceCapsuleConfig);
			// generate the random content of the caps
			GeneratedObjects.Add(caps);
		}

		public void OnCapsuleLanded(SpaceCapsule caps){
			this.GameController.GUIController.Log (caps.Name + " landed at position: " + caps.LandingPosition);
		}

		public SpaceCapsule FindCapsule(GameObject gameObject) {
			//return this.SpaceCapsules.Where (p => p.SpaceCapsulePrefabInstance == gameObject).SingleOrDefault ();
			var capsule = this.GeneratedObjects.Where (p => p.InternalName == gameObject.name).Cast<SpaceCapsule>().SingleOrDefault ();
            return capsule;
        }	// FindCapsule
	}
}