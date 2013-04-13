using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree
{
    public class SmsServiceElement : SmsElementBase
    {
        public Double Probability { get; set; }

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
            //Double currentProbability = CalculateGeometricDistribution();//index);
            Double r = new Random().NextDouble();

            if (r > Probability)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Calculates geometric distribution for real probability.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>4
        private Double CalculateGeometricDistribution(UInt32 index)
        {
            Double probability = 1 - Probability;
            Double result = Math.Pow(1 - probability, index - 1) * probability;
            return result;
        }

        private static Double CalculateGeometricDistribution()
        {
            Random r = new Random();
            Double probability = r.NextDouble();
            return probability;
        }
    }
}
