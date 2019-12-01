using Sram.Configuration;
using UnityEngine;
namespace Sram
{
	public class Sun : IGameComponent
	{
		GameTimeConfig GameTimeConfig;

		Transform SunTransform;
		Transform SunPivotTransform;

		// use a random period for revolution
		// random between 86400 seconds (i.e. 24 hours in real time) and 3600 seconds (i.e. 1 hours in real time)
		public int revolutionDuration { get; private set; } // total duration for one revolution; 86400=24h, 3600=1h, 60=1min
		float revolutionDeltaAngle = 0.0f;

		Vector3 revolutionDirection = new Vector3 (0.3f, 0.3f, 0);//0.01f);
		Vector3 startPosition;

		private Sun(IGameController gameController){
			GameTimeConfig=gameController.Config.GameTimeConfig;
			SunTransform=gameController.AssetManager.SunTransform;
			SunPivotTransform = gameController.AssetManager.SunPivotTransform;

			Random.seed = (int)System.DateTime.Now.Ticks;
			// define a random revolution period
			// first, try to define min and max related with the game time configuration
			//this.GameTimeConfig.TimeMultiplicator
			// TODO : fix values to define revolution period
			revolutionDuration = Random.Range (250, 5000);
//				(int)(3600 / this.GameTimeConfig.TimeMultiplicator),
//				(int)(86400 / this.GameTimeConfig.TimeMultiplicator));

			Debug.Log ("revolutionDuration="+revolutionDuration);

			// rotate sun about a random angle
			float startAngle = Random.Range(0,360);
			SunTransform.RotateAround (SunPivotTransform.position, this.revolutionDirection, startAngle);

		}	// Constructor

		public static Sun GetInstance (IGameController gameController) {
			return new Sun (gameController);
		}	// GetInstance

		public void Update(){
			revolutionDeltaAngle = Time.deltaTime / (revolutionDuration / this.GameTimeConfig.TimeAccelerator) * 360;
			SunTransform.RotateAround (SunPivotTransform.position, this.revolutionDirection, revolutionDeltaAngle);

			// rotate with (a little of...) realism, up on a side of the terrain, rotate around and go down on the opposite side; 
//			SunTransform.Rotate (Vector3.right, revolutionDeltaAngle);
//			SunTransform.Rotate (Vector3.up, revolutionDeltaAngle);
		}
	}
}

