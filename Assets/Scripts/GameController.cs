using UnityEngine;
//using Sram;
using Sram.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Sram
{
	public class GameController : MonoBehaviour, IGameController
	{
		public GameObject GUIControllerParent;
		public GUIController GUIController { get; private set; }
		public AssetManager AssetManager { get; private set; } 

		public Config Config { get; private set; }
		public FpsCalculator FpsCalculator { get; private set; }

		public GameDataRepository GameDataRepository { get; private set; }

		public InputManager InputManager { get; private set; }
		public SoundManager SoundManager { get; private set; }
		public GameObjectInteractionManager GameObjectInteractionManager { get; private set; }

		public GameTime GameTime { get; private set; }
		public Planet Planet { get; private set; }
		public Player Player { get; private set; }
		public SpaceCapsuleManager SpaceCapsuleManager { get; private set; }

		List<IGameComponent> GameComponents;

		// prefabs and materials
		public TerrainManager TerrainManager { get; private set; }
		Sun Sun;
//		public Texture2D m_splat0, m_splat1;
//		public Transform CapsulePrefab;

		Vector3 BasePosition;
		public Vector3 PlayerPosition {
			get {
				return this.transform.position;
			}
		}

		//float CurrentElapsed = 0;

		public bool IsPaused { get; private set; }

		void Awake(){
			this.Config = Config.GetConfig ();

			this.GameDataRepository = GameDataRepository.GetInstance (
				this.Config.GameTimeConfig,
				System.IO.Path.Combine(Application.dataPath, "gamedata.dat"));
		}	// Awake

		void Start () {
			this.GUIController  = GUIControllerParent.GetComponent<GUIController> ();
			this.AssetManager = GetComponent <AssetManager> ();
			
			this.BasePosition = this.transform.position;
			//this.GameIsLoading = true;
			this.IsPaused = false;

			this.GameComponents = new List<IGameComponent> ();

			this.FpsCalculator = FpsCalculator.GetInstance ();
			
			this.InputManager = InputManager.GetInstance (this, this.Config);
			this.SoundManager = SoundManager.GetInstance (this);
			this.GameObjectInteractionManager = GameObjectInteractionManager.GetInstance (this);
			
			this.GameTime = GameTime.GetInstance (this.Config.GameTimeConfig);
			this.Planet = Planet.GetInstance (this, this.Config.PlanetConfig);
			this.Player = Player.GetInstance (this, this.Config);
			this.SpaceCapsuleManager = SpaceCapsuleManager.GetInstance (this, this.Config.SpaceCapsuleManagerConfig);
			// Sun
			this.Sun = Sun.GetInstance (this);

			// add to components
			this.GameComponents.Add (this.FpsCalculator);
			this.GameComponents.Add (this.GameTime);
			this.GameComponents.Add (this.Planet);
			this.GameComponents.Add (this.Player);
			this.GameComponents.Add (this.SpaceCapsuleManager);
			this.GameComponents.Add (this.Sun);

			// generate terrain
			this.TerrainManager = 
				TerrainManager.GetInstance(this, this.Config.TerrainGeneratorConfig);

            var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            //Debug.Log("Level loaded -> " + Application.loadedLevelName + ": " + Application.loadedLevel);
            Debug.Log("Level loaded -> " + activeScene.name + ": " + activeScene.buildIndex);

            SpaceCapsule caps = new SpaceCapsule (this.SpaceCapsuleManager, this.Config.SpaceCapsuleManagerConfig.SpaceCapsuleConfig);
			caps.RepositionForDebug (new Vector3(55, this.TerrainManager.GetHeight(55, -26), -26));
			this.SpaceCapsuleManager.GeneratedObjects.Add (caps);

		}	// Start

		// Update is called once per frame
		void Update () {
			this.InputManager.Update ();
			this.SoundManager.Update ();

			if (!this.IsPaused) {
				//CurrentElapsed += Time.deltaTime;

//				float timeAccelerator = this.Config.GameTimeConfig.TimeAccelerator;

//				// update fps
//				this.FpsCalculator.Update ();
//
//				// update game time
//				this.GameTime.Update();
//
//				// update sun position
//				this.Sun.Update();
//
//				// update player status
//				this.Player.Update ();
//
//				// update space capsule manager
//				this.SpaceCapsuleManager.Update ();

				for(int i = 0; i< this.GameComponents.Count; i++){
					this.GameComponents[i].Update();
				}

				//CurrentElapsed = CurrentElapsed % 1; // 1 for 1 seconds
			}
		}	// Update

		void EnablePlayerControl()
		{
			CharacterController controller = GetComponent<CharacterController> ();
			if (controller != null) {
				controller.enabled = true;
				controller=null;
			}
			MouseLook component = GetComponent<MouseLook> ();
			if (component != null) {
				component.enabled = true;
				component=null;
			}
		}	// EnablePlayerControl

		void DisablePlayerControl()
		{
			CharacterController controller = GetComponent<CharacterController> ();
			if (controller != null) {
				controller.enabled = false;
				controller=null;
			}
			MouseLook component = GetComponent<MouseLook> ();
			if (component != null) {
				component.enabled = false;
				component=null;
			}
		}	// DisablePlayerControl

		public void OnPlayerDeath(){
			Cursor.visible = true;
			this.DisablePlayerControl ();
		}	// OnPlayerDeath

		public void OnPlayerRespawn(){
			this.transform.position = this.BasePosition;
			this.Player.Respawn (this.Config, false);
			Cursor.visible = false;
			this.EnablePlayerControl ();
		}	// OnPlayerRespawn

		public void Pause(){
			Console.WriteLine ("Game is paused!");
			Time.timeScale = 0.0f;
			Screen.lockCursor = false;
			Cursor.visible = true;

			// need to deactivate MouseLook component attached to the main camera
			// else camera continues to rotate when the game is paused 
			this.DisablePlayerControl ();
		}	// Pause

		public void UnPause(){
			Time.timeScale = 1.0f;
			Screen.lockCursor = true;
			Cursor.visible = false;
			this.IsPaused = false;

			// need to reactivate MouseLook component attached to the main camera
			// see the Pause method to get the reason
			this.EnablePlayerControl ();
		}	// UnPause

		public void SwitchPauseState(){
			this.IsPaused = !this.IsPaused;
		}	// SwitchPauseState
		

		public void SwitchInventoryOpenState(){
			this.Player.Inventory.SwithchOpenState ();
		}	// SwitchInventoryOpenState

		public void SwitchPlayerTorchlightState(){
			this.Player.SwitchTorchLightState ();
		}	// SwitchPlayerTorchlightState

		#region "Interaction with object"
		public GameObject gameObjectInteractable { get; private set; }

		public void DispathInteraction(InteractionType interaction){
			if (gameObjectInteractable == null)
				return;
			this.GameObjectInteractionManager.Dispatch(gameObjectInteractable, interaction);
		}	// DispathInteraction
		

		void OnCollisionEnter(Collision collision) {
			//Debug.Log ("GameController::OnCollisionEnter() - Collision: " + collision + " ; GameObject="+collision.gameObject.tag);
			if (collision.gameObject.tag == "SpaceCapsule") {
				gameObjectInteractable = collision.gameObject;
			}
		}	// OnCollisionEnter

		void OnCollisionExit(Collision collision) {
			//Debug.Log ("GameController::OnCollisionExit() - Collision: " + collision + " ; GameObject="+collision.gameObject.tag);
			if (collision.gameObject.tag == "SpaceCapsule") {
				collision.gameObject.GetComponent<Renderer>().material.color = Color.white;
				gameObjectInteractable = null;
			}
		}	// OnCollisionExit

		void OnCollisionStay(Collision collision) {
			//Debug.Log ("GameController::OnCollisionStay() - Collision: " + collision + " ; GameObject=" + collision.gameObject.tag);
			if (collision.gameObject.tag == "SpaceCapsule") {
				collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
				gameObjectInteractable = collision.gameObject;
			}
		}	// OnCollisionStay
		#endregion
	}
}

