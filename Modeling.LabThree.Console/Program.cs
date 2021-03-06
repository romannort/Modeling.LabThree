﻿using System;
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
                TotalCount = 50000,
                P1 = 0.7,
                P2 = 0.7,
                R = 0.75
            };
            //CustomParameters(sms);
            StatisticResults result = sms.Emulate();
            Print(result);
            result.GenerateTransitionsTable("output.table");
        }

        private static void Print(StatisticResults result)
        {
            IDictionary<String, Double> probabilities = result.StateProbabilities();
            System.Console.Clear();
            foreach (var element in probabilities)
            {
                System.Console.WriteLine("{0} {1}\n",element.Key, element.Value);
            }
            System.Console.WriteLine("Emitter blocking probability: {0}", result.EmitterBlockingProbability );
            System.Console.WriteLine("Average container content: {0}", result.AverageContainerContent);
        }

        /// <summary>
        /// Allow entering custom parameters from Console.
        /// </summary>
        /// <param name="sms">Sms to modify.</param>
        private static void CustomParameters(Sms sms)
        {
            System.Console.WriteLine("P1:");
            sms.P1 = ReadDouble();
            System.Console.WriteLine("P2:");
            sms.P2 = ReadDouble();
            System.Console.WriteLine("R:");
            sms.R = ReadDouble();
            System.Console.WriteLine("Count:");
            sms.TotalCount = (Int32)ReadDouble();
        }

        private static Double ReadDouble()
        {
            Double result = 0.0;
            while (!Double.TryParse(System.Console.ReadLine(), out result))
            {
                System.Console.WriteLine("Invalid format for floating point number!");
            }
            return result;
        }
    }
}
