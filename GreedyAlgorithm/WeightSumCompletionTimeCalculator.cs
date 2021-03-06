﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyAlgorithm
{
    public class WeightSumCompletionTimeCalculator
    {
        class Job
        {
            public double Score { get; set; }
            public int Weight { get; set; }
            public int Length { get; set; }
        }

        class AlgorithmOneComparer : IComparer<Job>
        {
            public int Compare(Job x, Job y)
            {
                if (x.Score < y.Score)
                    return 1;
                else if (x.Score > y.Score)
                    return -1;
                else
                {
                    if (x.Weight > y.Weight)
                        return -1;
                    else if (x.Weight < y.Weight)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        string dataFilePath;
        WeightSumCompletionTimeData data = new WeightSumCompletionTimeData();

        public WeightSumCompletionTimeCalculator(string dataFilePath)
        {
            this.dataFilePath = dataFilePath;
            ParseData();
        }

        private void ParseData()
        {
            WeightSumCompletionTimeDataParser dp = new WeightSumCompletionTimeDataParser(dataFilePath);
            dp.Parse(data);
        }

        public double RunAlgorithmOne()
        {
            IList<Tuple<int, int>> weightLengthList = data.GetWeightLengthList();

            SortedSet<Job> sortedCompTime = new SortedSet<Job>(new AlgorithmOneComparer());

            foreach (Tuple<int, int> pair in weightLengthList)
            { 
                double score = (double)pair.Item1 - (double)pair.Item2;

                sortedCompTime.Add(new Job() { Score = score, Weight = pair.Item1, Length = pair.Item2 });
            }

            double sum = 0.0;
            double length = 0;

            Job jobPrev = new Job() { Score = double.MaxValue };
            int count = 0;

            foreach (Job item in sortedCompTime)
            {
                count++;

                length += item.Length;
                sum += (double)item.Weight * length;

                if (length > .8 * int.MaxValue)
                    Debug.Assert(false);

                if (sum > .8 * double.MaxValue)
                    Debug.Assert(false);

                //if (jobPrev.Score <= item.Score)
                //    Debug.Assert(false);

                jobPrev = item;

            }

            return sum;


        }

        public double RunAlgorithmTwo()
        {
            IList<Tuple<int, int>> weightLengthList = data.GetWeightLengthList();

            SortedSet<Job> sortedCompTime = new SortedSet<Job>(new AlgorithmOneComparer());

            foreach (Tuple<int, int> pair in weightLengthList)
            {
                double score = (double)pair.Item1 / (double)pair.Item2;

                sortedCompTime.Add(new Job() { Score = score, Weight = pair.Item1, Length = pair.Item2 });
            }

            double sum = 0;
            int length = 0;

            foreach (Job item in sortedCompTime)
            {
                length += item.Length;
                sum += (double)item.Weight * (double)length;
            }

            return sum;
        }


    }

    public class WeightSumCompletionTimeDataParser
    {
        string filepath;

        public WeightSumCompletionTimeDataParser(string filepath)
        {
            this.filepath = filepath;
        }

        public void Parse(WeightSumCompletionTimeData data)
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

    public class WeightSumCompletionTimeData
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
