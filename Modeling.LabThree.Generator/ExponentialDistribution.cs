using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.Generator
{
    public static class ExponentialDistribution
    {
        private static ICollection<Double> SourceRealization;
        
        public static ICollection<Int32> Generate(Double lambda)
        {
            SourceRealization = GeneratorBase.GenerateRealization();
            Double divLambda = -1 / lambda;
            Func<Double, Int32> roundedValue = x => (Int32)(Math.Round(Math.Log(x) * divLambda)) + 1;
            ICollection<Int32> result = SourceRealization.Select(roundedValue).ToList();
            return result;
        }
    }
}
