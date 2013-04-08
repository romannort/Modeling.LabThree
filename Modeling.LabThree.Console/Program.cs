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
                P1 = 0.7M,
                P2 = 0.7M,
                R = 0.5M,
                TotalCount = 1000
            };

            StatisticResults result = sms.Emulate();
            IDictionary<String, Decimal> probabilities = result.StateProbabilities();
            
        }

        private void Print(IDictionary<String, Decimal> probabilities)
        {
            foreach (var element in probabilities)
            {
                System.Console.WriteLine("{0} {1}",element.Key, element.Value);
            }
        }
    }
}
