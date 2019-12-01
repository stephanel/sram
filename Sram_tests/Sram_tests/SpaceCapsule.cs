using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sram.Generators;

namespace Sram_tests
{
    class SpaceCapsule : IRandomizableObject
    {
        public string Name { get; private set; }

        public SpaceCapsule(SpaceCapsuleManager manager)
            : base()
        {
            this.Name = new SpaceCapsuleName().RandomName((IRandomizableObjectManager<IRandomizableObject>)manager);
        }
    }
}
