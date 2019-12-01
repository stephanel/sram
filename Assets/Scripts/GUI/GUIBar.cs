using UnityEngine;
using System;
namespace Sram
{
	public class GUIBar
	{
		string Label;
		Vector2 Position;
		Vector2 Size;
		Texture2D ForegroundTexture;
		
		int MarginLeft = 3;
		int MarginTop = 2;
		
		public static GUIBar GetGUIBar(string label, Vector2 position, Vector2 size, Texture2D foregroundTexture){
			return new GUIBar (){
				Label=label,
				Position=position,
				Size=size,
				ForegroundTexture=foregroundTexture,
			};
		}
		
		
		public void ToGUI(float valuePercent, string remainingTime, string remainingTimeBase1000)
		{
			GUI.BeginGroup (new Rect (Position.x, Position.y, Size.x, Size.y));
			GUI.Box (new Rect (0, 0, Size.x, Size.y), "");
			
			// draw the filled-in part:
			float width = Size.x * valuePercent / 100;
			GUI.BeginGroup (new Rect (0, 0, Size.x, Size.y));
			
			//			GUI.Box (new Rect (MarginLeft, MarginTop, width - MarginLeft*2, Size.y - MarginTop*2), "");
			GUI.DrawTexture(
				new Rect(
				MarginLeft,
				MarginTop,
				width - MarginLeft*2, 
				Size.y - MarginTop*2), 
				ForegroundTexture,
				ScaleMode.ScaleAndCrop,
				true,
				0 );
			
			GUIStyle labelStyle = GUI.skin.GetStyle ("Label");
			labelStyle.alignment = TextAnchor.MiddleCenter;
			string content = Label + " : " + (int)valuePercent + " % - " + remainingTime + " - " + remainingTimeBase1000;
			GUI.Label (new Rect(0, 0, Size.x, Size.y),  content);
			
			GUI.EndGroup ();
			
			GUI.EndGroup ();
		}
	}
}

