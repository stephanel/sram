using UnityEngine;
using Sram.Configuration;
using System;

namespace Sram
{
	public enum ReasonsAboutDeath
	{
		KilledByEnnemy,
		NotEnoughFood,
		NotEnoughOxygen,
		NotEnoughWater,
		TemperatureDownToZero,
	};

	public class Player : Sram.IGameComponent 
	{
		GameController GameController;
		CharacterController characterController;
		Transform transformCamera;

		public PlayerBase PlayerBase { get; private set; }
		public Inventory Inventory { get; private set; }
		public SpaceSuit SpaceSuit { get; private set; }

		public bool IsAlive { get; private set; }
		public ReasonsAboutDeath ReasonsAboutDeath { get; private set; }

		public PlayerStat HungerStat { get; private set; }
		public PlayerStat OxygenStat { get; private set; }
		public PlayerStat ThirstStat { get; private set; }

        public Vector3 Position { get { return this.transformCamera.position; } }

        float currentElapsed = 0;

		public bool IsGrounded{
			get {
				RaycastHit hit;
				if(Physics.Raycast(this.transformCamera.position, -Vector3.up, out hit))
				{
					if(hit.distance <= 2.0)	
						// 2.0 is an arbitrary value to compensate... compensate what ? we don't know
						// because after the camera down to the ground
						// hit.distance return 1.196686...
						// maybe due to the character motor, character controller, or the FPS input controller script ?!
						return true;
				}
				return false;
			}
		}
		public bool IsMoving{
			get {
//				if(this.IsJumping)
//					return false;
				if(!this.IsGrounded)
					return false;
				return Mathf.Abs(this.characterController.velocity.x) > 1 
					|| Mathf.Abs(this.characterController.velocity.z) > 1;
			}
		}	// IsMoving

		private Player(GameController gameController, Config config){
			this.GameController = gameController;
			this.characterController = GameController.GetComponent<CharacterController>();
			this.transformCamera = this.GameController.transform;
			// respawn method initialize many objects.
			// DO NOT call anything before Respawn method
			this.Respawn (config, true);
			this.ActivateTorchlight ();
		}	// Constructor

		public static Player GetInstance (GameController gameController, Config config) {
			return new Player (gameController, config);
		}	// GetInstance
		
		// Update is called once per frame
		public void Update () {
			if (this.IsAlive) {
				currentElapsed += Time.deltaTime;

				float timeAccelerator = this.GameController.Config.GameTimeConfig.TimeAccelerator;

				this.HungerStat.Update(currentElapsed, timeAccelerator);
				this.OxygenStat.Update(currentElapsed, timeAccelerator);
				this.ThirstStat.Update(currentElapsed, timeAccelerator);

				this.SpaceSuit.Update(currentElapsed, timeAccelerator);

				currentElapsed = currentElapsed % 1; // 1 for 1 seconds
			}
		}

		private void ActivateTorchlight(){
			Transform torchligth = this.transformCamera.FindChild("Torchligth");
			torchligth.gameObject.SetActive (this.SpaceSuit.TorchligthIsActive);
		}	// ActivateTorchlight

		public void SwitchTorchLightState()
		{
			this.SpaceSuit.SwitchTorchLightState ();
			this.ActivateTorchlight ();

		}	// SwitchTorchLightState

		public void Die(ReasonsAboutDeath reason)
		{
			this.IsAlive = false;
			// manage a reason of death ?
			this.ReasonsAboutDeath = reason;
			this.GameController.OnPlayerDeath();
		}	// Die

		public void Respawn(Config config, bool initializePlayer){
			IsAlive = true;
			HungerStat = PlayerStat.GetStat (this, ReasonsAboutDeath.NotEnoughFood, config.PlayerConfig.HungerLevelLostPerSecond, 1.0f, config.PlayerConfig.HungerLevel);
			OxygenStat = PlayerStat.GetStat (this, ReasonsAboutDeath.NotEnoughOxygen, config.PlayerConfig.OxygenLevelLostPerSecond, 1.0f, config.PlayerConfig.OxygenLevel);
			ThirstStat = PlayerStat.GetStat (this, ReasonsAboutDeath.NotEnoughWater, config.PlayerConfig.ThirstLevelLostPerSecond, 1.0f, config.PlayerConfig.ThirstLevel);
			Inventory = Inventory.GetInventory (this, config.InventoryConfig);
			SpaceSuit = SpaceSuit.GetSpaceSuit (this.GameController, this, config.SpaceSuitConfig);

			if (initializePlayer) {
				// do not reset these object on respawn, just on first initialization of the player
				PlayerBase = PlayerBase.GetPlayerBase (this, config.PlayerBaseConfig);
			}
		}	// Respawn

		public IGameObjectInteractable FoundInteraction(){
			IGameObjectInteractable obj = null;

			return obj;
		}

	}
}