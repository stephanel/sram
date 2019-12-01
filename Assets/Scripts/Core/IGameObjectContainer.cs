using Sram.Generators;
using System.Collections.Generic;
namespace Sram
{
	public interface IGameObjectContainer<T> where T : IRandomizableObject //: IGameObjectContainer
	{
		List<T> Items { get; }
	}

}

