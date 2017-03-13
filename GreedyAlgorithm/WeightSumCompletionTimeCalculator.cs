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

        public IList<Tuple<int, int>> GetWeightLengthCollection()
        {
            return weightLengthCollection;
        }
    }
}
