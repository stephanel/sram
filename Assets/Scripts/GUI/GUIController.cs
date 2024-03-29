using UnityEngine;
using System;
namespace Sram
{
	public class GUIController : MonoBehaviour
	{
		public GameObject GameControllerParent;
		public GUISkin CustomSkin;
		
		GameController GameController;

		GUILog GUILog;
		GUIComponent GUIKeyConfig;
		GUIComponent GUIDebugInfo;
		GUIComponent GameInfo;
		GUIComponent ActionInfo;
		GUIBar HungerBar;
		GUIBar ThirstBar;
		GUIBar OxygenBar;
		GUIBar EnergyBar;
		public Texture2D HungerBarTexture;
		public Texture2D ThirstBarTexture;
		public Texture2D OxygenBarTexture;
		public Texture2D EnergyBarTexture;
		
		//		public static GUIController GetGUIController (GameController gameController)
		//		{
		//			return new GUIController (){
		//				GameController=gameController,
		//				GUIDebugInfo = GUIComponent.GetComponent (10, 10),
		//				GUIPlayerInfo = GUIComponent.GetComponent (10, 80),
		//			};
		//		}
		
		void Start(){
			GameController = GameControllerParent.GetComponent<GameController> ();

			GUILog = GUILog.GetComponent (this.GameController.Config.MaxLogLinesCount, 10, Screen.height-190, 380, 190);
			GUIKeyConfig = GUIComponent.GetComponent (10, 150, 250);
			GUIDebugInfo = GUIComponent.GetComponent (10, 10, 250);
			GameInfo = GUIComponent.GetComponent (Screen.width - 250, 125, 250);
			ActionInfo = GUIComponent.GetComponent (Screen.width / 2 - 250 / 2, Screen.height / 2 - 50 / 2, 250);
			HungerBar = GUIBar.GetGUIBar ("Hunger", new Vector2(Screen.width-255,10), new Vector2(250,24), HungerBarTexture);
			ThirstBar = GUIBar.GetGUIBar ("Thirst", new Vector2(Screen.width-255,36), new Vector2(250,24), ThirstBarTexture);
			OxygenBar = GUIBar.GetGUIBar ("Oxygen", new Vector2(Screen.width-255,62), new Vector2(250,24), OxygenBarTexture);
			EnergyBar = GUIBar.GetGUIBar ("Energy", new Vector2(Screen.width-255,88), new Vector2(250,24), EnergyBarTexture);
		}
		
		void OnGUI() {
			
			// defin skin
			GUI.skin = this.CustomSkin;
			
			GUIStyle labelStyle = GUI.skin.GetStyle ("Label");
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontSize = 14;

			if (this.GameController.IsPaused) {
				// render pause menu 
				
				//layout start
				GUI.BeginGroup (new Rect (Screen.width / 2 - 150, 250, 300, 250));
				
				//the menu background box
				GUI.Box (new Rect (0, 0, 300, 250), "");
				
				//logo picture
				//GUI.Label(Rect(15, 10, 300, 68), logoTexture);
				
				///////pause menu buttons
				//game resume button
				if (GUI.Button (new Rect (55, 100, 180, 40), "Resume")) {
					//resume the game
					this.GameController.UnPause();
				}
				
				//main menu return button (level 0)
				if(GUI.Button(new Rect(55, 150, 180, 40), "Main Menu")) {
					Application.LoadLevel("Main");
				}
				
				//quit button
				if (GUI.Button (new Rect (55, 200, 180, 40), "Quit")) {
					Application.Quit ();
				}
				
				//layout end
				GUI.EndGroup (); 
				
			} else {

				// render in-game GUI
				this.GUIKeyConfig.Enqueue("Launch new space caps: "+this.GameController.Config.KeyConfig.SpaceCapsuleLauncher);
				this.GUIKeyConfig.Enqueue("Torchligth On/off: "+this.GameController.Config.KeyConfig.TorchligthOnOff);
				this.GUIKeyConfig.Enqueue("Time Multiplicator: x "+this.GameController.Config.GameTimeConfig.TimeMultiplicator);
				this.GUIKeyConfig.Enqueue("Time Accelerator: x "+this.GameController.Config.GameTimeConfig.TimeAccelerator);
				this.GUIKeyConfig.Enqueue("Show/hide FPS: "+this.GameController.Config.KeyConfig.ShowHideFps);
				this.GUIKeyConfig.ToGUI();

				if (this.GameController.Config.ShowHideFps) {
					this.GUIDebugInfo.Enqueue ("FPS : " + this.GameController.FpsCalculator.Fps.ToString ("f2"));
				}
				this.GUIDebugInfo.Enqueue ("Player position : " + this.GameController.PlayerPosition.ToString ());
				this.GUIDebugInfo.ToGUI ();

				// log
				GUILog.ToGUI();

				// player info
				if(this.GameController.Player.IsAlive){
					
					labelStyle.alignment = TextAnchor.MiddleLeft;
					labelStyle.normal.textColor = Color.white;

					this.GameInfo.Enqueue (this.GameController.GameTime.GetWorldTime());
					//this.GameInfo.Enqueue (this.GameController.GameTime.GetRealWorldTime());
					this.GameInfo.Enqueue (this.GameController.GameTime.GetWorldTimeBase1000()+ " uT");
					this.GameInfo.Enqueue("Ambient temperature: "+this.GameController.Planet.TemperatureLevel+"°C");
					this.GameInfo.ToGUI();
					
					this.HungerBar.ToGUI(
						this.GameController.Player.HungerStat.GetPercent (), 
						this.GameController.Player.HungerStat.RemainingTime,
						this.GameController.Player.HungerStat.RemainingTimeBase1000);
					this.ThirstBar.ToGUI(
						this.GameController.Player.ThirstStat.GetPercent (),
						this.GameController.Player.ThirstStat.RemainingTime,
						this.GameController.Player.ThirstStat.RemainingTimeBase1000);
					this.OxygenBar.ToGUI(
						this.GameController.Player.OxygenStat.GetPercent (),
						this.GameController.Player.OxygenStat.RemainingTime,
						this.GameController.Player.OxygenStat.RemainingTimeBase1000);
					this.EnergyBar.ToGUI(
						this.GameController.Player.SpaceSuit.EnergyStat.GetPercent (), 
						this.GameController.Player.SpaceSuit.EnergyStat.RemainingTime, 
						this.GameController.Player.SpaceSuit.EnergyStat.RemainingTimeBase1000);
					
					// Inventory
					this.GameController.Player.Inventory.ToGUI();

					if(this.GameController.gameObjectInteractable!=null){
						this.ActionInfo.Enqueue("Press F");
						this.ActionInfo.ToGUI();
					}

				} 
				else {
					// player is dead, render page for respawn
					
					//layout start
					GUI.BeginGroup (new Rect (Screen.width / 2 - 150, 250, 300, 250));
					
					//the menu background box
					GUI.Box (new Rect (0, 0, 300, 250), "");
					
					labelStyle.alignment = TextAnchor.MiddleCenter;
					labelStyle.normal.textColor = Color.red;
					GUI.Label (new Rect(10, 10, 300, 68),  "You are dead !");
					
					if(!this.GameController.Player.IsAlive){
						if (GUI.Button (new Rect (55, 80, 180, 40), "Respawn")) {
							//resume the game
							this.GameController.OnPlayerRespawn();
						}
					}
					
					//layout end
					GUI.EndGroup (); 
					
				}
			}
		}

		public void Log(string text){
			this.GUILog.Enqueue (text);
		}
	}
}

