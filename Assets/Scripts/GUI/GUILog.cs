using UnityEngine;
using System.Collections;
namespace Sram
{
	public class GUILog : IGUIComponent
	{
		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		
		public Queue Texts { get; private set; }

		int MaxLinesCount;

		public static GUILog GetComponent(int maxLinesCount, int left, int top, int width, int height)
		{
			return new GUILog(){
				MaxLinesCount=maxLinesCount,
				Left=left, 
				Top=top,
				Width=width,
				Height=height,
				Texts = new Queue(),
			};
		}
		
		public void Enqueue(string text)
		{
			if (Texts.Count >= this.MaxLinesCount)
				Texts.Dequeue ();
			Texts.Enqueue (text);
		}
		
		public void ToGUI()
		{	
			if (this.Texts.Count == 0)
				return;

			GUIStyle labelStyle = GUI.skin.GetStyle ("Label");
			labelStyle.alignment = TextAnchor.UpperLeft;
			//labelStyle.fontSize = 11;

			int lineHeight = (int)labelStyle.CalcSize(new GUIContent(this.Texts.Peek().ToString ())).y;

			//layout start
			GUI.BeginGroup (new Rect (this.Left, this.Top, this.Width, this.Height));
			
			//the menu background box
			GUI.Box (new Rect (0, 0, this.Width, this.Height), "");
			
			for (int i=0; i<Texts.Count; i++)
			{
				GUI.Label(new Rect(5, lineHeight*i+2, Width, lineHeight), Texts.ToArray()[i].ToString());
			}
			
			//layout end
			GUI.EndGroup (); 
		}
	}
}


