using System.Linq;
using UnityEngine;
namespace Sram.Generators
{
	public class SpaceShipNames : NameGenerator
	{
//			protected override string[] Prefixes = new string[]{
//				"AES",
//				"CCS",
//				"EAS",
//				"FTLS",
//				"HMS",
//				"HSV",
//				"UFS",
//				"USS",
//			};

		protected override string[] Names {
			get {
				return new string[]{
					"Alexander",
					"Interceptor",
					"Warrior",
				};
			}
		}

		public SpaceShipNames() : base() 
		{ }


	}
}

