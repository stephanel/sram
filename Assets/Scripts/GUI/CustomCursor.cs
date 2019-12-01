using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	public Texture2D cursor;
	public Vector2 Size = new Vector2(32,32);

	void OnGUI()
	{
		// timeScale = 0 means the game is paused
		if (Time.timeScale == 0)
			return;

		GUI.DrawTexture (
			new Rect (
			Input.mousePosition.x-Size.x / 2, 
			Input.mousePosition.y-Size.y / 2,
			Size.x, 
			Size.y), cursor);
	}

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
