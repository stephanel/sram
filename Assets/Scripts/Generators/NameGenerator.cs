using System;
using System.Linq;
//using UnityEngine;
namespace Sram.Generators
{
	public abstract class NameGenerator
	{
		protected virtual string[] Prefixs { get { return new string[]{}; } }
		protected abstract string[] Names { get; }
		protected virtual string[] Sufixs { get { return new string[]{}; } }

		protected bool HasPrefix { get; set; }
		protected bool HasSuffix { get; set; }

		Random rnd = new Random(System.DateTime.Now.Ticks.GetHashCode());

		protected NameGenerator (){
			this.HasPrefix = false;
			this.HasSuffix = false;
		}

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
			int nextInt = rnd.Next(this.Names.Length); //Random.Range (0, Names.Length - 1);
			float nextFloat = (float)rnd.NextDouble() * 10; //Random.value * 10;
			string name = this.Names [nextInt] + " " + System.Math.Round(nextFloat, 2, System.MidpointRounding.AwayFromZero).ToString ();

			if (this.HasPrefix && this.Prefixs != null && this.Prefixs.Length > 0)
				name = this.Prefixs[rnd.Next(this.Prefixs.Length)] + " " + name;

			if (this.HasSuffix && this.Sufixs != null && this.Sufixs.Length > 0)
				name = name + "-" + this.Sufixs[rnd.Next(this.Sufixs.Length)];

			return name;
		}	// GetNextRandomName
	}
}

