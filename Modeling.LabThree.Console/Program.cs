using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.LabThree.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Sms sms = new Sms()
            {
                ContainerCapacity = 1,
                P1 = 0.3,
                P2 = 0.2,
                R = 0.2,
                TotalCount = 10000
            };

            StatisticResults result = sms.Emulate();
            IDictionary<String, Double> probabilities = result.StateProbabilities();
            Print(probabilities);
        }

        private static void Print(IDictionary<String, Double> probabilities)
        {
            foreach (var element in probabilities)
            {
                System.Console.WriteLine("{0} {1}",element.Key, element.Value);
            }
        }
    }
}
