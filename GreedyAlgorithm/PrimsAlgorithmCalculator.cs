using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyAlgorithm
{
    public class PrimsAlgorithmCalculator
    {
        class Graph
        {
            Dictionary<int, Dictionary<int, int>> nodeNodeWeightGraph =
                new Dictionary<int, Dictionary<int, int>>();

            public void Add(int i, int j, int weight)
            {
                if (!nodeNodeWeightGraph.ContainsKey(i))
                    nodeNodeWeightGraph.Add(i, new Dictionary<int, int>());

                if (!nodeNodeWeightGraph.ContainsKey(j))
                    nodeNodeWeightGraph.Add(j, new Dictionary<int, int>());

                nodeNodeWeightGraph[i].Add(j, weight);
                nodeNodeWeightGraph[j].Add(i, weight);

            }

            public Dictionary<int, int> GetConnection(int node)
            {
                return nodeNodeWeightGraph[node];
            }
        }

        class MinSpanningTree
        {
            HashSet<int> nodes = new HashSet<int>();
            List<Tuple<int, int, int>> edges = new List<Tuple<int, int, int>>();

            public MinSpanningTree(int initialNode)
            {
                nodes.Add(initialNode);
            }

            public void Add(Tuple<int, int, int> edge)
            {
                edges.Add(edge);
            }

            public bool ContainsNode(int node)
            {
                return nodes.Contains(node);
            }
        }

        class Frontier
        {
            SortedDictionary<int, HashSet<int>> minCutTree = new SortedDictionary<int, HashSet<int>>();

            public void Add(int weight, int node)
            {
                if (!minCutTree.ContainsKey(weight))
                    minCutTree.Add(weight, new HashSet<int>());

                minCutTree[weight].Add(node);
            }

            public void Remove(int weight, int node)
            {
                if (minCutTree.ContainsKey(weight))
                {
                    if (minCutTree[weight].Count <= 1)
                        minCutTree.Remove(weight);
                    else
                        minCutTree[weight].Remove(node);
                }
            }

            public Tuple<int, int> GetMinimumCut()
            {
                KeyValuePair<int, HashSet<int>> minWeightNodes = minCutTree.First();

                return new Tuple<int, int>(minWeightNodes.Key, minWeightNodes.Value.First());
            }
        }
        
        string dataFilePath;
        PrimsData data = new PrimsData();
        Graph graph = new Graph();

        public PrimsAlgorithmCalculator(string dataFilePath)
        {
            this.dataFilePath = dataFilePath;
            ParseData();
        }

        private void ParseData()
        {
            PrimsDataParser dp = new PrimsDataParser(dataFilePath);
            dp.Parse(data);
        }

        private void CreateGraph()
        {
            IList<Tuple<int, int, int>> edges = data.GetGraphList();

            foreach(Tuple<int, int, int> edge in edges)
            {
                graph.Add(edge.Item1, edge.Item2, edge.Item3);
            }
        }

        public int RunAlgorithm()
        {
            // initialize the min spanning tree and frontier

            // Pick first node to put into the min spanning tree

            // add the other nodes into the frontier

            // pick the node with the lowest weight and add it to the mst

            // remove node from frontier and adjust connecting nodes

            // repeat last two steps 
            return 0;
        }
    }

    public class PrimsDataParser
    {
        string filepath;

        public PrimsDataParser(string filepath)
        {
            this.filepath = filepath;
        }

        public void Parse(PrimsData data)
        {
            string[] parsedLines = File.ReadAllLines(filepath);

            for (int i = 1; i < parsedLines.Length; i++)
            {
                string parsedLine = parsedLines[i];

                string[] tokens = parsedLine.Split();

                if (tokens.Length < 3)
                    continue;

                int nodeOneId = int.Parse(tokens[0]);
                int nodeTwoId = int.Parse(tokens[1]);
                int nodeThreeId = int.Parse(tokens[2]);

                data.Add(new Tuple<int, int, int>(nodeOneId, nodeTwoId, nodeThreeId));
            }
        }
    }

    public class PrimsData
    {
        List<Tuple<int, int, int>> graphCollection = new List<Tuple<int, int, int>>();

        public void Add(Tuple<int, int, int> item)
        {
            graphCollection.Add(item);
        }

        public IList<Tuple<int, int, int>> GetGraphList()
        {
            return graphCollection;
        }
    }
}
