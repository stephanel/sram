using System.Collections;
namespace Sram
{
	public interface IGUIComponent
	{
		int Left { get; set; }
		int Top { get; set; }
		int Width { get; set; }

		Queue Texts { get; }
		void Enqueue(string text);
		void ToGUI();
	}
}

