using System;
using System.Linq;
//using UnityEngine;
namespace Sram.Generators
{
	public abstract class NameGenerator
	{
		public abstract string[] Names { get; }

		protected NameGenerator (){ }
        Random rnd = new Random(System.DateTime.Now.Ticks.GetHashCode());

		public string RandomName(IRandomizableObjectManager<IRandomizableObject> manager){
			//Random.seed = System.DateTime.Now.Ticks.GetHashCode();
            string newname = GetNextRandomName();
			while (manager.GeneratedObjects.Where(p => p.Name == newname).Count() > 0) {
				newname = GetNextRandomName();
			}
			return newname;
		}	// RandomName

		protected string GetNextRandomName()
		{
            int nextInt = rnd.Next(Names.Length); //Random.Range (0, Names.Length - 1);
            float nextFloat = (float)rnd.NextDouble() * 10; //Random.value * 10;
			return Names [nextInt] + " " + System.Math.Round(nextFloat, 2, System.MidpointRounding.AwayFromZero).ToString ();
		}	// GetNextRandomName
	}
}

