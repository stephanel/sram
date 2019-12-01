using System;
using UnityEngine;
namespace Sram
{
	public class GameObjectInteractionManager
	{
		GameController GameController;

		public static GameObjectInteractionManager GetInstance(GameController gameController){
			return new GameObjectInteractionManager (){
				GameController=gameController,
			};
		}

//		public void Dispatch(InteractionType interaction){
//			// get object with interact to
//			IGameObjectInteractable obj = this.GameController.Player.FoundInteraction();
//
//			if (obj!=null) {
//				switch (interaction) {
//					case InteractionType.Open:
//							obj.Open ();
//							break;
//
//					default:
//							break;
//				}
//			}
//		}	// Dispatch

		public void Dispatch(GameObject gameObject, InteractionType interaction){
			if (gameObject==null)
				return;

			// get object with interact to
			IGameObjectInteractable obj = this.GameController.SpaceCapsuleManager.FindCapsule(gameObject);
			if (obj==null)
				return;

			switch (interaction) {
				case InteractionType.Open:
					obj.Open ();
					break;
					
				default:
					break;
			}

		}	// Dispatch

//		public void Dispatch(ControllerColliderHit hit){
//			if (hit.gameObject.tag == "SpaceCapsule") 
//			{
//				IGameObjectInteractable caps = this.GameController.SpaceCapsuleManager.FindCapsule(hit.gameObject);
//				if(caps!=null){
//					caps.Open ();
//				}
//			}
//		}	// Dispatch
	
	}
}

