using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.Generator
{
    public static class RandomGenerator
    {
        private static Random random = new Random(42);

        public static Double Next()
        {
            return random.NextDouble();
        }
    }
}
