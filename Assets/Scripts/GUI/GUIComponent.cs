using UnityEngine;
using System.Collections;
namespace Sram
{
	public class GUIComponent : IGUIComponent
	{
		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }

		public Queue Texts { get; private set; }

		public static GUIComponent GetComponent(int left, int top, int width)
		{
			return new GUIComponent(){
				Left=left, 
				Top=top,
				Width=width,
				Texts = new Queue(),
			};
		}

		public void Enqueue(string text)
		{
			Texts.Enqueue (text);
		}

		public void ToGUI()
		{	
			if (this.Texts.Count == 0)
				return;

			int lineHeight = (int)GUI.skin.GetStyle ("Label").CalcSize(new GUIContent(this.Texts.Peek().ToString ())).y;

			int totalHeight = this.Texts.Count * lineHeight;

			//layout start
			GUI.BeginGroup (new Rect (this.Left, this.Top, this.Width, totalHeight));
			
			//the menu background box
			GUI.Box (new Rect (0, 0, this.Width, totalHeight), "");

			for (int i=0; i<Texts.Count; i++)
			{
				GUI.Label(new Rect(5, lineHeight*i+2, Width, lineHeight), Texts.Dequeue().ToString());
			}

			//layout end
			GUI.EndGroup (); 
		}
	}
}

