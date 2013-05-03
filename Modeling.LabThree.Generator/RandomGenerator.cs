using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.Generator
{
    /// <summary>
    /// Generates random numbers
    /// </summary>
    public static class RandomGenerator
    {
        private static Random random = new Random(42);

        /// <summary>
        /// Generate random number between 0.0 and 1.0
        /// </summary>
        /// <returns></returns>
        public static Double Next()
        {
            return random.NextDouble();
        }
    }
}
