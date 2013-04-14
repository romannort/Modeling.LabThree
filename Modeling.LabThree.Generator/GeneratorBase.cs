using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.Generator
{
    public class GeneratorBase
    {

        static GeneratorBase()
        {
            random = new Random();
        }

        public static UInt32 DesiredSize
        {
            get;
            set;
        }

        private static Double NextNumber()
        {
            Double result = random.NextDouble(); 
            return result;
        }

        public static IList<Double> GenerateRealization()
        {
            IList<Double> realization = new List<Double>();
            for( UInt32 i = 0; i < DesiredSize; ++i)
            {
                realization.Add(NextNumber());
            }
            return realization;
        }


        public static Random random { get; set; }
    }
}
