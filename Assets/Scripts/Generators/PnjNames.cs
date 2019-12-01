namespace Sram.Generators
{
	public class PnjNames : NameGenerator
	{
		protected override string[] Names {
			get {
				return new string[]{
					"Dave",
					"Frank",
				};
			}
		}

		public PnjNames () : base()
		{
		}
	}
}

