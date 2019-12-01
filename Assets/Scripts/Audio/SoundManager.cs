using UnityEngine;
using System.Collections;
namespace Sram {
	public class SoundManager {

		GameController GameController;

		AudioSource AudioSource;

		public static SoundManager GetInstance (GameController gameController)
		{
			return new SoundManager (){
				GameController=gameController,
				AudioSource = gameController.GetComponent<AudioSource> (),
			};
		}

		public void Update () {
			if (this.GameController.Player.IsMoving) {
				if(!this.AudioSource.isPlaying){
					this.AudioSource.enabled = true;
					this.AudioSource.Play();
				}
			} else {
				this.AudioSource.Stop();
			}
		}
	}
}
