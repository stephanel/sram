namespace Sram.Generators
{
	public class BotNames : NameGenerator
	{
		protected override string[] Sufixs {
			get {
				return new string[]{
					"021",
				};
			}
		}
		protected override string[] Names {
			get {
				return new string[]{
					"XX",
				};
			}
		}

		public BotNames () : base()
		{
			this.HasSuffix = true;
		}
	}
}

