using System.Collections;
using UnityEngine;
namespace Sram
{
	public class DebugInfo : IGUIComponent
	{
		public int Left { get; set; }
		public int Top { get; set; }
		public int Width { get; set; }

		public static DebugInfo GetDebugInfo(int left, int top)
		{
			return new DebugInfo () { 
				Left=left, 
				Top=top,
				Width=250,
				Texts = new Queue(),
			};
		}

		public Queue Texts { get; private set; }

		public void Enqueue(string text)
		{
			Texts.Enqueue (text);
		}

		public void ToGUI()
		{
			int h = 20;
			for (int i=0; i<Texts.Count; i++)
			{
				GUI.Label(new Rect(Left, Top+h*i, Width, h), Texts.Dequeue().ToString());
			}
		}

	}
}


