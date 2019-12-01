using Sram.Configuration;
using UnityEngine;
namespace Sram
{
	public class InputManager
	{
		GameController GameController;
		Config Config;

		public static InputManager GetInstance (GameController gameController, Config config)
		{
			return new InputManager (){
				GameController=gameController,
				Config=config,
			};
		}

		public void Update(){
			// FPS
			if(Input.GetKeyDown(Config.KeyConfig.ShowHideFps)) {
				// show fps on screen
				this.Config.ShowHideFps = !this.Config.ShowHideFps;
			}

			// Launging sapce caps
			if(Input.GetKeyDown(Config.KeyConfig.SpaceCapsuleLauncher)) {
				// show fps on screen
				this.GameController.SpaceCapsuleManager.LaunchNewSpaceCapsule();
			}

			// Pause
			if(Input.GetKeyDown(Config.KeyConfig.PauseGame)) {
				// pause the game
				this.GameController.SwitchPauseState();
				if(this.GameController.IsPaused) 
					this.GameController.Pause(); 
				else
					this.GameController.UnPause ();
			}

			// Time acceleration
			if (Input.GetKeyDown (Config.KeyConfig.TimeAcceleratorPlus))
				if(this.GameController.Config.GameTimeConfig.TimeAccelerator<256f)
					this.GameController.Config.GameTimeConfig.TimeAccelerator *= 2;
			if (Input.GetKeyDown (Config.KeyConfig.TimeAcceleratorLess))
				if(this.GameController.Config.GameTimeConfig.TimeAccelerator>0.125f)
					this.GameController.Config.GameTimeConfig.TimeAccelerator /= 2;

			// Inventory
			if (Input.GetKeyDown (Config.KeyConfig.InventoryOpener)) {
				this.GameController.SwitchInventoryOpenState();
			}

			// player's torchlight
			if (Input.GetKeyDown (Config.KeyConfig.TorchligthOnOff)) {
				this.GameController.SwitchPlayerTorchlightState();
			}

			// Dispatch interaction with a game object
			if (Input.GetKeyDown (Config.KeyConfig.ObjectInteractionKey)) {
				this.GameController.DispathInteraction(InteractionType.Open);
			}

		}

	}
}

