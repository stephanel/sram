using System.Linq;
namespace Sram.Generators
{
	public class SpaceCapsuleNames : NameGenerator
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

		protected override string[] Names {
			get {
				return new string[]{
					"Columbus",
					"Cruiser",
					"Discovery",
					"Enterprise",
					"Explorer",
					"Horizon",
					"Magellan",
					"Vespucci",
					"Voyager",
				};
			}
		}

		public SpaceCapsuleNames () : base() 
		{ }

	}
}

