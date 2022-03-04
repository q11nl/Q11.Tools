using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q11.Tools.Conversion;

namespace TestConsole
{
    internal class Tester
    {
        public void Start()
        {
            long value = 6;

            var actual = value.ChangeType<int>();
        }
    }
}
