using grappr.Tests.Square8;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grappr.Tests
{
    [TestFixture]
    public class StateExpansionTests
    {
        [Test]
        public void Test_Square8_Expansion()
        {
            for (int i = 0; i < 9; i++)
            {
                var square = new int[9];
                square[i] = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (j >= i)
                        square[j + 1] = j + 1;
                    else
                        square[j] = j + 1;
                }
                IState init = new Square(square);
                Console.WriteLine(init.ToString());
                foreach (var s in init.Successors)
                {
                    Console.WriteLine("---------\n{0} ({1}{2})", s.Action, s.Cost, s.State.IsGoal ? ", Goal" : "");
                    Console.WriteLine(s.State);
                }

                Console.WriteLine("------------------------------------------");
            }
        }
    }
}
