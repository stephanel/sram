using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sram.Generators;
namespace Sram_tests
{
    class SpaceCapsuleManager : IRandomizableObjectManager<IRandomizableObject> 
    {
        public List<IRandomizableObject> GeneratedObjects { get; private set; }

        public SpaceCapsuleManager()
        {
            GeneratedObjects = new List<IRandomizableObject>();
        }
    }
}
