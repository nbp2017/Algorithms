using GreedyAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunGreedyAlgorithm();

            Console.ReadKey();
        }

        public static void RunGreedyAlgorithm()
        {
            Problem3_1_1();
            Problem3_1_2();
        }

        public static void Problem3_1_1()
        {
            string dataFilePath = @"..\..\..\jobs.txt";
            WeightSumCompletionTimeCalculator calculator =
                new WeightSumCompletionTimeCalculator(dataFilePath);

            double resultOne = calculator.RunAlgorithmOne();
            double resultTwo = calculator.RunAlgorithmTwo();

            Console.WriteLine(resultOne);
            Console.WriteLine(resultTwo);

            Console.WriteLine("1: " + (resultOne - 69119377652.0));
            Console.WriteLine("2: " + (resultTwo - 77949246841.0));
        }

        public static void Problem3_1_2()
        {
            string dataFilePath = @"..\..\..\edges.txt";
            PrimsAlgorithmCalculator pac = new PrimsAlgorithmCalculator(dataFilePath);

            int result = pac.RunAlgorithm();  // -3612829

            Console.WriteLine(result);

        }
    }
}
