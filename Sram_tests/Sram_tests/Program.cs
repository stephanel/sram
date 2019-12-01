using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sram_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceCapsuleManager mng = new SpaceCapsuleManager();

            for (int i = 0; i < 30; i++)
            {
                SpaceCapsule caps = new SpaceCapsule(mng);
                Console.WriteLine(caps.Name);
                System.Threading.Thread.Sleep(50);
            }
            Console.ReadLine();
        }
    }
}
