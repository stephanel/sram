using UnityEngine;
using System.Collections;
namespace Sram
{
	public class MenuCursor : MonoBehaviour {
		
		public Texture2D cursorTexture;
		public Vector2 size = new Vector2(32, 32);

		void OnGUI()
		{
			GUI.DrawTexture (
				new Rect (
				Event.current.mousePosition.x-size.x / 2, 
				Event.current.mousePosition.y-size.y / 2,
				size.x, 
				size.y), cursorTexture);
		}
		
		// Use this for initialization
		void Start () {
			//cursorTexture.visible = false;
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}