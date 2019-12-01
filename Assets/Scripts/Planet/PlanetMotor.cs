using UnityEngine;
using System.Collections;

public class PlanetMotor : MonoBehaviour {

	public Transform pivot;

	public int revolutionDuration = 3600; // total duration for one revolution; 86400=24h, 3600=1h, 60=1min
	float revolutionDeltaAngle = 0.0f;
	float revolutionCurrentAngle = 0.0f;
	int revolutionsCount = 0;

	public int rotationDuration = 60; // total duration for one rotation; 86400=24h, 3600=1h, 60=1min
	float rotationDeltaAngle = 0.0f;
	float rotationCurrentAngle = 0.0f;
	int rotationsCount = 0;

	public Vector3 revolutionDirection = Vector3.up;
	public Vector3 rotationDirection = Vector3.down;

	Vector2 pointToShowDebugInfo;

	/// Initializes working variables and performs starting calculations.  
	void Start () {
	}  
	
	/*void OnGUI(){
		int x = 10, y = 10, h = 20;
		string[] texts = new string[]{
			"Elapsed Time: "+Time.realtimeSinceStartup,
			transform.name+": "+transform.position,
			"Revolution Angle (delta): "+revolutionDeltaAngle.ToString(),
			"Current revolution: "+revolutionCurrentAngle.ToString(),
			"Revolutions count: "+revolutionsCount.ToString(),
			"Rotation Angle (delta): "+rotationDeltaAngle.ToString(),
			"Current rotation: "+rotationCurrentAngle.ToString(),
			"Rotations count: "+rotationsCount.ToString(),
		};
		for (int i=0; i<texts.Length; i++)
		{
			GUI.Label(new Rect(x, y+h*i, 250, h), texts[i]);
		}
	}*/

	// Update is called once per frame
	void Update () {
		if (revolutionDuration != -1) {
			// rotate around the pivot transform
			revolutionDeltaAngle = Time.deltaTime / revolutionDuration * 360;
			revolutionCurrentAngle += revolutionDeltaAngle;
			transform.RotateAround (pivot.position, this.revolutionDirection, revolutionDeltaAngle);

			if (revolutionCurrentAngle > 360.0f) {
				revolutionsCount++;
				revolutionCurrentAngle = 0.0f;
			}
		}

		if (rotationDuration != -1) {
			// rotate around itself
			rotationDeltaAngle = Time.deltaTime / rotationDuration * 360;
			rotationCurrentAngle += rotationDeltaAngle;
			transform.Rotate (this.rotationDirection, rotationDeltaAngle); 

			if (rotationCurrentAngle > 360.0f) {
				rotationsCount++;
				rotationCurrentAngle = 0.0f;
			}
		}

	}

}
