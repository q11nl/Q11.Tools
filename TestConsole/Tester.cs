using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using Q11.Tools.Conversion;

namespace TestConsole
{
    internal class Tester
    {
        public void Start()
        {
            var ok = Enumerable.Range(1, 10).ChangeType<List<long>>();
            var nyok = Enumerable.Range(1, 10).ChangeType<long[]>();
        }

    }
}
