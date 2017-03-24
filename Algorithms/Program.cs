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
            Test1();

            //Test2();

            Console.ReadKey();
        }

        public static void Test1()
        {
            string dataFilePath = @"..\..\..\jobs.txt";
            WeightSumCompletionTimeCalculator calculator =
                new WeightSumCompletionTimeCalculator(dataFilePath);

            int resultOne = calculator.RunAlgorithmOne();
            int resultTwo = calculator.RunAlgorithmTwo();

            Console.WriteLine(resultOne);
            Console.WriteLine(resultTwo);
        }

        public static void Test2()
        {
            string dataFilePath = @"..\..\..\edges.txt";
            PrimsAlgorithmCalculator pac = new PrimsAlgorithmCalculator(dataFilePath);

            int result = pac.RunAlgorithm();

            Console.WriteLine(result);

        }
    }
}
