using Sram.Configuration;
using System;
using UnityEngine;
namespace Sram
{
	public class GameTime : IGameComponent
	{
		GameTimeConfig Config;
		int ss = 0;
		int mm = 0;
		int hh = 0;
		int dd = 1;
		string seconds, minutes, hours;
		float currentTime = 0; 

//		int ssr = 0;
//		int mmr = 0;
//		int hhr = 0;
//		int ddr = 1;
//		string secondsr, minutesr, hoursr;
//		float currentRealTime = 0; 

		int dd1000 = 1;
		float currentTimeBase1000 = 0;	// 1000 uT = 24 heures

		private GameTime(GameTimeConfig config){
			this.Config = config;
			this.currentTime = 0.0f;
			//this.currentRealTime = 0.0f;
			this.currentTimeBase1000 = 0.0f;
		}	// constructor

		public static GameTime GetInstance (GameTimeConfig config) {
			return new GameTime (config);
		}

		public string GetWorldTimeBase1000()
		{
			return "Day " + dd1000 + " - " + Mathf.Floor(currentTimeBase1000);
		}
//		public string GetRealWorldTime()
//		{
//			return "Real - Day " + ddr + " - " + hoursr + ":" + minutesr + ":" + secondsr;
//		}
		public string GetWorldTime()
		{
			return "Day " + dd + " - " + hours + ":" + minutes + ":" + seconds;
		}

		// Update is called once per frame
		public void Update () {
			// Update the current cycle time:  
			float timeMultiplicator = this.Config.TimeMultiplicator;
			float timeAccelerator = this.Config.TimeAccelerator;

			//Debug.Log ("Time multiplicator="+(timeMultiplicator/timeAccelerator));

			float dt = Time.deltaTime;

			currentTime += dt * (timeMultiplicator * timeAccelerator); 
			//currentRealTime += dt * timeAccelerator;
			//currentTimeBase1000 += dt * (timeMultiplicator * timeAccelerator) / 86400f * 1000f; // 86400 = 24 hours in seconds = 24*60*60 

			if (currentTime >= 1) {
				ss += 1;
				currentTime = currentTime % 1;  
				currentTimeBase1000 += GameTimeConverter.ToBase1000 (1); //1 / 86400f * 1000f;
			}

//			if(currentRealTime > 1){
//				ssr += 1;
//				currentRealTime = currentRealTime % 1;
//			}

			if (currentTimeBase1000 > 1000) {
				dd1000+=1;
				currentTimeBase1000 = currentTimeBase1000 % 1000;
			}


			// game-time
			if (ss >= 60) {
				mm+=1;
				ss=0;
			}
			if (mm >= 60) {
				hh+=1;
				mm=0;
			}
			if (hh >= 24) {
				dd+=1;
				hh=0;
			}

			if (ss < 10)
				seconds = "0" + ss;
			else
				seconds = ss.ToString ();
			if (mm < 10)
				minutes = "0" + mm;
			else
				minutes = mm.ToString ();
			if (hh < 10)
				hours = "0" + hh;
			else
				hours = hh.ToString ();


//			// real time
//			if (ssr >= 60) {
//				mmr+=1;
//				ssr=0;
//			}
//			if (mmr >= 60) {
//				hhr+=1;
//				mmr=0;
//			}
//			if (hhr >= 24) {
//				ddr+=1;
//				hhr=0;
//			}
//
//			if (ssr < 10)
//				secondsr = "0" + ssr;
//			else
//				secondsr = ssr.ToString ();
//			if (mmr < 10)
//				minutesr = "0" + mmr;
//			else
//				minutesr = mmr.ToString ();
//			if (hhr < 10)
//				hoursr = "0" + hhr;
//			else
//				hoursr = hhr.ToString ();


		}
	}
}
