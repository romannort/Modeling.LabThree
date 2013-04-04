using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmoElementWithProbability
    {
        public Decimal Probability { get; set; }

        public UInt16 State { get; set; }

        public Boolean this[ Int32 index]
        {
            get
            {
                if (lastIndex != index)
                {
                    done = IsDone(index);
                }
                return done;
            }
        }

        private Int32 lastIndex = -1;

        private Boolean done = false;

        /// <summary>
        /// Calcualte Done value.
        /// </summary>
        /// <param name="index"></param>
        private Boolean IsDone(Int32 index)
        {
            this.lastIndex = index;
            Boolean result = false;
            Decimal currentProbability = CalculateGeometricDistribution(index);
            if (currentProbability >= this.Probability)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Calculates geometric distribution for real probability.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private static Decimal CalculateGeometricDistribution(Int32 index)
        {
            Random r = new Random(index);
            Double probability = r.NextDouble();
            Double result = Math.Pow(1 - probability, index - 1) * probability;
            return (Decimal)result;
        }
    }
}
