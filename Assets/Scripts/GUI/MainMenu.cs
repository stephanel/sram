
using Sram.Configuration;
using System.Collections;
using UnityEngine;
namespace Sram{
	public class MainMenu : MonoBehaviour, IGameController {

		public Config Config { get; private set; }
		public AssetManager AssetManager { get; private set; } 
		Sun Sun;

		GUISkin CustomSkin;

		int width = 320;

		bool gameIsLoading;

		AsyncOperation asyncOperation;


		void Awake(){
			this.Config = Config.GetConfig ();
			this.AssetManager = GetComponent <AssetManager> ();

//			string filepath = System.IO.Path.Combine (Application.dataPath, "gamedata.dat");
//			if (System.IO.File.Exists (filepath))
//				System.IO.File.Delete (filepath);
			//			GameDataRepository.CreateGameData (gameTimeConfig, filepath);
		}	// Awake

		void Start(){
			gameIsLoading = false;

			this.CustomSkin = Resources.Load ("CustomerGUISkin") as GUISkin;

			this.Sun = Sun.GetInstance (this);
			TerrainManager.GetInstance(this, this.Config.TerrainGeneratorConfig);

//			Debug.Log ("Application.dataPath: " + Application.dataPath);
//			Debug.Log ("Application.streamingAssetsPath: " + Application.streamingAssetsPath);
//			Debug.Log ("Application.persistentDataPath: " + Application.persistentDataPath);
//			Debug.Log ("Application.temporaryCachePath: " + Application.temporaryCachePath);
//			Debug.Log ("Level " + Application.loadedLevel + " loaded");
		}	// Start

//		void ReportLoadProgress(float progress){
//			GUI.Label(new Rect(10,10,300, 50), new GUIContent("Loading... "+progress+"%"));
//		}	// ReportLoadProgress


		void Update(){
			this.Sun.Update ();
		}
		void OnGUI(){
			GUI.skin = this.CustomSkin;

			GUIStyle labelStyle = GUI.skin.GetStyle ("Label");
			GUIStyle buttonStyle = GUI.skin.GetStyle ("Button");

			labelStyle.alignment = TextAnchor.MiddleCenter;
			labelStyle.fontSize = 14;
			labelStyle.fontStyle = FontStyle.Bold;
			
			buttonStyle.alignment = TextAnchor.MiddleCenter;
			buttonStyle.fontSize = 14;
			buttonStyle.fontStyle = FontStyle.Bold;

			if (this.gameIsLoading) {
//				float progress = this.asyncOperation.progress;
//				ReportLoadProgress(Mathf.Round(progress*100));
//				if(progress>=1){
//					// destruct asyncop object
//					ClearObject();
//					this.gameIsLoading = false;
//				}
				GUI.Label(new Rect(10,10,300, 50), new GUIContent("Loading... "));
				return;
			}

			int labelLeftRight = 10;
			int labelBottomTopMargin = 20;
			int buttonLeftRightMargin = 100;
			int buttonBottomTopMargin = 20;
			float labelWidth = width - labelLeftRight * 2;
			float buttonWidth = width - buttonLeftRightMargin * 2;

			// title
			float titleHeight = labelStyle.CalcHeight (new GUIContent ("S.R.A.M."), labelWidth);

			// "New Game" button
			GUIContent newgameButtonContent = new GUIContent ("New Game");
			float newgameButtonHeight = buttonStyle.CalcHeight (newgameButtonContent, buttonWidth);
			float newgameButtonTop = titleHeight + labelBottomTopMargin * 2;

			// "Load Game" button
			GUIContent loadgameButtonContent = new GUIContent ("Load Game");
			float loadgameButtonHeight = buttonStyle.CalcHeight (loadgameButtonContent, buttonWidth);
			float loadgameButtonTop = newgameButtonTop + newgameButtonHeight + 5;

			// "Quit" button
			GUIContent quitButtonContent = new GUIContent ("Quit");
			float quitButtonHeight = buttonStyle.CalcHeight (quitButtonContent, buttonWidth);
			float quitButtonTop = loadgameButtonTop + loadgameButtonHeight + 5;

			int height = (int)(quitButtonTop + quitButtonHeight + buttonBottomTopMargin);

			//layout start
			GUI.BeginGroup (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height));

			GUI.Box (new Rect (0, 0, width, height), "");

			// title
			GUI.Label (new Rect (labelLeftRight, labelBottomTopMargin, (int)labelWidth, titleHeight), new GUIContent ("S.R.A.M."));

			// "New Game" button
			if(GUI.Button(new Rect(buttonLeftRightMargin, newgameButtonTop, (int)buttonWidth,  newgameButtonHeight), newgameButtonContent)){
				gameIsLoading=true;
				Application.LoadLevel("Sram");
				//asyncOperation = Application.LoadLevelAdditiveAsync("Sram");
			}

			// "Load Game" button
			if(GUI.Button(new Rect(buttonLeftRightMargin, loadgameButtonTop, (int)buttonWidth,  loadgameButtonHeight), loadgameButtonContent)){
				
			}

			// "Quit" button
			if(GUI.Button(new Rect(buttonLeftRightMargin, quitButtonTop, (int)buttonWidth, quitButtonHeight), quitButtonContent)){
				Application.Quit();
			}

			// layout end
			GUI.EndGroup ();

		}	// OnGUI
	}
}