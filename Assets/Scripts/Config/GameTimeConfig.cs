//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18449
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace Sram.Configuration
{
	public class GameTimeConfig
	{
		public float TimeAccelerator { get; set; }	// use to accelerate/decelerate the time
		public float TimeMultiplicator{ get; private set; }	// speed of time

		private GameTimeConfig(){
			this.TimeAccelerator = 1f;
			this.TimeMultiplicator = 3f; // x times more speed than the real time
		}
		public static GameTimeConfig GetConfig ()
		{
			return new GameTimeConfig ();
		}
	}
}

