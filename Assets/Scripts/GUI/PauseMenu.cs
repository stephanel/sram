using UnityEngine;
using System.Collections;
namespace Sram
{
	public class PauseMenu {

		public static void Show(GameController gameController) {
			Cursor.visible = true;

			//layout start
			GUI.BeginGroup(new Rect(Screen.width / 2 - 150, 50, 300, 250));
			
			//the menu background box
			GUI.Box(new Rect(0, 0, 300, 250), "");
			
			//logo picture
			//GUI.Label(Rect(15, 10, 300, 68), logoTexture);
			
			///////pause menu buttons
			//game resume button
			if(GUI.Button(new Rect(55, 100, 180, 40), "Resume")) {
				//resume the game
				Cursor.visible = false;
				Time.timeScale = 1.0f;
				gameController.UnPause (); //.GameIsPaused=false;
			}
			
			//			//main menu return button (level 0)
			//			if(GUI.Button(new Rect(55, 150, 180, 40), "Main Menu")) {
			//				Application.LoadLevel(0);
			//			}

			//game resume button
			if(GUI.Button(new Rect(55, 150, 180, 40), "Return to main menu")) {
				//resume the game
				Application.LoadLevel ("Main"); //.GameIsPaused=false;
			}

			//quit button
			if(GUI.Button(new Rect(55, 200, 180, 40), "Quit")) {
				Application.Quit();
			}
			
			//layout end
			GUI.EndGroup(); 
		}

	}
}