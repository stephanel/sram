using UnityEngine;
using System.Collections;

public class GUIHeadUpButtons : MonoBehaviour {

	public Transform Base1;
	public Transform Base2;
	public Transform topOfHill;

	void OnGUI( )
	{
		if (GUI.Button(new Rect(10, 70, 150, 20),"Go to Base1"))
		{
			//Debug.Log("Clicked the button with text");
			transform.position = Base1.position;
			// go to base 1
			transform.Translate( new Vector3(0,1,0) );
			// go a little above the spike
			transform.rotation = Base1.rotation; 
		}

		if (GUI.Button(new Rect(10, 100, 150, 20),"Go to Base2"))
		{ 
			// Debug.Log("Clicked the button with text");
			transform.position = Base2.position;
			// go to base 1
			transform.Translate( new Vector3(0,1,0) );
			// go a little above the spike
			transform.rotation = Base2.rotation; 
		}
		if (GUI.Button(new Rect(10, 130, 150, 20),"Go on top of hill"))
		{ 
			// Debug.Log("Clicked the button with text");
			transform.position = topOfHill.position;
			// go to base 1
			transform.Translate( new Vector3(0,1,0) );
			// go a little above the spike
			transform.rotation = topOfHill.rotation; 
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
