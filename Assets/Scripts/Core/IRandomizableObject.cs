using System;
namespace Sram.Generators
{
	public interface IRandomizableObject
	{
		string InternalName { get; }
		string Name { get; }
		void Update();
	}
}

