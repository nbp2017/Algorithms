using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyAlgorithm
{
    public class WeightSumCompletionTimeCalculator
    {
        class AlgorithmOneComparer : IComparer<Tuple<int, int, int>>
        {
            public int Compare(Tuple<int, int, int> x, Tuple<int, int, int> y)
            {
                if (x.Item1 < y.Item1)
                    return -1;
                else if (x.Item1 > y.Item1)
                    return 1;
                else
                {
                    if (x.Item2 > y.Item2)
                        return -1;
                    else if (x.Item2 < y.Item2)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        string dataFilePath;
        Data data = new Data();

        public WeightSumCompletionTimeCalculator(string dataFilePath)
        {
            this.dataFilePath = dataFilePath;
            ParseData();
        }

        private void ParseData()
        {
            DataParser dp = new DataParser(dataFilePath);
            dp.Parse(data);
        }

        public void RunAlgorithmOne()
        {
            IList<Tuple<int, int>> weightLengthList = data.GetWeightLengthList();

            SortedSet<Tuple<int, int, int>> sortedCompTime = new SortedSet<Tuple<int, int, int>>(new AlgorithmOneComparer());

            foreach (Tuple<int, int> pair in weightLengthList)
            {
                int score = pair.Item1 - pair.Item2;
                
                sortedCompTime.Add(new Tuple<int, int, int>(score, pair.Item1, pair.Item2));
            }

            int sum = 0;

            foreach (Tuple<int, int, int> pair in sortedCompTime)
            {
                sum += pair.Item1;  // TODO: how does the score work;
            }


        }

        public void RunAlgorithmTwo()
        {

        }


    }

    public class DataParser
    {
        string filepath;

        public DataParser(string filepath)
        {
            this.filepath = filepath;
        }

        public void Parse(Data data)
        {
            string[] parsedLines = File.ReadAllLines(filepath);

            for (int i = 1; i < parsedLines.Length; i++)
            {
                string parsedLine = parsedLines[i];

                string[] tokens = parsedLine.Split();

                if (tokens.Length < 2)
                    continue;

                int weight = int.Parse(tokens[0]);
                int length = int.Parse(tokens[1]);

                data.Add(new Tuple<int, int>(weight, length));
            }
        }
    }

    public class Data
    {
        List<Tuple<int, int>> weightLengthCollection = new List<Tuple<int, int>>();

        public void Add(Tuple<int, int> item)
        {
            weightLengthCollection.Add(item);
        }

        public IList<Tuple<int, int>> GetWeightLengthList()
        {
            return weightLengthCollection;
        }
    }
}
