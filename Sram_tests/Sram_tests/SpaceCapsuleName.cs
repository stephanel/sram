using System.Linq;
//using UnityEngine;
namespace Sram.Generators
{
	public class SpaceCapsuleName : NameGenerator
	{
//		Good for spaceship not for space capsule
//		public static string[] Prefixes = new string[]{
//			"AES",
//			"CCS",
//			"EAS",
//			"FTLS",
//			"HMS",
//			"HSV",
//			"UFS",
//			"USS",
//		};

		public override string[] Names {
			get {
				return new string[]{
					"Cruiser",
					"Enterprise",
					"Explorer",
					"Horizon",
					"Intercep",
					"Voyager",
					"Warrior",
				};
			}
		}

		public SpaceCapsuleName () : base() 
		{ }

//		public static string RandomName(SpaceCapsuleManager manager){
//			Random.seed = System.DateTime.Now.Ticks.GetHashCode();
//			string newname = GetNextRandomName();
//			while (manager.GeneratedObjects.Where(p => p.Name == newname).Count() > 0) {
//				newname = GetNextRandomName();
//			}
//			return newname;
//		}	// RandomName
//
//		private static string GetNextRandomName()
//		{
//			int nextInt = Random.Range (0, Names.Length - 1);
//			float nextFloat = Random.value * 10;
//			return Names [nextInt] + " " + System.Math.Round(nextFloat, 2, System.MidpointRounding.AwayFromZero).ToString ();
//		}	// GetNextRandomName
	}
}

