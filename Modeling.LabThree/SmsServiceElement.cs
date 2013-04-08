using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsServiceElement : SmsElementBase
    {
        public Decimal Probability { get; set; }

        public Boolean this[UInt32 index]
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

        private UInt32 lastIndex = 0;

        private Boolean done = false;

        /// <summary>
        /// Calcualte Done value.
        /// </summary>
        /// <param name="index"></param>
        private Boolean IsDone(UInt32 index)
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
        private static Decimal CalculateGeometricDistribution(UInt32 index)
        {
            Random r = new Random((Int32)index);
            Double probability = r.NextDouble();
            Double result = Math.Pow(1 - probability, index - 1) * probability;
            return (Decimal)result;
        }
    }
}
