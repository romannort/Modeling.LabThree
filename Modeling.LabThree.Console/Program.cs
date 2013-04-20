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
                P1 = 0.7,
                P2 = 0.7,
                R = 0.75,
                TotalCount = 500000
            };

            StatisticResults result = sms.Emulate();
            Print(result);
            result.GenerateTransitionsTable("output.table");
        }

        private static void Print(StatisticResults result)
        {
            IDictionary<String, Double> probabilities = result.StateProbabilities();
            foreach (var element in probabilities)
            {
                System.Console.WriteLine("{0} {1}",element.Key, element.Value);
            }
            System.Console.WriteLine("Emitter blocking probability: {0}", result.EmitterBlockingProbability );
            System.Console.WriteLine("Average container content: {0}", result.AverageContainerContent);
        }
    }
}
