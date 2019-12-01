using UnityEngine;
using System;
namespace Sram
{
	public class FpsCalculator : IGameComponent
	{
		public float UpdateInterval = 0.5F;
		private double LastInterval;
		private int Frames = 0;
		public float Fps { get; private set; }

		public static FpsCalculator GetInstance()
		{
			return new FpsCalculator (){
				LastInterval = Time.realtimeSinceStartup,
				Frames = 0,
			};
		}

		public void Update()
		{
			++Frames;
			float timeNow = Time.realtimeSinceStartup;
			if (timeNow > LastInterval + UpdateInterval) 
			{
				Fps = Frames / (timeNow - (float)LastInterval);
				Frames = 0;
				LastInterval = timeNow;
			}
		}

	}
}

