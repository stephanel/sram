using System.Collections.Generic;
namespace Sram.Generators
{
	public interface IRandomizableObjectManager<T>  where T : IRandomizableObject
	{
		List<T> GeneratedObjects { get; }
	}
}

